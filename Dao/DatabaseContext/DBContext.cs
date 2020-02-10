using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Dao.DatabaseContext
{
    public class DBContext
    {
        private IConfiguration _configuration;
        public readonly IMongoDatabase _database = null;
        public readonly MongoClient _client = null;

        public DBContext(IConfiguration iconfig)
        {
            _configuration = iconfig;
            _client = new MongoClient(_configuration.GetSection("MongoConnection:ConnectionString").Value);
            if (_client != null)
                _database = _client.GetDatabase(_configuration.GetSection("MongoConnection:DatabaseName").Value);
        }

        public MongoClient GetClient()
        {
            return this._client;
        }

        public IMongoDatabase GetDataBase()
        {
            return this._database;
        }

        public IMongoCollection<LoginDTO> UsuariosCollection()
        {
            return _database.GetCollection<LoginDTO>("usuarios");
        }

        public IMongoCollection<TodoDTO> TodoCollection()
        {
            return _database.GetCollection<TodoDTO>("todo");
        }
    }
}
