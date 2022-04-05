using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Infra.Mapeamentos
{
    public class TicketMapeamento : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("BIGINT").ValueGeneratedOnAdd();
            builder.Property(x => x.IdUsuarioAbertura).HasColumnName("IdUsuarioAbertura").HasColumnType("BIGINT");
            builder.Property(x => x.IdUsuarioConclusao).HasColumnName("IdUsuarioConclusao").HasColumnType("BIGINT");
            builder.Property(x => x.IdCliente).HasColumnName("IdCliente").HasColumnType("BIGINT");
            builder.Property(x => x.IdTicketSituacao).HasColumnName("IdTicketSituacao").HasColumnType("SMALLINT");
            builder.Property(x => x.Codigo).HasColumnName("Codigo").HasColumnType("INT");
            builder.Property(x => x.DataAbertura).HasColumnName("DataAbertura").HasColumnType("DATETIME");
            builder.Property(x => x.DataConclusao).HasColumnName("DataConclusao").HasColumnType("DATETIME");

            builder.HasOne(d => d.UsuarioAbertura).WithMany().HasForeignKey(d => d.IdUsuarioAbertura);
            builder.HasOne(d => d.UsuarioConclusao).WithMany().HasForeignKey(d => d.IdUsuarioConclusao);
            builder.HasOne(d => d.Cliente).WithMany().HasForeignKey(d => d.IdCliente);
            builder.HasOne(d => d.TicketSituacao).WithMany().HasForeignKey(d => d.IdTicketSituacao);
        }
    }
}
