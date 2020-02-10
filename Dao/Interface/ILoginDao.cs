using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Dao.Interface
{
    public interface ILoginDao
    {
        Task<LoginDTO> Authentication(LoginDTO parameters);
        string CreateToken(LoginDTO parameters);
    }
}
