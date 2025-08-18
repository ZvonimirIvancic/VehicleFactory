    using AutoMapper;
    using Service.Models;
    using Service.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Service.Mapper
    {
        public class VehicleFactoryAutoMapperProfile : Profile
        {
            public VehicleFactoryAutoMapperProfile()
            {

                CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();

                CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
            }
        }
    }
