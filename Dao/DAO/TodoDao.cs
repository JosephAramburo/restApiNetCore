using Dao.DatabaseContext;
using Dao.Interface;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.DAO
{
    public class TodoDao : ITodoDao
    {
        public readonly DBContext _context = null;
        public readonly string NameCollection = "todo";
        private IConfiguration config;

        public TodoDao(IConfiguration config)
        {
            this.config = config;
            _context = new DBContext(config);
        }

        public async Task<TodoDTO> GetById(string _id)
        {
            try
            {
                if (_id == null || _id.Equals(""))
                {
                    throw new Exception("Falta campo Descripcion");
                }

                var filter = Builders<TodoDTO>.Filter.Eq("_id", _id);
                var complexFilter   = Builders<TodoDTO>.Filter.And(filter);
                var data            = await _context.TodoCollection().Find(complexFilter).ToListAsync();
                var dataArray       = data.ToArray();


                return dataArray.Length > 0 ? dataArray[0] : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<TodoPaginationDTO> GetByFilters(TodoPaginationDTO parameters)
        {
            List<TodoDTO> listTodo  = new List<TodoDTO>();
            int limitForPage        = int.Parse(this.config.GetSection("MongoConnection:LimitForPage").Value);
            long countDocument      = 0;
            int skip                = (parameters.page - 1) * limitForPage;
            try
            {
                var filters = this.SetFilterQuery(parameters.filters);

                if (filters.Count == 0)
                {
                    countDocument   = this._context.TodoCollection().CountDocuments(new BsonDocument());
                    var getAll      = await _context.TodoCollection().Find(Builders<TodoDTO>.Filter.Empty).Skip(skip).Limit(limitForPage).ToListAsync();
                    return new TodoPaginationDTO
                    {
                        count   = countDocument,
                        data    = getAll.ToArray(),
                        page    = parameters.page
                    };
                }

                var complexFilter   = Builders<TodoDTO>.Filter.And(filters);
                countDocument       = this._context.TodoCollection().CountDocuments(complexFilter);
                var data            = await _context.TodoCollection().Find(complexFilter).Skip(skip).Limit(limitForPage).ToListAsync();

                return new TodoPaginationDTO
                {
                    count   = countDocument,
                    data    = data.ToArray(),
                    page    = parameters.page
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<TodoDTO> Save(TodoDTO parameters)
        {
            try
            {
                this.ValidateRequiredFields(parameters);

                parameters.CreatedAt = DateTime.Now;

                //await this._context.GetDataBase().CreateCollectionAsync(this.NameCollection);

                await this._context.TodoCollection().InsertOneAsync(parameters);

                return parameters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }      

        public async Task<TodoDTO> Update(TodoDTO parameters, bool isUpdate = true)
        {
            var filters = new List<FilterDefinition<TodoDTO>>();
            try
            {
                if(isUpdate)
                    this.ValidateRequiredFields(parameters);

                filters = this.SetFilters(parameters, true);
                var complexFilterUp = Builders<TodoDTO>.Filter.And(filters);

                var update = Builders<TodoDTO>.Update.Set("Status", parameters.Status)
                    .Set("UpdatedAt", DateTime.Now);

                if (isUpdate)
                {
                    update.Set("Description", parameters.Description)
                    .Set("File", parameters.File)
                    .Set("TypeFile", parameters.TypeFile);
                }

                var complexFilter = Builders<TodoDTO>.Filter.And(filters);

                await this._context.TodoCollection().FindOneAndUpdateAsync(complexFilterUp, update, new FindOneAndUpdateOptions<TodoDTO> { ReturnDocument =  ReturnDocument.Before });

                return parameters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private List<FilterDefinition<TodoDTO>> SetFilters(TodoDTO parameters, bool byId = false)
        {
            var filters = new List<FilterDefinition<TodoDTO>>();
            if (byId)
            {
                var filter = Builders<TodoDTO>.Filter.Eq("_id", parameters._id);
                filters.Add(filter);
                return filters;
            }

            return filters;
        }

        private List<FilterDefinition<TodoDTO>> SetFilterQuery(TodoDTO parameters)
        {
            var filters = new List<FilterDefinition<TodoDTO>>();

            if ( parameters.Description != null && !parameters.Description.Equals(""))
            {
                var filter = Builders<TodoDTO>.Filter.Regex("Description", new BsonRegularExpression(parameters.Description, "i"));
                filters.Add(filter);
            }

            if (parameters._id != null && !parameters._id.Equals(""))
            {
                var filter = Builders<TodoDTO>.Filter.Regex("_id", new BsonRegularExpression(parameters._id, "i"));
                filters.Add(filter);
            }

            if (parameters.Status != null && !parameters.Status.Equals(""))
            {
                var filter = Builders<TodoDTO>.Filter.Eq("Status", parameters.Status);
                filters.Add(filter);
            }

            return filters;
        }

        private void ValidateRequiredFields(TodoDTO parameters)
        {
            if (parameters.Description.Equals(""))
            {
                throw new Exception("Falta campo Descripcion");
            }

            if (parameters.Status.Equals(""))
            {
                throw new Exception("Falta campo Status");
            }

            if (parameters.File.Equals(""))
            {
                throw new Exception("Falta campo File");
            }

            if (parameters.TypeFile.Equals(""))
            {
                throw new Exception("Falta campo TypeFile");
            }
        }

    }
}
