using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace DomainObject.Interface
{
    public interface ILoginDomainObject
    {
        Task<LoginDTO> Authentication(LoginDTO parameters);
        string CreateToken(LoginDTO parameters);
    }
}
