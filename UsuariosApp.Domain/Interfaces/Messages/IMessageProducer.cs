using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Interfaces.Messages
{
    /// <summary>
    /// Interface para gravarmos mensagens na fila do RabbitMQ
    /// </summary>
    public interface IMessageProducer
    {
        /// <summary>
        /// Método para gravar conteúdo na fila
        /// </summary>
        void Send(string message);
    }
}
