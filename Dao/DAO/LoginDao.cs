using System;
using Dao.Interface;
using DTO;
using Dao.DatabaseContext;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System.Threading.Tasks;

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
        public async Task<LoginDTO> Authentication()
        {
            LoginDTO loginDTO = null;
            try
            {

                using (var cursor = await _context.UsuariosCollection().FindAsync<LoginDTO>(new BsonDocument()))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        var batch = cursor.Current;
                        foreach (var document in batch)
                        {
                            // process document
                            loginDTO = document;
                        }
                    }
                }

                //var data = _context.UsuariosCollection().FindSync<LoginDTO>;
                //var data = _context.UsuariosCollection().Equals(new { email = "deyvid1914@gmail.com" });

                //var data = _context.UsuariosCollection().

                //using (IAsyncCursor<BsonDocument> cursor = await _context.GetCollectionByName("dsdss").FindAsync(new BsonDocument()))
                //{
                //    while (await cursor.MoveNextAsync())
                //    {
                //        IEnumerable<BsonDocument> batch = cursor.Current;
                //        foreach (BsonDocument document in batch)
                //        {
                //            Console.WriteLine(document);
                //            Console.WriteLine();
                //        }
                //    }
                //}

                return loginDTO;
            }catch(Exception ex)
            {
                throw new Exception( ex.Message.ToString());
            }
        }
        #endregion
    }
}
