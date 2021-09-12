using ApiWorkShop.Data;
using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Service
{
    // interface para a classe UsuarioService
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetAllUsuarios();
        public Task<Usuario> GetUsuarioById(int id);
        public Task<Usuario> GetLoginUser(Usuario user);
    }
    public class UsuarioService : IUsuarioService
    {
        // Variável do tipo DbContext para uso do EntityFrameWork
        private readonly MyDbContext _context;

        // Construtor para iniciar a classe com o DbContext, 
        // Classe recebe como Parâmetro o DbContext que foi construido com as tabelas 
        // Com a biblioteca do EntityFramework
        public UsuarioService(MyDbContext context)
        {
            _context = context;
        }

        //Função para Recuperar um usuário da tabela pelo seu id
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios
                                 .Where(u => u.UsuarioId == id)
                                 .FirstOrDefaultAsync();
        }

        //Função para recuperar todos os usuarios do banco
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            return await _context.Usuarios
                .Include(p => p.Produtos)
                .ToListAsync();
        }

        //Função para recuperar um usuario pelo email
        public async Task<Usuario> GetLoginUser(Usuario user)
        {
            return await _context.Usuarios
                .Where(u => u.Email == user.Email).FirstOrDefaultAsync();

        }
    }
}
