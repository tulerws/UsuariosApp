using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    /// <summary>
    /// Classe para escrever mensagens na fila
    /// </summary>
    public class MessageProducer : IMessageProducer
    {
        public void Send(string message)
        {
            #region Conectando no servidor do RabbitMQ

            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQSettings.RabbitUrl)
            };

            #endregion

            #region Gravando a mensagem na fila

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare(
                        queue: RabbitMQSettings.RabbitQueue, //nome da fila
                        durable: true, //fila permanente criada no servidor
                        exclusive: false, //fila pode ser compartilhada com outros projetos
                        autoDelete: false, //os itens da fila não são automaticaticamente excluidos / removidos
                        arguments: null
                        );

                    //escrevendo a mensagem na fila
                    model.BasicPublish(
                        exchange: string.Empty,
                        routingKey: RabbitMQSettings.RabbitQueue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(message)   
                        );
                }
            }

            #endregion
        }
    }
}
