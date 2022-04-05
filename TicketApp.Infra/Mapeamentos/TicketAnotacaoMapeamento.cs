using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Infra.Mapeamentos
{
    public class TicketAnotacaoMapeamento : IEntityTypeConfiguration<TicketAnotacao>
    {
        public void Configure(EntityTypeBuilder<TicketAnotacao> builder)
        {
            builder.ToTable("TicketAnotacao");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("BIGINT").ValueGeneratedOnAdd();
            builder.Property(x => x.IdTicket).HasColumnName("IdTicket").HasColumnType("BIGINT");
            builder.Property(x => x.IdUsuario).HasColumnName("IdUsuario").HasColumnType("BIGINT");
            builder.Property(x => x.Texto).HasColumnName("Texto").HasColumnType("VARCHAR").HasMaxLength(512);
            builder.Property(x => x.DataCadastro).HasColumnName("DataCadastro").HasColumnType("DateTime");

            builder.HasOne(d => d.Ticket).WithMany().HasForeignKey(d => d.IdTicket);
            builder.HasOne(d => d.Usuario).WithMany().HasForeignKey(d => d.IdUsuario);
        }
    }
}
