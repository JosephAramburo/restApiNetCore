using DTO;

namespace Dao.Interface
{
    public interface ILoginDao
    {
        LoginDTO Authentication();
    }
}
