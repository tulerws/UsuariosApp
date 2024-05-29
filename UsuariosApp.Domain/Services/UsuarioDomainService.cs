using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributos
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IMessageProducer _messageProducer;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository, IMessageProducer messageProducer)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _messageProducer = messageProducer;
        }

        public void CriarConta(Usuario usuario)
        {
            #region Verificar se já existe um usuário com o email cadastrado

            if (_usuarioRepository.GetByEmail(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            #endregion

            #region Criptografar a senha do usuário

            usuario.Senha = CryptoHelper.SHA256Encrypt(usuario.Senha);

            #endregion

            #region Preenchendoo id, a data e hora de cadastro e o perfil do usuário

            var perfil = _perfilRepository.GetByNome("DEFAULT");
            usuario.PerfilId = perfil.Id;
            usuario.DataHoraCadastro = DateTime.Now;
            usuario.Id = Guid.NewGuid();

            #endregion

            #region Cadastrar o usuário

            _usuarioRepository.Add(usuario);

            #endregion

            #region Enviar mensagem de boas vindas para o usuário

            var mensagem = new Mensagem
            {
                Destinatario = usuario.Email,
                Assunto = "Conta de usuário criada com sucesso!",
                Texto = $"Olá {usuario.Nome},\nParabéns, sua conta foi criada no nosso sistema.\n\nAtt\nCOTI Informática"
            };

            _messageProducer.Send(JsonConvert.SerializeObject(mensagem));

            #endregion
        }

        public Usuario? Autenticar(string email, string senha)
        {
            #region Criptografar a senha informada

            var senhaCriptografada = CryptoHelper.SHA256Encrypt(senha);

            #endregion

            #region Buscar o usuário no banco de dados através do email e da senha

            var usuario = _usuarioRepository.GetByEmailAndSenha(email, senhaCriptografada);

            #endregion

            #region Verificar se o usuário foi encontrado

            if (usuario != null)
                return usuario;
            else
                throw new ApplicationException("Acesso negado. Usuário não encontrado.");

            #endregion
        }

        public Usuario? ObterDados(Guid id)
        {
            #region Buscar o usuário no banco de dados através do ID

            var usuario = _usuarioRepository.GetById(id);

            #endregion

            #region Verificar se o usuário foi encontrado

            if (usuario != null)
                return usuario;
            else
                throw new ApplicationException("Usuário não encontrado.");

            #endregion
        }
    }
}
