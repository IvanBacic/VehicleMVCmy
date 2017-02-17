using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.Service;

namespace VehicleMVC
{
    public class MakerViewModel
    {
        [Key]
        public int MakeID { get; set; }
        public string MakerName { get; set; }
        public string MakerDescription { get; set; }
        public IList<IVehicleModel> MakersModels { get; set; }
    }
}