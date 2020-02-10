using DomainObject.DomainObject;
using DomainObject.Interface;
using DTO;
using Manager.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager.manager
{
    public class TodoManager : ITodoManager
    {
        private readonly IConfiguration _config;
        private readonly ITodoDomainObject iTodoDomainObject;

        public TodoManager(IConfiguration config)
        {
            _config = config;
            this.iTodoDomainObject = new TodoDomainObject(_config);
        }

        public async Task<TodoDTO[]> GetByFilters(TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoDomainObject.GetByFilters(parameters);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TodoDTO> Save(TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoDomainObject.Save(parameters);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TodoDTO> Update(TodoDTO parameters, bool isUpdate = true)
        {
            try
            {
                var data = await this.iTodoDomainObject.Update(parameters, isUpdate);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
