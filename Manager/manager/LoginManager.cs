using DomainObject.DomainObject;
using DomainObject.Interface;
using DTO;
using Manager.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Manager
{
    public class LoginManager: ILoginManager
    {
        private readonly ILoginDomainObject iLoginDomainObject;
        private readonly IConfiguration _config;

        public LoginManager(IConfiguration config)
        {
            _config = config;
            this.iLoginDomainObject = new LoginDomainObject(_config);
        }

        public async Task<LoginDTO> Authentication(LoginDTO parameters)
        {
            try
            {
                var data =  await this.iLoginDomainObject.Authentication(parameters);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CreateToken(LoginDTO parameters)
        {
            try
            {
                var data = this.iLoginDomainObject.CreateToken(parameters);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
