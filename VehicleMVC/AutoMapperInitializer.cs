using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleMVC
{
    public class AutoMapperInitializer 
    {
        public static void AutomapperMappings()
        {
            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<IVehicleModel, ModelViewModel>().ReverseMap();
                config.CreateMap<IVehicleMake, MakerViewModel>().ReverseMap();
            });
        }


    }
}