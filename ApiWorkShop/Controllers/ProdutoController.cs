using ApiWorkShop.Model;
using ApiWorkShop.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiWorkShop.Controllers
{
    // Classe de controle principal de nossas rotas usadas pelo frontend
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMainService _mainService;


        // Construtor para injetar na classe ao se iniciada nosso services,
        // dessa forma temos acesso a todos os comandos desses classes dentro do nosso controller.
        public ProdutoController(IProdutoService produtoService, IMainService mainService)
        {
            _produtoService = produtoService;
            _mainService = mainService;
        }

        // Rota para adicionar produto
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            try
            {
                // invoca nosso service passando como atributo o model vindo do frontend, e declarando que é do tipo produto
                _mainService.Add<Produto>(produto);

                if (await _mainService.saveChangesAsync())
                {
                    // retorna o produto criado
                    return Ok(await _produtoService.GetById(produto.ProdutoId));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                // retorna um erro.
                throw ex;
            }
        }

        // Rota para upload de imagem, guarda a imagem no servidor
        [HttpPost("upload")]
        public async Task<IActionResult> UploadDeImagem()
        {

            // Upload de imagem para o servidor
            try
            {
                // Cria uma pasta caso não exita
                if (!Directory.Exists("Images"))
                {
                    // comando para criação de pasta
                    Directory.CreateDirectory("Images");
                }

                //captura em uma variável o file enviado por request.
                var file =  Request.Form.Files[0];

                // Criação do caminho para salvar a imagem no servidor.
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Images");

                // verifica se o arquivo chegou com sucesso. 
                if(file.Length > 0)
                {
                    // Coloca na variável o nome do file.
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                    // Cria o caminho completo com o nome do file. Remove caracteres indesejados.
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    // retorna em caso de sucesso
                    return Ok("Imagem salva com sucesso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BadRequest("Erro ao fazer upload da imagem");
        
        }

        //Rota para update de produtos
        [HttpPut("update")]
        public async Task<IActionResult> AtualizarProduto(Produto model)
        {
            try
            {
                // Salva na variável o produto
                var produto = await _produtoService.GetById(model.ProdutoId);

                if (produto == null) return BadRequest();

                // verifica se há mudança de imagem no produto
                if (String.IsNullOrEmpty(model.ImagemUrl))
                    model.ImagemUrl = produto.ImagemUrl;

                // atribui id do produto ao model do request
                model.ProdutoId = produto.ProdutoId;

                // cria o update do produto.
                _mainService.Update<Produto>(model);

                // salva o produto no banco de dados.
                if (await _mainService.saveChangesAsync())
                {
                    //retorna o produto criado com sucesso
                    return Ok(await _produtoService.GetById(model.ProdutoId));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Rota Para deleção do Produto.
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            try
            {
                // Recupera o Produto
                var produto = await _produtoService.GetById(id);
                // Verifica se ele existe 
                if (produto == null) return BadRequest();
                // faz a exclusão do Protudo pelo nosso service.
                _mainService.Delete<Produto>(produto);

                if (await _mainService.saveChangesAsync())
                {
                    // retorna o Produto excluido
                    return Ok($"Produto {produto.Nome} Removido");
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Rota para recuperar do banco todos os Produto e exibir os dados.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // recupera os Produto e atribui a uma variável para exibição.
                var produtos = await _produtoService.GetAllProducts();
                //verifica se deu certo.
                if (produtos == null)
                    return BadRequest();
                // verifica se existe Produto
                if (produtos.Count == 0)
                    return BadRequest("Não foi possível trazer Produtos, lista vazia");
                // retorna todos Produto
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //Recupera um Produto pelo o id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                // variável para Produto recuperado do banco
                var produto = await _produtoService.GetById(id);
                // verifica se deu certo
                if (produto == null) return BadRequest("Não foi possível trazer o Produto");
                // Exibe Produto
                return Ok(produto);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
