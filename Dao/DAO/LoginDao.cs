using System;
using Dao.Interface;
using DTO;
using Dao.DatabaseContext;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

                var complexFilter = Builders<LoginDTO>.Filter.And(filters);

                using (var cursor = await _context.UsuariosCollection().FindAsync<LoginDTO>(complexFilter))
                {
                    while (await cursor.MoveNextAsync() && loginDTO == null)
                    {
                        var batch = cursor.Current;
                        foreach (var document in batch)
                        {                            
                            if (BCrypt.Net.BCrypt.Verify(parameters.password, document.password))
                                loginDTO = document;
                            else
                                throw new Exception("Password incorrecto");
                        }
                    }
                }

                loginDTO.password = "";

                return loginDTO;
            }catch(Exception ex)
            {
                throw new Exception( ex.Message.ToString());
            }
        }
        #endregion

        #region Create Token
        public string CreateToken(LoginDTO parameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._config.GetSection("Jwt:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, parameters._id),
                    new Claim(ClaimTypes.Name, parameters.nombreCompleto),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
        #endregion
    }
}
