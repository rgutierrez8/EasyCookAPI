﻿using EasyCookAPI.Core;
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
        private readonly IRecipeService _recipeService;
        public UserService(EasyCookContext context, IMapper mapper, IRecipeService recipeService) : base(context)
        {
            _mapper = mapper;
            _recipeService = recipeService;
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
    }
}
