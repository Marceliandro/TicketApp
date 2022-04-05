using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Infra.Mapeamentos
{
    public class ClienteMapeamento : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("BIGINT").ValueGeneratedOnAdd();
            builder.Property(x => x.Codigo).HasColumnName("Codigo").HasColumnType("VARCHAR").HasMaxLength(16);
            builder.Property(x => x.CPF).HasColumnName("CPF").HasColumnType("VARCHAR").HasMaxLength(11);
        }
    }
}
