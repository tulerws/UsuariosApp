using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.API.DTOs
{
    /// <summary>
    /// Modelo de dados para a requisição de autenticação de usuários da API.
    /// </summary>
    public class AutenticarUsuarioRequestDTO
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string? Senha { get; set; }
    }
}
