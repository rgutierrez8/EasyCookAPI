using EasyCookAPI.Core;
using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EasyCookAPI.Core.Services
{
    public class UserService : Repository<User>, IUserService
    {
        private IMapper _mapper;
        private readonly IHelper _helper;
        private readonly IRecipeService _recipeService;
        public UserService(EasyCookContext context, IMapper mapper, IRecipeService recipeService, IHelper helper) : base(context)
        {
            _mapper = mapper;
            _recipeService = recipeService;
            _helper = helper;
        }

        public UserDTO? GetUser(int id)
        {
            var data = FindByCondition(user => user.Id == id).FirstOrDefault();
            var dataRecipes = _recipeService.GetRecipesByUser(id);

            return data != null ? _mapper.MapUserToUserDTO(data, dataRecipes) : null;
        }
        public int GetId(string username)
        {
            var data = FindByCondition(user => user.Username == username).FirstOrDefault();

            return data != null ? data.Id : -1;
        }

        public void NewUser(NewUserDTO newUser)
        {
            Create(_mapper.MapNewUserToUser(newUser));
            Save();
        }
        public LogedUserDTO? Login(UserLoginDTO loginDTO)
        {
            var data = FindByCondition(source => source.Username == loginDTO.Username && source.Pass == _helper.EncryptPassSha25(loginDTO.Password)).FirstOrDefault();

            if (data != null)
            {
                return _mapper.MapUserTOLogedUserDTO(data);
            }

            return null;
        }
        public void UpdatePass(UpdateUserPassDTO update)
        {
            var data = FindByCondition(source => source.Username == update.UserName && source.Email == update.Email).FirstOrDefault();

            if(data != null)
            {
                data.Pass = _helper.EncryptPassSha25(update.Password);
                Update(data);
                Save();
            }
        }
    }
}
