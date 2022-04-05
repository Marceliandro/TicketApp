using TicketApp.Base;

namespace TicketApp.Dominio.Entidades
{
    public class TicketSituacao : Entity<TicketSituacao, short>
    {
        public string Nome { get; set; }
    }
}
