using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Logs.Collections;
using UsuariosApp.Infra.Logs.Persistance;
using UsuariosApp.Infra.Messages.Helpers;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Consumers
{
    /// <summary>
    /// Classe para ler e processar os itens da fila.
    /// </summary>
    public class MessageConsumer : BackgroundService
    {
        #region Atributos

        private readonly IServiceProvider? _serviceProvider;
        private readonly IConnection? _connection;
        private readonly IModel? _model;

        #endregion

        #region Construtor

        public MessageConsumer(IServiceProvider? serviceProvider)
        {
            _serviceProvider = serviceProvider;

            //abrindo conexão com o servidor do RabbitMQ
            var connectionFactory = new ConnectionFactory { Uri = new Uri(RabbitMQSettings.RabbitUrl) };

            //criando conexão com o servidor
            _connection = connectionFactory.CreateConnection();
            //criando conexão com a fila
            _model = _connection.CreateModel();
            _model?.QueueDeclare(
                queue: RabbitMQSettings.RabbitQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        #endregion

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //criando um objeto para ler cada item contido na fila
            var consumer = new EventingBasicConsumer(_model);

            //fazendo o processamento da fila
            consumer.Received += (sender, args) =>
            {
                //lendo o item que está na fila
                var contentArray = args.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                //deserializando o texto de JSON para objeto
                var mensagem = JsonConvert.DeserializeObject<Mensagem>(contentString);

                //enviando o email..
                using (var scope = _serviceProvider.CreateScope())
                {
                    var logMensagens = new LogMensagens
                    {
                        Id = Guid.NewGuid(),
                        DataHora = DateTime.Now
                    };

                    try
                    {
                        MailHelper.SendMail(mensagem);

                        logMensagens.Status = "SUCESSO";
                        logMensagens.Mensagem = $"Email enviado com sucesso para: {mensagem.Destinatario}";
                    }
                    catch(Exception e)
                    {
                        logMensagens.Status = "ERRO";
                        logMensagens.Mensagem = $"Falha ao enviar email para: {mensagem.Destinatario} -> {e.Message}";
                    }
                    finally
                    {
                        //gravar o log
                        var logMensagensPersistance = new LogMensagensPersistance();
                        logMensagensPersistance.Insert(logMensagens);
                    }

                    _model?.BasicAck(args.DeliveryTag, false);
                }
            };

            //removendo o item da fila
            _model?.BasicConsume(RabbitMQSettings.RabbitQueue, false, consumer);
            return Task.CompletedTask;
        }
    }
}