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
    public class UsuarioRepositorio : RepositorioBase<Usuario, long>, IUsuarioRepositorio
    {
        private readonly TicketContexto _context;
        public UsuarioRepositorio(TicketContexto context) : base(context) 
        {
            _context = context;
        }

        public int GetCodigoSequence()
        {
            string query = "SELECT NEXT VALUE FOR SQ_UsuarioCodigo";
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var secondsTimeOut = (int)TimeSpan.FromMinutes(2).TotalMilliseconds;
                return connection.QueryFirst<int>(query, param:null, transaction: null, commandTimeout: secondsTimeOut, commandType: null);             
            }
        }
    }
}
