using System;
using System.Threading.Tasks;
using Dao;
using Dao.Interface;
using DomainObject.Interface;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DomainObject.DomainObject
{
    public class LoginDomainObject: ILoginDomainObject
    {
        private readonly ILoginDao iLoginDao;
        private readonly IConfiguration _config;

        public LoginDomainObject(IConfiguration config)
        {
            _config = config;
            this.iLoginDao = new LoginDao(_config);
        }

        public async Task<LoginDTO> Authentication(LoginDTO parameters)
        {
            try
            {
                var data = await this.iLoginDao.Authentication(parameters);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
