﻿using AutoMapper;
using RestauranteAPI.Models.Dto;
using Firebase.Database;

namespace RestauranteAPI.Models.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //source, destination
            //Database user object to Data transfer object User model
            CreateMap<FirebaseObject<User>, UserDto>()
                .ForMember(dest=>dest.Key,opt=>opt.MapFrom(src=>src.Key))
                .ForMember(dest=>dest.FirstName,opt=>opt.MapFrom(src=>src.Object.FirstName))
                .ForMember(dest=>dest.LastName,opt=>opt.MapFrom(src=>src.Object.LastName))
                .ForMember(dest=>dest.Email,opt=>opt.MapFrom(src=>src.Object.Email))
                .ForMember(dest=>dest.Username,opt=>opt.MapFrom(src=>src.Object.Username))
                .ForMember(dest=>dest.Password,opt=>opt.MapFrom(src=>src.Object.Password));
            CreateMap<FirebaseObject<Product>, ProductDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Object.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Object.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Object.Price))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Object.IsAvailable))
                .ForMember(dest => dest.StartingTime, opt => opt.MapFrom(src => src.Object.StartingTime))
                .ForMember(dest => dest.EndingTime, opt => opt.MapFrom(src => src.Object.EndingTime))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Object.Image))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Object.Category));



                }
    }
}
