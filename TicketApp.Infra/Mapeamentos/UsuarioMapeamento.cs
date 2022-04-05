using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Infra.Mapeamentos
{
    public class UsuarioMapeamento : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("BIGINT").ValueGeneratedOnAdd();
            builder.Property(x => x.Codigo).HasColumnName("Codigo").HasColumnType("VARCHAR").HasMaxLength(8);
            builder.Property(x => x.Nome).HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(64);
            builder.Property(x => x.Login).HasColumnName("Login").HasColumnType("VARCHAR").HasMaxLength(20);
            builder.Property(x => x.Senha).HasColumnName("Senha").HasColumnType("VARCHAR").HasMaxLength(30);
        }
    }
}
