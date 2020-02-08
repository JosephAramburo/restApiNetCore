using System;
using System.Collections.Generic;
using System.Text;
using Dao.Interface;
using DTO;

namespace Dao
{
    public class LoginDao: ILoginDao
    {
        #region Auth
        public LoginDTO Authentication()
        {
            LoginDTO loginDTO = null;
            try
            {
                return loginDTO = new LoginDTO
                {
                    name = "Joseph"
                };
            }catch(Exception ex)
            {
                throw new Exception( ex.Message.ToString());
            }
        }
        #endregion
    }
}
