using System;
using TicketApp.Base;

namespace TicketApp.Dominio.Entidades
{
    public class Ticket : Entity<Ticket, long>
    {
        public long IdUsuarioAbertura { get; set; }
        public long? IdUsuarioConclusao { get; set; }
        public long IdCliente { get; set; }
        public short IdTicketSituacao { get; set; }
        public int Codigo { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }

        public virtual Usuario UsuarioAbertura { get; set; }
        public virtual Usuario UsuarioConclusao { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual TicketSituacao TicketSituacao { get; set; }
    }
}
