using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Interface
{
    public interface ITodoDao
    {
        Task<TodoDTO[]> GetByFilters(TodoDTO parameters);
        Task<TodoDTO> Save(TodoDTO parameters);
        Task<TodoDTO> Update(TodoDTO parameters, bool isUpdate = true);
    }
}
