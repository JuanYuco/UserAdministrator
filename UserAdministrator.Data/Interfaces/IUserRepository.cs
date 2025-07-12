using UserAdministrator.Data.Models;

namespace UserAdministrator.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task SaveAsync(User user);
        Task DeleteAsync(int id);
    }
}
