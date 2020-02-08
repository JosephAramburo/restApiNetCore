using DomainObject.DomainObject;
using DomainObject.Interface;
using DTO;
using System;

namespace Manager
{
    public class LoginManager: ILoginManager
    {
        private readonly ILoginDomainObject iLoginDomainObject;

        public LoginManager()
        {
            this.iLoginDomainObject = new LoginDomainObject();
        }

        public LoginDTO Authentication()
        {
            try
            {
                return this.iLoginDomainObject.Authentication();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
