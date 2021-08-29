using ApiWorkShop.Model;
using ApiWorkShop.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMainService _mainService;

        public UsuarioController(IUsuarioService usuarioService, IMainService mainService)
        {
            _usuarioService = usuarioService;
            _mainService = mainService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(Usuario usuario)
        {
            try
            {
                _mainService.Add<Usuario>(usuario);

                if(await _mainService.saveChangesAsync())
                {
                    return Ok ( await _usuarioService.GetUsuarioById(usuario.UsuarioId));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> AtualizarUsuario(Usuario model)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioById(model.UsuarioId);
                if (usuario == null) return BadRequest("Usuario não existe");

                model.UsuarioId = usuario.UsuarioId;

                _mainService.Update<Usuario>(model);

                if(await _mainService.saveChangesAsync())
                {
                    return Ok(await _usuarioService.GetUsuarioById(model.UsuarioId));
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return BadRequest("Não foi possivel atualizar o Usuário");
        }
        [HttpPut("Delete")]
        public async Task<IActionResult> DeletarUsuario(Usuario model)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioById(model.UsuarioId);
                
                if (usuario == null) return BadRequest("Usuario não existe");

                model.UsuarioId = usuario.UsuarioId;

                _mainService.Delete<Usuario>(model);

                if (await _mainService.saveChangesAsync())
                {
                    return Ok($"Usuário {usuario.Nome} Removido");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return BadRequest("Não foi possivel excluir o Usuário");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsuarios();

                if (usuarios == null)
                    return BadRequest();
                if (usuarios.Count == 0)
                    return BadRequest("Não foi possível trazer Usuários, lista vazia");

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var usuario = await _usuarioService.GetUsuarioById(id);

                if (usuario == null) return BadRequest("Não foi possível trazer o Usuário");

                return Ok(usuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
