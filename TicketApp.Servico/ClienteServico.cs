using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Dominio.Interfaces.Servico;
using TicketApp.Dominio.Utils;

namespace TicketApp.Servico
{
    public class ClienteServico : IClienteServico
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IMapper mapper, IClienteRepositorio clienteRepositorio)
        {
            _mapper = mapper;
            _clienteRepositorio = clienteRepositorio;
        }

        public ResultDTO Editar(ClienteEditarDTO clienteEditarDTO)
        {
            try
            {
                var cliente = _clienteRepositorio.GetById(clienteEditarDTO.Id);

                if (cliente == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Cliente não encontrado com Id = {clienteEditarDTO.Id}." });

                if (string.IsNullOrEmpty(clienteEditarDTO.CPF))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"CPF e um campo obrigatório para editar." });

                string cpf = Regex.Replace(clienteEditarDTO.CPF, "[^0-9]", "");

                if (cpf.Length != 11)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"CPF inválido." });

                cliente.CPF = cpf;

                _clienteRepositorio.Update(cliente);
                _clienteRepositorio.Commit();

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<Cliente, ClienteDTO>(cliente),
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
                var cliente = _clienteRepositorio.GetById(id);
                if (cliente == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Cliente não encontrado com Id = {id}." });

                _clienteRepositorio.Delete(cliente);
                _clienteRepositorio.Commit();
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

        public ResultDTO Get(string cpf)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpf))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Informe o filtro cpf para busca." });

                cpf = Regex.Replace(cpf, "[^0-9]", "");

                if (cpf.Length != 11)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"CPF inválido." });


                var cliente = _clienteRepositorio
                              .Get
                              .Where(x => x.CPF.Contains(cpf))
                              .Select(x => new ClienteDTO
                              {
                                  Id = x.Id,
                                  CPF = x.CPF,
                                  Codigo = x.Codigo
                              }).AsEnumerable();

                return new ResultDTO()
                {
                    DataObject = cliente,
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

        public ResultDTO GetById(long id)
        {
            try
            {
                var cliente = _clienteRepositorio
                    .Get
                    .Where(x => x.Id == id)
                    .Select(s => new ClienteDTO
                    {
                        Id = s.Id,
                        CPF = s.CPF,
                        Codigo = s.Codigo
                    }).FirstOrDefault();

                if (cliente == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Cliente não encontrado com Id = {id}." });

                return new ResultDTO()
                {
                    DataObject = cliente,
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

        public ResultDTO Salvar(ClienteSalvarDTO clienteSalvarDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(clienteSalvarDTO.CPF))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"CPF e um campo obrigatório para o cadastro." });

                string cpf = Regex.Replace(clienteSalvarDTO.CPF, "[^0-9]", "");

                if (cpf.Length != 11)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"CPF inválido." });

                var cliente = new Cliente()
                {
                    CPF = cpf,
                    Codigo = _clienteRepositorio.GetCodigoSequence().ToString()
                };

                cliente = _clienteRepositorio.Add(cliente, commit: true);

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<Cliente, ClienteDTO>(cliente),
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