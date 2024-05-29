using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Logs.Collections;

namespace UsuariosApp.Infra.Logs.Contexts
{
    public class MongoDBContext
    {
        #region Atributos

        private IMongoDatabase? _mongoDatabase;

        #endregion

        #region Construtor

        public MongoDBContext()
        {
            //acessando o >>servidor<< do mongoDB
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017/"));
            var mongoClient = new MongoClient(settings);

            //conectando no banco de dados
            _mongoDatabase = mongoClient.GetDatabase("BD_LogMensageria");
        }

        #endregion

        #region Mapeamento das collections

        public IMongoCollection<LogMensagens> LogMensagens
            => _mongoDatabase.GetCollection<LogMensagens>("LogMensagens");

        #endregion
    }
}
