namespace UsuariosApp.API.DTOs
{
    /// <summary>
    /// Modelo de dados para a resposta de consulta de dados do usuário.
    /// </summary>
    public class ObterDadosUsuarioResponseDTO
    {
        public Guid? Id { get; set; }
        public string? NomeUsuario { get; set; }
        public string? Email { get; set; }
        public Guid? PerfilId { get; set; }
        public string? NomePerfil { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
    }
}
