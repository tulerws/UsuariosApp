using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Logs.Collections
{
    /// <summary>
    /// Modelo de dados para a collection que irá gravar os logs da mensageria no MongoDB
    /// </summary>
    public class LogMensagens
    {
        public Guid? Id { get; set; }
        public DateTime? DataHora { get; set; }
        public string? Status { get; set; }
        public string? Mensagem { get; set; }
    }
}
