using Biblioteca.Models.Dto;

namespace Biblioteca.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> saveUser(UserDto userDto);
    }
}
