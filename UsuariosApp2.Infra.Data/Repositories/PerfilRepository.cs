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
    public class PerfilRepository : IPerfilRepository
    {
        public Perfil? GetByNome(string nome)
        {
            using (var context = new DataContext())
            {
                return context.Set<Perfil>()
                    .Where(p => p.Nome.Equals(nome))
                    .FirstOrDefault();
            }
        }
    }
}
