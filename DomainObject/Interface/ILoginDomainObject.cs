using DTO;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DomainObject.Interface
{
    public interface ILoginDomainObject
    {
        Task<LoginDTO> Authentication(LoginDTO parameters);
    }
}
