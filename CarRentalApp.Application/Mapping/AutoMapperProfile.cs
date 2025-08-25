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
            CreateMap<CarUpdateDto, Car>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<RentalCreateDto, Rental>();
            CreateMap<RentalUpdateDto, Rental>();

        }
    }
}
