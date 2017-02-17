using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleMVC
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IVehicleService>().To<VehicleService>();
        }
    }
}   