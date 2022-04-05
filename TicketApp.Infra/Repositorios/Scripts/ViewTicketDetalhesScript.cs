using System.Collections.Generic;

namespace TicketApp.Infra.Repositorios.Scripts
{
    public class ViewTicketDetalhesScript
    {
        private static readonly string SELECT_BASE = @"SELECT 
  	                                                         t1.Id, 
  	                                                         t1.IdUsuarioAbertura,
  	                                                         t1.CodigoUsuarioAbertura, 
  	                                                         t1.NomeUsuarioAbertura,
  	                                                         t1.IdUsuarioConclusao,
  	                                                         t1.CodigoUsuarioConclusao, 
  	                                                         t1.NomeUsuarioConclusao,
  	                                                         t1.IdCliente,
  	                                                         t1.CodigoCliente,
  	                                                         t1.CpfCliente, 	
  	                                                         t1.IdTicketSituacao,
  	                                                         t1.SituacaoTicket,
  	                                                         t1.CodigoTicket,
  	                                                         t1.DataAbertura,
  	                                                         t1.DataConclusao,
  	                                                         t1.QuantidadeAnotacoes
                                                       FROM VW_TicketDetalhes as t1 
                                                       WHERE t1.id is not null";

        public static string BuscaDetalhadaQuery(int codigoTicket, string codigoUsuario, string nomeUsuario, string codigoCliente, string cpfCliente)
        {
            List<string> parametros = new();

            if (codigoTicket > 0)
                parametros.Add("t1.CodigoTicket = @codigoTicket");
            if (!string.IsNullOrEmpty(codigoUsuario))
                parametros.Add("(t1.CodigoUsuarioAbertura = @codigoUsuario OR t1.CodigoUsuarioConclusao = @codigoUsuario)");
            if(!string.IsNullOrEmpty(nomeUsuario))
                parametros.Add("(t1.NomeUsuarioConclusao like @nomeUsuario OR t1.NomeUsuarioAbertura like @nomeUsuario)");     
            if(!string.IsNullOrEmpty(codigoCliente))
                parametros.Add("t1.CodigoCliente = @codigoCliente");            
            if(!string.IsNullOrEmpty(cpfCliente))
                parametros.Add("t1.CpfCliente = @cpfCliente");

            return SELECT_BASE + " AND " + string.Join(" AND ", parametros);
        }
    }
}
