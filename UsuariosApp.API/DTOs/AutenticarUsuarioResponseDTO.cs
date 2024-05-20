namespace UsuariosApp.API.DTOs
{
    /// <summary>
    /// Modelo de dados para a resposta de autenticação de usuários da API.
    /// </summary>
    public class AutenticarUsuarioResponseDTO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
    }
}
