using System;
using TicketApp.Base;

namespace TicketApp.Dominio.Entidades
{
    public class TicketAnotacao : Entity<TicketAnotacao, long>
    {
        public long IdTicket { get; set; }
        public long IdUsuario { get; set; }
        public string Texto { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
