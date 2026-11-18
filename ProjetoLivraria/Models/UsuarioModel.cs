namespace ProjetoLivraria.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
    }
}
