using ApiWorkShop.Data;
using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Service
{
    public interface IProdutoService 
    {
        public Task<List<Produto>> GetAllProducts();
        public Task<Produto> GetById(int id);
    }
    public class ProdutoService : IProdutoService
    {
        private readonly MyDbContext _context;

        public ProdutoService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Produto>> GetAllProducts()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _context.Produtos
                                 .Where(p => p.ProdutoId == id)
                                 .FirstOrDefaultAsync();
        }
    }
}
