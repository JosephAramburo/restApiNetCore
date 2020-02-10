using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Manager;
using Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using restApi.constants;

namespace restApi.Controllers
{
    [Route(constants.ControllerConstants.Authentication)]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public class AuthenticationController : ControllerBase
    {
        private ILoginManager iLoginManager;
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
            this.iLoginManager = new LoginManager(_config);
        }

        [HttpPost(APIconstants.login)]        
        public async Task<ActionResult<LoginResponseDTO>> Authentication([FromBody] LoginDTO parameters)
        {
            try
            {
                var data = await this.iLoginManager.Authentication(parameters);
                if (data == null)
                {
                    return Conflict(new { message = "Problemas al consultar datos" });
                }

                var token = this.iLoginManager.CreateToken(data);

                return Ok(new LoginResponseDTO {
                    User = data,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }
    }
}