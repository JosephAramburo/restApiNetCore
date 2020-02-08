using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restApi.constants;

namespace restApi.Controllers
{
    [Route(constants.ControllerConstants.Authentication)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ILoginManager iLoginManager;

        public AuthenticationController()
        {
            this.iLoginManager = new LoginManager();
        }

        [HttpGet(APIconstants.login)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<LoginDTO> Authentication()
        {
            try
            {
                var data = this.iLoginManager.Authentication();
                if (data == null)
                {
                    return Conflict(new { message = "Problemas al consultar datos" });
                }

                return data;
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }
    }
}