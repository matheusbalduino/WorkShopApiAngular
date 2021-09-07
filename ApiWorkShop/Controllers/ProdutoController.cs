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
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMainService _mainService;

        public ProdutoController(IProdutoService produtoService, IMainService mainService)
        {
            _produtoService = produtoService;
            _mainService = mainService;
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            try
            {
                _mainService.Add<Produto>(produto);

                if (await _mainService.saveChangesAsync())
                {
                    return Ok(await _produtoService.GetById(produto.ProdutoId));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDeImagem()
        {

            // Upload de imagem para o servidor
            try
            {
                if (!Directory.Exists("Images"))
                {
                    Directory.CreateDirectory("Images");
                }

                var file =  Request.Form.Files[0];
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Images");

                if(file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok("Imagem salva com sucesso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BadRequest("Erro ao fazer upload da imagem");
        
        }

        [HttpPut("update")]
        public async Task<IActionResult> AtualizarProduto(Produto model)
        {
            try
            {
                var produto = await _produtoService.GetById(model.ProdutoId);

                if (produto == null) return BadRequest();

                model.ProdutoId = produto.ProdutoId;

                _mainService.Update<Produto>(model);

                if (await _mainService.saveChangesAsync())
                {
                    return Ok(await _produtoService.GetById(model.ProdutoId));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            try
            {
                var produto = await _produtoService.GetById(id);

                if (produto == null) return BadRequest();

                _mainService.Delete<Produto>(produto);

                if (await _mainService.saveChangesAsync())
                {
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var produtos = await _produtoService.GetAllProducts();

                if (produtos == null)
                    return BadRequest();
                if (produtos.Count == 0)
                    return BadRequest("Não foi possível trazer Produtos, lista vazia");

                return Ok(produtos);
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
                var produto = await _produtoService.GetById(id);

                if (produto == null) return BadRequest("Não foi possível trazer o Produto");

                return Ok(produto);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
