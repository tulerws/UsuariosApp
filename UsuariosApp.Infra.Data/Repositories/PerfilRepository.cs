using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        public Perfil? GetByNome(string nome)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Perfil>()
                    .Where(p => p.Nome.Equals(nome))
                    .FirstOrDefault();
            }
        }
    }
}
