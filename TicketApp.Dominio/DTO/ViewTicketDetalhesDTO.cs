using System;

namespace TicketApp.Dominio.DTO
{
    public class ViewTicketDetalhesDTO
    {
        public long Id { get; set; }
        public long IdUsuarioAbertura { get; set; }
        public string CodigoUsuarioAbertura { get; set; }
        public string NomeUsuarioAbertura { get; set; }
        public long? IdUsuarioConclusao { get; set; }
        public string CodigoUsuarioConclusao { get; set; }
        public string NomeUsuarioConclusao { get; set; }
        public long IdCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string CpfCliente { get; set; }
        public short IdTicketSituacao { get; set; }
        public string SituacaoTicket { get; set; }
        public int CodigoTicket { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int? QuantidadeAnotacoes { get; set; }
    }
}
