using Application.DTOs.RecipeDto;
using Application.DTOs.UserDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();

            CreateMap<Recipe, CreateRecipeDto>().ReverseMap();
            CreateMap<Recipe, UpdateRecipeDto>().ReverseMap();
            CreateMap<Recipe, GetRecipeDto>().ReverseMap();
            CreateMap<Recipe, DeleteRecipe>().ReverseMap();

            // CreateMap<User, GetUserDto>().ReverseMap();

        }
    }
}