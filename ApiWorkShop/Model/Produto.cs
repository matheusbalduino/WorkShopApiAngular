using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Model
{
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
