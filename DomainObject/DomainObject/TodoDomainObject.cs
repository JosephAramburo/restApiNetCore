using Dao.DAO;
using Dao.Interface;
using DomainObject.Interface;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DomainObject
{
    public class TodoDomainObject: ITodoDomainObject
    {
        private readonly ITodoDao iTodoDao;
        private readonly IConfiguration _config;

        public TodoDomainObject(IConfiguration config)
        {
            iTodoDao = new TodoDao(config);
            _config = config;
        }

        public async Task<TodoDTO[]> GetByFilters(TodoDTO parameters)
        {
            try
            {
                var data = await this.iTodoDao.GetByFilters(parameters);
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
                var data = await this.iTodoDao.Save(parameters);
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
                var data = await this.iTodoDao.Update(parameters, isUpdate);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
