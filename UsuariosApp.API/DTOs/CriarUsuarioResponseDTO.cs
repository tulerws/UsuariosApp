namespace UsuariosApp.API.DTOs
{
    /// <summary>
    /// Modelo de dados para a resposta de autenticação de usuários da API.
    /// </summary>
    public class CriarUsuarioResponseDTO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}
