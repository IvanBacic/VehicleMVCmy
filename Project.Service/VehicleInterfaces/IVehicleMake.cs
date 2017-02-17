using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleMake
    {
        [Key]
        int MakeID { get; set; }
        string MakerName { get; set; }
        string MakerDescription { get; set; }
        IList<IVehicleModel> MakersModels { get; set; }
    }
}
