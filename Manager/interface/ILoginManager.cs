using DTO;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface ILoginManager
    {
        Task<LoginDTO> Authentication(LoginDTO parameters);
        string CreateToken(LoginDTO parameters);
    }
}
