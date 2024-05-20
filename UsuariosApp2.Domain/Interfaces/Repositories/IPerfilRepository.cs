using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp2.Domain.Entities;

namespace UsuariosApp2.Domain.Interfaces.Repositories
{
    public interface IPerfilRepository
    {
        Perfil? GetByNome(string nome);
    }
}
