using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleModel
    {
        [Key]
        int ModelID { get; set; }
        string ModelName { get; set; }
        int MakeID { get; set; }
        IVehicleMake ModelsMaker { get; set; }
    }
}
