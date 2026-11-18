// Importando o namespace que contém atributos de validação dos dados (Data Annotations)
using System.ComponentModel.DataAnnotations;

namespace ProjetoLivraria.Models
{
    public class LoginViewModel
    {
        //Declarando a classe pública que representa os dados enviados no formulário de login
        public class LoginViewModel
        {
            // Tornando o campo 'Email' obrigatório; se tiveer vazio, ele vai exibir uma mensagem personalizada 
            [Required(ErrorMessage = "Informe o e-mail.")]

            //Validando se o texto digitado está no formato padrão de um e-mail válido
            [EmailAddress(ErrorMessage = "E-mail inválido.")]

            //Propriedade pública que armazena o e-mail, inicializada por segurança com string vazia
            public string Email { get; set; } = string.Empty;

            //Torna o campo 'Senha' obrigatório; se estiver vazio, exibe a mensagem personalizada
            [Required(ErrorMessage = "Informe a senha.")]

            // Informando ao ASP.NET Core que este campo é do tipo senha (mascara os caracteres na interface)
            [DataType(DataType.Password)]

            // Propriedade pública que armazena a senha. Inicializada com string vazia 
            public string senha { get; set; } = string.Empty;
        }
    }
}
