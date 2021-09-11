using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Data
{
    public class MyDbContext: DbContext
    {
        
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

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
