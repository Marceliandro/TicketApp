using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Enums;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Dominio.Interfaces.Servico;
using TicketApp.Dominio.Utils;

namespace TicketApp.Servico
{
    public class TicketServico : ITicketServico
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositorio _ticketRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public TicketServico(
            IMapper mapper,
            ITicketRepositorio ticketRepositorio,
            IUsuarioRepositorio usuarioRepositorio,
            IClienteRepositorio clienteRepositorio)
        {
            _mapper = mapper;
            _ticketRepositorio = ticketRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }

        public ResultDTO BuscaDetalhada(int codigoTicket, string codigoUsuario, string nomeUsuario, string codigoCliente, string cpfCliente)
        {
            try
            {
                if (codigoTicket <= 0 && string.IsNullOrWhiteSpace(codigoUsuario) && string.IsNullOrWhiteSpace(nomeUsuario) && string.IsNullOrWhiteSpace(codigoCliente) && string.IsNullOrWhiteSpace(cpfCliente))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Informe pelo menos um filtro para busca." });

                return new ResultDTO()
                {
                    DataObject = _ticketRepositorio.BuscaDetalhada(codigoTicket, codigoUsuario, nomeUsuario, codigoCliente, cpfCliente),
                    IsTrue = true,
                    Message = "success"
                };
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = MessagesException.Get(ex) });
            }
        }

        public ResultDTO Editar(TicketEditarDTO dto)
        {
            try
            {
                var ticket = _ticketRepositorio.GetById(dto.Id);

                if (ticket == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket não encontrado com Id = {dto.Id}." });

                if (_usuarioRepositorio.GetById(dto.IdUsuarioConclusao) == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Usuário não encontrado para o IdUsuarioConclusao = {dto.IdUsuarioConclusao}." });

                ticket.IdUsuarioConclusao = dto.IdUsuarioConclusao;
                ticket.IdTicketSituacao = (short)TicketSituacaoEnum.Concluido;
                ticket.DataConclusao = DateTime.Now;

                _ticketRepositorio.Update(ticket);
                _ticketRepositorio.Commit();

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<Ticket, TicketDTO>(ticket),
                    IsTrue = true,
                    Message = "success"
                };
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = MessagesException.Get(ex) });
            }
        }

        public void Excluir(long id)
        {
            try
            {
                var ticket = _ticketRepositorio.GetById(id);
                if (ticket == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket não encontrado com Id = {id}." });

                _ticketRepositorio.Delete(ticket);
                _ticketRepositorio.Commit();
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = MessagesException.Get(ex) });
            }
        }

        public ResultDTO GetById(long id)
        {
            try
            {
                var ticket = _ticketRepositorio
                    .Get
                    .Where(x => x.Id == id)
                    .Select(x => new TicketDTO
                    {
                        Id = x.Id,
                        IdUsuarioAbertura = x.IdUsuarioAbertura,
                        IdUsuarioConclusao = x.IdUsuarioConclusao,
                        IdCliente = x.IdCliente,
                        IdTicketSituacao = x.IdTicketSituacao,
                        Codigo = x.Codigo,
                        DataAbertura = x.DataAbertura,
                        DataConclusao = x.DataConclusao
                    }).FirstOrDefault();

                if (ticket == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket não encontrado com Id = {id}." });

                return new ResultDTO()
                {
                    DataObject = ticket,
                    IsTrue = true,
                    Message = "success"
                };
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = MessagesException.Get(ex) });
            }
        }

        public ResultDTO Salvar(TicketSalvarDTO ticketSalvarDTO)
        {
            try
            {
                if (_usuarioRepositorio.GetById(ticketSalvarDTO.IdUsuarioAbertura) == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Usuário não encontrado para o IdUsuarioAbertura {ticketSalvarDTO.IdUsuarioAbertura}." });

                if (_clienteRepositorio.GetById(ticketSalvarDTO.IdCliente) == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Cliente não encontrado para o IdCliente {ticketSalvarDTO.IdCliente}." });

                if (_ticketRepositorio.Get.Where(x => x.IdCliente == ticketSalvarDTO.IdCliente && x.IdTicketSituacao == (short)TicketSituacaoEnum.EmAtendimento).Count() > 0)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Já existe um Ticket aberto para o cliente de id {ticketSalvarDTO.IdCliente}" });

                var ticket = new Ticket()
                {
                    IdUsuarioAbertura = ticketSalvarDTO.IdUsuarioAbertura,
                    IdCliente = ticketSalvarDTO.IdCliente,
                    IdTicketSituacao = (short)TicketSituacaoEnum.EmAtendimento,
                    Codigo = _ticketRepositorio.GetCodigoSequence(),
                    DataAbertura = DateTime.Now,
                };

                ticket = _ticketRepositorio.Add(ticket, commit: true);

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<Ticket, TicketDTO>(ticket),
                    IsTrue = true,
                    Message = "success"
                };
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = MessagesException.Get(ex) });
            }
        }
    }
}
