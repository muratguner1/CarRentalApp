using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalApp.Application.DTOs.Car;
using CarRentalApp.Application.DTOs.Rental;
using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, CarResponseDto>();
            CreateMap<User, UserResponseDto>();
            CreateMap<Rental, RentalResponseDto>();

            CreateMap<CarCreateDto, Car>();
            CreateMap<UserCreateDto, User>();
            CreateMap<RentalCreateDto, Rental>();
            

            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(options => options.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string s && string.IsNullOrEmpty(s))));

            CreateMap<CarUpdateDto, Car>()
                .ForAllMembers(options => options.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    !(srcMember is string s && string.IsNullOrEmpty(s)) &&
                    !(srcMember is int i && i == 0) &&
                    !(srcMember is decimal d && d == 0)));

            CreateMap<RentalUpdateDto, Rental>()
                .ForAllMembers(options => options.Condition((src, dest, srcMember) =>
                    srcMember != null && 
                    !(srcMember is string s && string.IsNullOrEmpty(s)) &&
                    !(srcMember is DateTime dt && dt == DateTime.MinValue)));
        }
    }
}
