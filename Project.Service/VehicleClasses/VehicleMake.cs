using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleMake : IVehicleMake
    {
        [Key]
        public int MakeID { get; set; }
        public string MakerName { get; set; }
        public string MakerDescription { get; set; }
        public IList<IVehicleModel> MakersModels { get; set; }
    }
}
