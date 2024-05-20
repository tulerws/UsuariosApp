using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp2.Domain.Entities
{
    public class Usuario
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public Guid PerfilId { get; set; }

        #endregion

        #region Relacionamentos

        public Perfil? Perfil { get; set; }

        #endregion
    }
}
