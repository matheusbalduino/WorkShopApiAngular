using ApiWorkShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Service
{
    // Declaração da Interface para uso na Classe Main Service.
    public interface IMainService
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> saveChangesAsync();
    }
    public class MainService : IMainService
    {
        // Variável do tipo DbContext para uso do EntityFrameWork
        private MyDbContext _context; 

        // Construtor para iniciar a classe com o DbContext, 
        // Classe recebe como Parâmetro o DbContext que foi construido com as tabelas 
        // Com a biblioteca do EntityFramework
        public MainService(MyDbContext context) 
        {
            _context = context;
        }

        // A funções a seguir são construidas de forma genérica,
        // dessa Forma é possivel usar para todos os models.

        // Função de adicionar um objeto Model de uma tabela.
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        // Função Para Excluir o um objeto model da tabela.
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        // Função de atualizar um objeto Model de uma tabela.
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        // Função para Salvar e persistir os dados no Database.
        public async Task<bool> saveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
