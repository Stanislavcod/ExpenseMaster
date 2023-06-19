﻿using AutoMapper;
using ExpenseMaster.BusinessLogic.AbstractDto;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, User>()
        .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
        .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
        .AfterMap((src, dest) =>
        {
            PasswordHasher.CreatePasswordHash(src.Password, out byte[] passwordHash, out byte[] passwordSalt);
            dest.PasswordHash = passwordHash;
            dest.PasswordSalt = passwordSalt;
        })
        .ReverseMap()
        .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, UserDto>()
        .ReverseMap();

            CreateMap<Budget, CreateBudgetDto>().ReverseMap();

            CreateMap<Budget, UpdateBudgetDto>().ReverseMap();
            CreateMap<Budget, BudgetDto>().ReverseMap();

        }
    }
}
