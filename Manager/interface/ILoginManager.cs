using DTO;
using System.Threading.Tasks;

namespace Manager
{
    public interface ILoginManager
    {
        Task<LoginDTO> Authentication();        
    }
}
