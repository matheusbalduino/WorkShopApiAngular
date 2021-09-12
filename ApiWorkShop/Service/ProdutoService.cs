using ApiWorkShop.Data;
using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Service
{
    // Interface para a classe ProdutoService
    public interface IProdutoService 
    {
        public Task<List<Produto>> GetAllProducts();
        public Task<Produto> GetById(int id);
    }

    // Classe Para acesso aos produtos no banco de dados
    public class ProdutoService : IProdutoService
    {
        // Variável do tipo DbContext para uso do EntityFrameWork
        private readonly MyDbContext _context;

        // Construtor para iniciar a classe com o DbContext, 
        // Classe recebe como Parâmetro o DbContext que foi construido com as tabelas 
        // Com a biblioteca do EntityFramework
        public ProdutoService(MyDbContext context)
        {
            _context = context;
        }

        // Função Para recuperar todos os Produtos da tabela Produtos
        public async Task<List<Produto>> GetAllProducts()
        {
            return await _context.Produtos.ToListAsync();
        }

        //Função para recuperar um produto específico pelo seu id
        public async Task<Produto> GetById(int id)
        {
            return await _context.Produtos
                                 .Where(p => p.ProdutoId == id)
                                 .Include(u=>u.Usuario)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();
        }
    }
}
