using ApiWorkShop.Data;
using ApiWorkShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Service
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetAllUsuarios();
        public Task<Usuario> GetUsuarioById(int id);
        public Task<Usuario> GetLoginUser(Usuario user);
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly MyDbContext _context;
        public UsuarioService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios
                                 .Where(u => u.UsuarioId == id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            return await _context.Usuarios
                .Include(p => p.Produtos)
                .ToListAsync();
        }

        public async Task<Usuario> GetLoginUser(Usuario user)
        {
            return await _context.Usuarios
                .Where(u => u.Email == user.Email).FirstOrDefaultAsync();

        }
    }
}
