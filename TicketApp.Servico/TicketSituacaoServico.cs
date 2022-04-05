using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Dominio.Interfaces.Servico;
using TicketApp.Dominio.Utils;

namespace TicketApp.Servico
{
    public class TicketSituacaoServico : ITicketSituacaoServico
    {
        private readonly IMapper _mapper;
        private readonly ITicketSituacaoRepositorio _ticketSituacaoRepositorio;

        public TicketSituacaoServico(IMapper mapper, ITicketSituacaoRepositorio ticketSituacaoRepositorio)
        {
            _mapper = mapper;
            _ticketSituacaoRepositorio = ticketSituacaoRepositorio;
        }

        public ResultDTO Editar(TicketSituacaoEditarDTO ticketSituacaoEditarDTO)
        {
            try
            {
                var ticketSituacao = _ticketSituacaoRepositorio.GetById(ticketSituacaoEditarDTO.Id);

                if (ticketSituacao == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket Situação não encontrado com Id = {ticketSituacaoEditarDTO.Id}." });

                if (string.IsNullOrEmpty(ticketSituacaoEditarDTO.Nome))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Nome e um campo obrigatório para editar." });

                ticketSituacao.Nome = ticketSituacaoEditarDTO.Nome;

                _ticketSituacaoRepositorio.Update(ticketSituacao);
                _ticketSituacaoRepositorio.Commit();

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<TicketSituacao, TicketSituacaoDTO>(ticketSituacao),
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

        public void Excluir(short id)
        {
            try
            {
                var ticketSituacao = _ticketSituacaoRepositorio.GetById(id);
                if (ticketSituacao == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket Situação não encontrado com Id = {id}." });

                _ticketSituacaoRepositorio.Delete(ticketSituacao);
                _ticketSituacaoRepositorio.Commit();
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

        public ResultDTO Get()
        {
            try
            {
                var ticketSituacao = _ticketSituacaoRepositorio
                    .Get
                    .Select(x => new TicketSituacaoDTO
                    {
                        Id = x.Id,
                        Nome = x.Nome
                    }).AsEnumerable();

                return new ResultDTO()
                {
                    DataObject = ticketSituacao,
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

        public ResultDTO GetById(short id)
        {
            try
            {
                var ticketSituacao = _ticketSituacaoRepositorio
                    .Get
                    .Where(x => x.Id == id)
                    .Select(x => new TicketSituacaoDTO
                    {
                        Id = x.Id,
                        Nome = x.Nome
                    }).FirstOrDefault();

                if (ticketSituacao == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Ticket Situação não encontrado com Id = {id}." });

                return new ResultDTO()
                {
                    DataObject = ticketSituacao,
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

        public ResultDTO Salvar(TicketSituacaoSalvarDTO ticketSituacaoSalvarDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(ticketSituacaoSalvarDTO.Nome))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Nome e um campo obrigatório para editar." });

                var ticketSituacao = new TicketSituacao()
                {
                    Nome = ticketSituacaoSalvarDTO.Nome
                };

                ticketSituacao = _ticketSituacaoRepositorio.Add(ticketSituacao, commit: true);

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<TicketSituacao, TicketSituacaoDTO>(ticketSituacao),
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
