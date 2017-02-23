using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleDBContext : DbContext
    {
        public IDbSet<VehicleMake> Makers { get; set; }
        public IDbSet<VehicleModel> Models { get; set; }
       
    }
}
