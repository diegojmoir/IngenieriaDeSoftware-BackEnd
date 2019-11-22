using System;
using AutoMapper;
using RestauranteAPI.Models.Dto;
using Firebase.Database;

namespace RestauranteAPI.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //source, destination
            //Database user object to Data transfer object User model
            CreateMap<FirebaseObject<User>, UserDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Object.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Object.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Object.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Object.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Object.Password));


            CreateMap<User,UserDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ID.ToString()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ID.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.StartingDate, opt => opt.MapFrom(src => src.StartingDate))
                .ForMember(dest => dest.EndingDate, opt => opt.MapFrom(src => src.EndingDate))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src =>Guid.Parse( src.Key)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.StartingDate, opt => opt.MapFrom(src => src.StartingDate))
                .ForMember(dest => dest.EndingDate, opt => opt.MapFrom(src => src.EndingDate))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => Guid.Parse(src.ID.ToString())))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        }
    }
}
