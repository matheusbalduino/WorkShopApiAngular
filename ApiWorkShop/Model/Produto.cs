using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Model
{
    // Model do Produtos, classe criada para abastecimento dos atributos recuperados do banco
    // Usada para atribuir memória do sistema, armazenando os dados temporariamente para exibição
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string ImagemUrl { get; set; }

        //Relacionamento 1:N
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
