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
using TicketApp.Dominio.Utils.Auth;
using TicketApp.Dominio.Utils.Cryptography;

namespace TicketApp.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IMapper _mapper;
        public readonly IUsuarioRepositorio _usuarioRepositorio;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public UsuarioServico(
            IMapper mapper,
            IUsuarioRepositorio usuarioRepositorio,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public ResultDTO Get(string nome, string login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(login))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Informe pelo menos um filtro para busca." });

                var usuarios = _usuarioRepositorio
                    .Get
                    .Where(x => x.Nome.Contains(nome) || x.Login.Contains(login))
                    .Select(x => new UsuarioDTO
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Codigo = x.Codigo,
                        Login = x.Login
                    }).AsEnumerable();

                return new ResultDTO()
                {
                    DataObject = usuarios,
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

        public ResultDTO Editar(UsuarioEditarDTO dto)
        {
            try
            {
                var usuario = _usuarioRepositorio.GetById(dto.Id);

                if (usuario == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Usuario não encontrado com Id = {dto.Id}" });

                if (string.IsNullOrEmpty(dto.Nome))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Nome e um campo obrigatório para editar." });

                usuario.Nome = dto.Nome;

                _usuarioRepositorio.Update(usuario);
                _usuarioRepositorio.Commit();

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<Usuario, UsuarioDTO>(usuario),
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
                var usuario = _usuarioRepositorio.GetById(id);
                if (usuario == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Usuário não encontrado com Id = {id}" });

                _usuarioRepositorio.Delete(usuario);
                _usuarioRepositorio.Commit();
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
                var usuario = _usuarioRepositorio
                    .Get
                    .Where(x => x.Id == id)
                    .Select(s => new UsuarioDTO
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        Codigo = s.Codigo,
                        Login = s.Login
                    }).FirstOrDefault();

                if (usuario == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = $"Usuário não encontrado com Id = {id}" });

                return new ResultDTO()
                {
                    DataObject = usuario,
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

        public ResultDTO Salvar(UsuarioSalvarDTO usuarioDto)
        {
            try
            {
                if (string.IsNullOrEmpty(usuarioDto.Nome))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Nome e um campo obrigatório para o cadastro" });

                if (string.IsNullOrEmpty(usuarioDto.Login))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Login e um campo obrigatório para o cadastro" });

                if (string.IsNullOrEmpty(usuarioDto.Senha))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Senha e um campo obrigatório para o cadastro" });

                var usuario = new Usuario()
                {
                    Nome = usuarioDto.Nome,
                    Login = usuarioDto.Login,
                    Senha = Aes128.Encrypt(usuarioDto.Senha),
                    Codigo = _usuarioRepositorio.GetCodigoSequence().ToString()
                };

                usuario = _usuarioRepositorio.Add(usuario, commit: true);

                return new ResultDTO()
                {
                    DataObject = _mapper.Map<Usuario, UsuarioDTO>(usuario),
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

        public ResultDTO Auth(UsuarioAuthDTO usuarioAuthDTO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuarioAuthDTO.Login) && string.IsNullOrWhiteSpace(usuarioAuthDTO.Senha))
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Login e senha são obrigatorios." });

                var usuario = _usuarioRepositorio.Get.Where(x => x.Login.Equals(usuarioAuthDTO.Login) && x.Senha.Equals(Aes128.Encrypt(usuarioAuthDTO.Senha))).FirstOrDefault();
                if (usuario == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = $"Login e/ou senha inválido." });

                JwtAuth jwtAuth = new(_signingConfigurations, _tokenConfigurations);

                return new ResultDTO()
                {
                    DataObject = jwtAuth.CreateToken(usuarioAuthDTO.Login),
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
