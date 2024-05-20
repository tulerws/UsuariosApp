using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp2.Domain.Helpers;
using UsuariosApp2.Domain.Entities;
using UsuariosApp2.Domain.Interfaces.Repositories;
using UsuariosApp2.Domain.Interfaces.Services;

namespace UsuariosApp2.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository  _perfilRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public void CriarConta(Usuario usuario)
        {
            #region Verificar se já existe um usuário cadastrado com este email
            
            if (_usuarioRepository.GetByEmail(usuario.Email) != null)
            {
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");
            }

            #endregion

            //criptografando a senha do usuário
            usuario.Senha = CryptoHelper.SHA256Encrypt(usuario.Senha);

            #region Preenchendo o id, a data e hora de cadastro e o perfil do usuário 

            var perfil = _perfilRepository.GetByNome("DEFAULT");
            usuario.PerfilId = perfil.Id;
            usuario.DataHoraCadastro = DateTime.Now;
            usuario.Id = Guid.NewGuid();

            #endregion

            _usuarioRepository.Add(usuario);
        }

        public Usuario? Autenticar(string email, string senha)
        {
            //criptografar a senha informada
            var senhaCrpta = CryptoHelper.SHA256Encrypt(senha);

            //buscar no banco o usuário através do email e da senha criptografada
            var usuario = _usuarioRepository.GetByEmailAndSenha(email, senhaCrpta);


            if (usuario != null)
                return usuario;
            else
                throw new ApplicationException("Acesso negado. Usuário não encontrado.");
        }
    }
}
