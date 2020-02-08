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

        public DBContext(IConfiguration iconfig)
        {
            _configuration = iconfig;
            var client = new MongoClient(_configuration.GetSection("MongoConnection:ConnectionString").Value);
            if (client != null)
                _database = client.GetDatabase(_configuration.GetSection("MongoConnection:DatabaseName").Value);
        }
        
        public IMongoCollection<LoginDTO> UsuariosCollection()
        {
            return _database.GetCollection<LoginDTO>("usuarios");
        }
    }
}
