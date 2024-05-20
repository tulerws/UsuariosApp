using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp2.Domain.Entities;
using UsuariosApp2.Domain.Interfaces.Repositories;
using UsuariosApp2.Infra.Data.Contexts;

namespace UsuariosApp2.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Add(Usuario usuario)
        {
            using(var context = new DataContext())
            {
                context.Add(usuario);
                context.SaveChanges();
            }
        }

        public Usuario? GetByEmail(string email)
        {
            using(var context = new DataContext())
            {
                return context.Set<Usuario>()
                    .Where(u => u.Email.Equals(email))
                    .FirstOrDefault();
            }
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            using(var context = new DataContext())
            {
                return context.Set<Usuario>()
                    .Where(u => u.Email.Equals(email) && u.Senha.Equals(senha))
                    .FirstOrDefault();
            }
        }
    }
}
