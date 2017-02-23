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

        void BindModelsWithMaker();
        void BindMakersWithModels();
        IList<IVehicleMake> GetSortedMakers(string sortOrder,string searchString);
        IList<IVehicleModel> GetSortedModels(string sortOrder, string searchString);

        void DeleteMaker(int? makerId);
        void DeleteModel(int? modelId);

        void UpdateMaker(IVehicleMake vehicleMake);
        void UpdateModel(IVehicleModel vehicleModel);

        IVehicleModel SerchModel(string modelName);
        IVehicleMake SerchMaker(string makerName);

        IVehicleMake FindMaker(int? id);
        IVehicleModel FindModel(int? id);

        IList<IVehicleModel> FindModelsFromMaker(int? id);
        }
}
