using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.HasKey(u => u.UsuarioId);

            builder.Property(u => u.Nome)
                .HasColumnType("varchar(80)");

            builder.Property(u => u.Sobrenome)
                .HasColumnType("varchar(80)");

            builder.Property(u => u.Senha)
                .HasColumnType("varchar(20)");

            //Relacionamento 1:N
            builder.HasMany(u => u.Produtos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);
            
            builder.ToTable("Usuarios");
        }
    }
}
