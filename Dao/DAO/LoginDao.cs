using System;
using Dao.Interface;
using DTO;
using Dao.DatabaseContext;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Dao
{
    public class LoginDao: ILoginDao
    {
        public readonly DBContext _context = null;
        public readonly string NameCollection = "usuarios";
        private readonly IConfiguration _config;

        public LoginDao(IConfiguration config)
        {
            _config = config;
            _context = new DBContext(_config);
        }


        #region Auth
        public async Task<LoginDTO> Authentication(LoginDTO parameters)
        {
            LoginDTO loginDTO = null;
            var filters = new List<FilterDefinition<LoginDTO>>();
            try
            {
                if(!parameters.email.Equals(""))
                {
                    var filter = Builders<LoginDTO>.Filter.Eq("email", parameters.email);
                    filters.Add(filter);
                }

                if (!parameters.password.Equals(""))
                {
                    var filter = Builders<LoginDTO>.Filter.Eq("password", parameters.password);
                    filters.Add(filter);
                }

                var complexFilter = Builders<LoginDTO>.Filter.And(filters);

                using (var cursor = await _context.UsuariosCollection().FindAsync<LoginDTO>(complexFilter))
                {
                    while (await cursor.MoveNextAsync() && loginDTO == null)
                    {
                        var batch = cursor.Current;
                        foreach (var document in batch)
                        {
                            loginDTO = document;
                        }
                    }
                }

                return loginDTO;
            }catch(Exception ex)
            {
                throw new Exception( ex.Message.ToString());
            }
        }
        #endregion
    }
}
