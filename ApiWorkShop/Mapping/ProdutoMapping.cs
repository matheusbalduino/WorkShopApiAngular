using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.ProdutoId);

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(400)");

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(80)");

            builder.Property(p => p.Preco)
                .HasColumnType("decimal");

            builder.Property(p => p.ImagemUrl)
                .HasColumnType("varchar(200)");

            builder.Property(p => p.UsuarioId)
              .HasColumnType("int");

            // Relacionamento 1:N
            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Produtos);


            builder.ToTable("Produtos");

        }
    }
}
