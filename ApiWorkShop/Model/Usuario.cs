using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWorkShop.Model
{
    // Model do usuário, classe criada para abastecimento dos atributos recuperados do banco
    // Usada para atribuir memória do sistema, armazenando os dados temporariamente para exibição
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        //Relacionamento 1:N
        public List<Produto> Produtos { get; set; } // Cria uma lista do tipo Produtos para exibição dos produtos do Usuário.
    }
}
