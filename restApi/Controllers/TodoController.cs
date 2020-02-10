using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using restApi.constants;
using Manager.Interface;
using Manager.manager;

namespace restApi.Controllers
{
    [Route(constants.ControllerConstants.Todo)]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoManager iTodoManager;
        private readonly IConfiguration _config;

        public TodoController(IConfiguration config)
        {
            this.iTodoManager = new TodoManager(config);
            _config = config;
        }

        [HttpGet(APIconstants.TodoGetById)]
        public async Task<ActionResult<TodoDTO>> GetById([FromQuery]TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoManager.GetByFilters(parameters);

                if(data.Count().Equals(0))
                    return Conflict(new { message = "No se entontró información" });

                return Ok(data[0]);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet(APIconstants.TodoFilters)]
        public async Task<ActionResult<TodoDTO>> GetByFilters([FromQuery]TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoManager.GetByFilters(parameters);

                if (data.Count().Equals(0))
                    return Conflict(new { message = "No se entontró información" });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }

        [HttpPost(APIconstants.TodoAdd)]
        public async Task<ActionResult<TodoDTO>> Save([FromBody] TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoManager.Save(parameters);

                return Created("",data);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut(APIconstants.TodoUpdate)]
        public async Task<ActionResult<TodoDTO>> Uptade(string id,[FromBody] TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoManager.Update(parameters, true);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }

        [HttpDelete(APIconstants.TodoUpdate)]
        public async Task<ActionResult<TodoDTO>> Delete(string id)
        {
            try
            {
                var data = await this.iTodoManager.Update(new TodoDTO
                {
                    _id = id,
                    Status = false
                }, false);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message.ToString() });
            }
        }
    }
}