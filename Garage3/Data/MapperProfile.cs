using AutoMapper;
using Garage3.Models;
using Garage3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDetailsViewModel>();
            CreateMap<User, UserAddViewModel>().ReverseMap();
            CreateMap<User, UserListViewModel>()
                .ForMember(
                       dest => dest.VehicleCount,
                       from => from.MapFrom(s => s.Vehicles.Count));

            CreateMap<ParkingPlace, ParkingPlaceListViewModel>()
                .ForMember(
                dest => dest.Username,
                from => from.MapFrom(s => s.Vehicle.User.FullName))
                .ForMember(
                dest => dest.VehicleType,
                from => from.MapFrom(s => s.Vehicle.VehicleType.Name));

            CreateMap<VehicleAddViewModel, Vehicle>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.VehicleType, opt => opt.Ignore());




            CreateMap<ParkingPlace, ReceiptViewModel>()
                .ForMember(
                dest => dest.Username,
                from => from.MapFrom(s => s.Vehicle.User.FullName));

            CreateMap<VehicleType, VehicleTypeAddViewModel>().ReverseMap();
        }
    }
}
