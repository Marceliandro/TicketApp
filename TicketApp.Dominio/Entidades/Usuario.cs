using TicketApp.Base;

namespace TicketApp.Dominio.Entidades
{
    public class Usuario : Entity<Usuario, long>
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
