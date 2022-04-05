using TicketApp.Base;

namespace TicketApp.Dominio.Entidades
{
    public class Cliente : Entity<Cliente, long>
    {
        public string Codigo { get; set; }
        public string CPF { get; set; }
    }
}
