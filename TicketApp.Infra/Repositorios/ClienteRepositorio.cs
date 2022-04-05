using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using TicketApp.Base;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Infra.Contextos;

namespace TicketApp.Infra.Repositorios
{
    public class ClienteRepositorio : RepositorioBase<Cliente, long>, IClienteRepositorio
    {
        private readonly TicketContexto _context;
        public ClienteRepositorio(TicketContexto context) : base(context)
        {
            _context = context;
        }

        public long GetCodigoSequence()
        {
            string query = "SELECT NEXT VALUE FOR SQ_ClienteCodigo";
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var secondsTimeOut = (int)TimeSpan.FromMinutes(2).TotalMilliseconds;
                return connection.QueryFirst<long>(query, param: null, transaction: null, commandTimeout: secondsTimeOut, commandType: null);
            }
        }
    }
}
