using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleService
    {
        bool AddMaker(VehicleMake maker);
        bool AddModel(VehicleModel model);

        IList<IVehicleModel> GetModels();
        IList<IVehicleMake> GetMakers();

        void SortMakers();
        void SortModels();

        void DeleteMaker(int makerId);
        void DeleteModel(int modelId);

        void UpdateMaker(VehicleMake vehicleMake);
        void UpdateModel(VehicleModel vehicleModel);

        IVehicleModel SerchModel(string modelName);
        IVehicleMake SerchMaker(string makerName);

        IVehicleMake FindMaker(int? id);
        IVehicleModel FindModel(int? id);
  
        }
}
