using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Infra.Mapeamentos
{
    public class TicketSituacaoMapeamento : IEntityTypeConfiguration<TicketSituacao>
    {
        public void Configure(EntityTypeBuilder<TicketSituacao> builder)
        {
            builder.ToTable("TicketSituacao");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("SMALLINT").ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(64);
        }
    }
}
