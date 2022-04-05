using System;

namespace TicketApp.Dominio.DTO
{
    public class TicketDTO
    {
        public long Id { get; set; }
        public long IdUsuarioAbertura { get; set; }
        public long? IdUsuarioConclusao { get; set; }
        public long IdCliente { get; set; }
        public short IdTicketSituacao { get; set; }
        public int Codigo { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
    }
}
