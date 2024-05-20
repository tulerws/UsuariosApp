using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp2.Domain.Entities;

namespace UsuariosApp2.Domain.Interfaces.Services
{
    /// <summary>
    /// Métodos de serviço de domínio para o Usuário
    /// </summary>
    public interface IUsuarioDomainService
    {
        /// <summary>
        /// Método para criação de conta do usuário
        /// </summary>
        void CriarConta(Usuario usuario);

        /// <summary>
        /// Método para realizar a autenticação da conta do usuário
        /// </summary>
        Usuario? Autenticar(string email, string senha);
    }
}
