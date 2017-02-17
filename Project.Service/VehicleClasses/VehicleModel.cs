using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleModel: IVehicleModel
    {
        [Key]
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int MakeID { get; set; }
        public IVehicleMake ModelsMaker { get; set; }
    }
}
