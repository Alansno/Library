using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Models.Dto;
using Biblioteca.Services.Interfaces;

namespace Biblioteca.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _libraryContext;
        public UserService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task<bool> saveUser(UserDto userDto)
        {
            var newUser = new User
            {
                Name = userDto.Name,
                DateBirth = userDto.DateBirth,
            };

            try
            {
                await _libraryContext.Users.AddAsync(newUser);
                await _libraryContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                throw new Exception("Algo salio mal", ex);
            }
        }
    }
}
