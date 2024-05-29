using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Messages.Settings
{
    /// <summary>
    /// Classe para armazenar as configurações de conexão
    /// com o servidor da mensageria (RabbitMQ)
    /// </summary>
    public class RabbitMQSettings
    {
        /// <summary>
        /// Caminho do servidor do RabbitMQ
        /// </summary>
        public static string RabbitUrl => "amqps://sqshoxpi:YDvQM2gm-5AV0MO5e9Sbo0AlSiOMzj8J@shark.rmq.cloudamqp.com/sqshoxpi";

        /// <summary>
        /// Nome da fila no RabbitMQ
        /// </summary>
        public static string RabbitQueue => "emails_usuario";
    }
}
