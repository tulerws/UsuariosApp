using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp2.Domain.Entities;

namespace UsuariosApp2.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        public Usuario? GetByEmail(string email);
        public Usuario? GetByEmailAndSenha(string email, string senha);
    }
}
