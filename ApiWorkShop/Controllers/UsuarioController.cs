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
    // Classe de controle principal de nossas rotas usadas pelo frontend

    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMainService _mainService;

        // Construtor para injetar na classe ao se iniciada nosso services,
        // dessa forma temos acesso a todos os comandos desses classes dentro do nosso controller.
        public UsuarioController(IUsuarioService usuarioService, IMainService mainService)
        {
            _usuarioService = usuarioService;
            _mainService = mainService;
        }

        // Rota padrão para adicionar um usuário
        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(Usuario usuario)
        {
            try
            {
                // invoca nosso service passando como atributo o model vindo do frontend, e declarando que é do tipo usuário
                _mainService.Add<Usuario>(usuario); 

                if(await _mainService.saveChangesAsync())
                {
                    // retorna o usuário criado
                    return Ok ( await _usuarioService.GetUsuarioById(usuario.UsuarioId));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                // retorna um erro.
                throw ex;
            }
        }

        //Rota para atualização de usuário
        [HttpPut("update")]
        public async Task<IActionResult> AtualizarUsuario(Usuario model)
        {
            try
            {
                // Recupera o usuário para atualiza-lo
                var usuario = await _usuarioService.GetUsuarioById(model.UsuarioId);
                if (usuario == null) return BadRequest("Usuario não existe");
                // atribui o mesmo id ao nosso model vindo do front como requisição.
                model.UsuarioId = usuario.UsuarioId;

                // Passa para a classe mainservice nosso model do tipo usuario para atualizá-lo.
                _mainService.Update<Usuario>(model);

                if(await _mainService.saveChangesAsync())
                {
                    // retorna o usuário atualizado
                    return Ok(await _usuarioService.GetUsuarioById(model.UsuarioId));
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return BadRequest("Não foi possivel atualizar o Usuário");
        }

        // Rota Para deleção do usuário.
        [HttpPut("Delete")]
        public async Task<IActionResult> DeletarUsuario(Usuario model)
        {
            try
            {
                // Recupera o usuário
                var usuario = await _usuarioService.GetUsuarioById(model.UsuarioId);
                // Verifica se ele existe 
                if (usuario == null) return BadRequest("Usuario não existe");
                
                model.UsuarioId = usuario.UsuarioId;

                // faz a exclusão do usuario pelo nosso service.
                _mainService.Delete<Usuario>(model);

                if (await _mainService.saveChangesAsync())
                {
                    // retorna o usuário excluido
                    return Ok($"Usuário {usuario.Nome} Removido");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return BadRequest("Não foi possivel excluir o Usuário");
        }

        // Rota para recuperar do banco todos os usuários e exibir os dados.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // recupera os usuários e atribui a uma variável para exibição.
                var usuarios = await _usuarioService.GetAllUsuarios();
                //verifica se deu certo.
                if (usuarios == null)
                    return BadRequest();
                // verifica se existe usuário
                if (usuarios.Count == 0)
                    return BadRequest("Não foi possível trazer Usuários, lista vazia");
                // retorna todos usuários
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //Recupera um usuário pelo o id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                // variável para usuário recuperado do banco
                var usuario = await _usuarioService.GetUsuarioById(id);

                // verifica se deu certo
                if (usuario == null) return BadRequest("Não foi possível trazer o Usuário");
                
                // Exibe usuário
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Rota para login de usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login(Usuario user)
        {
            try
            {
                //Recupera o usuario pelo email.
                var usuario = await _usuarioService.GetLoginUser(user);
                // Verifica se deu certo
                if (usuario == null) return BadRequest();
                // verifica se a senha encripitada está correta.
                if (BCrypt.Net.BCrypt.Verify(user.Senha,usuario.Senha))
                {
                    // retorna o usuário para o frontend
                    return Ok(usuario);
                }
                else
                {
                    // Retorna caso não encontrado.
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
