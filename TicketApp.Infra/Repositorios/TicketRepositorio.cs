using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TicketApp.Base;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Infra.Contextos;
using TicketApp.Infra.Repositorios.Scripts;

namespace TicketApp.Infra.Repositorios
{
    public class TicketRepositorio : RepositorioBase<Ticket, long>, ITicketRepositorio
    {
        private readonly TicketContexto _context;
        public TicketRepositorio(TicketContexto context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ViewTicketDetalhesDTO> BuscaDetalhada(int codigoTicket, string codigoUsuario, string nomeUsuario, string codigoCliente, string cpfCliente)
        {
            string query = ViewTicketDetalhesScript.BuscaDetalhadaQuery(codigoTicket, codigoUsuario, nomeUsuario, codigoCliente, cpfCliente);
            var parametros = new DynamicParameters();
            parametros.Add("@codigoTicket", codigoTicket, DbType.Int32);
            parametros.Add("@codigoUsuario", codigoUsuario, DbType.String);
            parametros.Add("@nomeUsuario", $"%{nomeUsuario}%", DbType.String);
            parametros.Add("@codigoCliente", codigoCliente, DbType.String);
            parametros.Add("@cpfCliente", cpfCliente, DbType.String);

            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var secondsTimeOut = (int)TimeSpan.FromMinutes(2).TotalMilliseconds;
                return connection.Query<ViewTicketDetalhesDTO>(query, parametros, null, true, secondsTimeOut, CommandType.Text).ToList();
            }
        }

        public int GetCodigoSequence()
        {
            string query = "SELECT NEXT VALUE FOR SQ_TicketCodigo";
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var secondsTimeOut = (int)TimeSpan.FromMinutes(2).TotalMilliseconds;
                return connection.QueryFirst<int>(query, param: null, transaction: null, commandTimeout: secondsTimeOut, commandType: null);
            }
        }
    }
}
