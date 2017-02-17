using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.Service;

namespace VehicleMVC
{
    public class ModelViewModel
    {
        [Key]
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int MakeID { get; set; }
        public IVehicleMake ModelsMaker { get; set; }
    }
}