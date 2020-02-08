using DTO;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Dao.Interface
{
    public interface ILoginDao
    {
        Task<LoginDTO> Authentication(LoginDTO parameters);
    }
}
