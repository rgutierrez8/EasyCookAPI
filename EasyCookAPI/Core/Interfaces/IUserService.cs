using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IUserService
    {
        public UserDTO? GetUser(int id);
        public int GetId(string username);
        LogedUserDTO? Login(UserLoginDTO loginDTO);
        void NewUser(NewUserDTO newUser);
    }
}
