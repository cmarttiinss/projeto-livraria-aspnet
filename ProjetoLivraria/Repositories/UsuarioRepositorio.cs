using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using ProjetoLivraria.Models;
using System.Data;

namespace ProjetoLivraria.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;
        private readonly PasswordHasher<UsuarioModel> _passwordHasher;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Connection") ?? string.Empty;
            _passwordHasher = new PasswordHasher<UsuarioModel>();

        }

        public async Task<bool> Cadastrar(UsuarioModel usuario, string senhaPura)
        {
            try
            {   //
                usuario.SenhaHash = _passwordHasher.HashPassword(usuario, senhaPura);
                //
                using var conexao = new MySqlConnection(_connectionString);
                //
                await conexao.OpenAsync();
                //

                var query = "INSERT INTO usuarios (Nome, Email, SenhaHash) Values (@Nome, @Email, #SenhaHash)";
                //
                using var comando = new MySqlCommand(query, conexao);


                // Adicionando os valores dos parâmentros de forma segura aos comandos SQL
                comando.Parameters.AddWithValue("@Nome", usuario.nome);
                comando.Parameters.AddWithValue("@Email", usuario.Email);
                comando.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);

                // Executa o comando no banco e retorna a quantidade de linhas afetadas pela operação
                var linhasAfetadas = await comando.ExecuteNonQueryAsync();
                //
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                // Se der erro, ele vai para aqui e mostrar exatamente o motivo
                throw new Exception($"Erro detalhado no MySQL: {ex.Message}");
            }
        }
        // Método assíncrono para buscar um usuário no banco de dados através do seu e-mail
        public async Task<UsuarioModel?> BuscarEmail(string email) 
        {
            using var conexao = new MySqlConnection(_connectionString);
            await conexao.OpenAsync();

            // Definindo a query SQL para selecionar os dados do usuário com base no e-mail fornecido
            var query = "Select Id, Nome, Email, SenhaHash FROM usuarios WHERE Email = @Email";
            // Cria o comando SQL para executar a consulta
            using var comando = new MySqlCommand(query, conexao);
            //Adiciona o parâmetro de e-mail de forma segura
            comando.Parameters.AddWithValue("@Email", email);

            // Executa a leitura dos dados retornados pelo banco de dados
            using var reader = await comando.ExecuteReaderAsync();
            if (await reader.ReadAsync))
            {
                //Retorna um novo objeto UsuarioModel 
                return new UsuarioModel
                {
                    Id = Convert.ToInt32(reader["Id"]), // Converte com segurança o ID para inteiro
                    nome = reader.GetString("Nome"), // Obtém o nome como string
                    Email = reader.GetString("Email"), // Obtém o e-mail como string
                    SenhaHash = reader.GetString("SenhaHash") // Obtém o hash da senha como string
                };
            }
            // se nenhum usuário for encontrado com esse e-mail, retorna null
            return null;
            
        }
    }
}
