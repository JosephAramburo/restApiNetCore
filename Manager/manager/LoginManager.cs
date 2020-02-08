using DomainObject.DomainObject;
using DomainObject.Interface;
using DTO;
using Microsoft.Extensions.Configuration;
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

        public async Task<LoginDTO> Authentication()
        {
            try
            {
                var data =  await this.iLoginDomainObject.Authentication();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
