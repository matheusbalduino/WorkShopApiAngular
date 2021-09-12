using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Data
{
    // Classe para declarar Para o EntityFramework nossas tabelas 

    public class MyDbContext: DbContext
    {
        
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //Função para dizer que deve ser construida as tabelas baseado no Mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        // Add-Migration Initial -Verbose -Context MyDbContext
        // Update-Database -Context MyDbContext
        // 
    }
}
