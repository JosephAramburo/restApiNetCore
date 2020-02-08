using System;
using Dao;
using Dao.Interface;
using DomainObject.Interface;
using DTO;

namespace DomainObject.DomainObject
{
    public class LoginDomainObject: ILoginDomainObject
    {
        private readonly ILoginDao iLoginDao;

        public LoginDomainObject()
        {
            this.iLoginDao = new LoginDao();
        }

        public LoginDTO Authentication()
        {
            try
            {
                return this.iLoginDao.Authentication();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
