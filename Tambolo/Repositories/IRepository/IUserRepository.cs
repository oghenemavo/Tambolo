using Tambolo.Dtos;

namespace Tambolo.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<bool> CreateAccountAsync(UserCreateDto model);
    }
}
