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
    public class TicketAnotacaoServico : ITicketAnotacaoServico
    {
        private readonly IMapper _mapper;
        private readonly ITicketAnotacaoRepositorio _ticketAnotacaoRepositorio;
        private readonly ITicketRepositorio _ticketRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public TicketAnotacaoServico(
            IMapper mapper,
            ITicketAnotacaoRepositorio ticketAnotacaoRepositorio,
            ITicketRepositorio ticketRepositorio,
            IUsuarioRepositorio usuarioRepositorio)
        {
            _mapper = mapper;
            _ticketAnotacaoRepositorio = ticketAnotacaoRepositorio;
            _ticketRepositorio = ticketRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ResultDTO AnotacoesTicket(long IdTicket)
        {
            try
            {
                var ticketAnotacao = _ticketAnotacaoRepositorio
                    .Get
                    .Select(x => new TicketAnotacaoDTO
                    {
                        Id = x.Id,
                        IdTicket = x.IdTicket,
                        IdUsuario = x.IdUsuario,
                        Texto = x.Texto,
                        DataCadastro = x.DataCadastro
                    }).AsEnumerable();

                return new ResultDTO()
                {
                    DataObject = ticketAnotacao,
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

        public ResultDTO Editar(TicketAnotacaoEditarDTO ticketAnotacaoEditarDTO)
        {
            try
            {
                var ticketAnotacao = _ticketAnotacaoRepositorio.GetById(ticketAnotacaoEditarDTO.Id);

                if (ticketAnotacao == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket Anotação não encontrado com Id = {ticketAnotacaoEditarDTO.Id}." });

                var ticket = _ticketRepositorio.GetById(ticketAnotacao.IdTicket);
                if (ticket.IdTicketSituacao == (short)TicketSituacaoEnum.Concluido)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket com Id = {ticket.Id} já concluído" });

                if (string.IsNullOrWhiteSpace(ticketAnotacaoEditarDTO.Texto))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Texto e um campo obrigatório para editar." });

                if (ticketAnotacaoEditarDTO.Texto.Length > 512)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Texto maior que o tamanho máximo de 512 caracteres permitidos." });

                ticketAnotacao.Texto = ticketAnotacaoEditarDTO.Texto;

                _ticketAnotacaoRepositorio.Update(ticketAnotacao);
                _ticketAnotacaoRepositorio.Commit();

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<TicketAnotacao, TicketAnotacaoDTO>(ticketAnotacao),
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
                var ticketAnotacao = _ticketAnotacaoRepositorio.GetById(id);
                if (ticketAnotacao == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket Anotação não encontrado com Id = {id}." });

                _ticketAnotacaoRepositorio.Delete(ticketAnotacao);
                _ticketAnotacaoRepositorio.Commit();
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
                var ticketAnotacao = _ticketAnotacaoRepositorio
                     .Get
                     .Where(x => x.Id == id)
                     .Select(x => new TicketAnotacaoDTO
                     {
                         Id = x.Id,
                         IdTicket = x.IdTicket,
                         IdUsuario = x.IdUsuario,
                         Texto = x.Texto,
                         DataCadastro = x.DataCadastro
                     }).FirstOrDefault();

                if (ticketAnotacao == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket Anotação não encontrado com Id = {id}." });

                return new ResultDTO()
                {
                    DataObject = ticketAnotacao,
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

        public ResultDTO Salvar(TicketAnotacaoSalvarDTO ticketAnotacaoSalvarDTO)
        {
            try
            {
                var ticket = _ticketRepositorio.GetById(ticketAnotacaoSalvarDTO.IdTicket);

                if (ticket == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket não encontrado com Id = {ticketAnotacaoSalvarDTO.IdTicket}." });

                if (ticket.IdTicketSituacao == (short)TicketSituacaoEnum.Concluido)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket com Id = {ticketAnotacaoSalvarDTO.IdTicket} já concluído" });

                if (_usuarioRepositorio.GetById(ticketAnotacaoSalvarDTO.IdUsuario) == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Usuário não encontrado para o IdUsuario = {ticketAnotacaoSalvarDTO.IdUsuario}." });

                if (string.IsNullOrEmpty(ticketAnotacaoSalvarDTO.Texto))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Texto e um campo obrigatório para editar." });

                if (ticketAnotacaoSalvarDTO.Texto.Length > 512)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Texto maior que o tamanho máximo de 512 caracteres permitidos." });

                var ticketAnotacao = new TicketAnotacao()
                {
                    IdTicket = ticketAnotacaoSalvarDTO.IdTicket,
                    IdUsuario = ticketAnotacaoSalvarDTO.IdUsuario,
                    Texto = ticketAnotacaoSalvarDTO.Texto,
                    DataCadastro = DateTime.Now
                };

                ticketAnotacao = _ticketAnotacaoRepositorio.Add(ticketAnotacao, commit: true);

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<TicketAnotacao, TicketAnotacaoDTO>(ticketAnotacao),
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
