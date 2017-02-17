using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Service
{
   public class VehicleService : IVehicleService
    {
        private readonly VehicleDBContext context = new VehicleDBContext();

        public bool AddMaker(VehicleMake maker)
        {
            context.Makers.Add(maker);
            return Convert.ToBoolean(context.SaveChanges());
        }

        public bool AddModel(VehicleModel model)
        {
            context.Models.Add(model);
            return Convert.ToBoolean(context.SaveChanges());
        }

        public IList<IVehicleModel> GetModels()
        {
            IList <VehicleModel> models = context.Models.ToList();
            IList<IVehicleModel> Models = Mapper.Map<IList<IVehicleModel>>(models);
            return Models;
        }

        public IList<IVehicleMake> GetMakers()
        {
            IList<VehicleMake> makers = context.Makers.ToList();
            IList<IVehicleMake> Makers = Mapper.Map<IList<IVehicleMake>>(makers);
            return Makers;
        }

        public void SortMakers()
        {
            context.Makers.OrderByDescending(sortby => sortby.MakerName);
            context.SaveChanges();
        }

        public void SortModels()
        {
            context.Models.OrderByDescending(sortby => sortby.ModelName);
            context.SaveChanges();
        }

        public void DeleteMaker(int makerId)
        {
            context.Makers.Remove(context.Makers.Find(makerId));
            context.SaveChanges();
        }

        public void DeleteModel(int modelId)
        {
            context.Models.Remove(context.Models.Find(modelId));
            context.SaveChanges();
        }

        public void UpdateMaker(VehicleMake vehicleMake)
        {
            context.Entry(vehicleMake).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void UpdateModel(VehicleModel vehicleModel)
        {
            context.Entry(vehicleModel).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IVehicleModel SerchModel(string modelName)
        {
            return context.Models.Find(modelName);
        }

        public IVehicleMake SerchMaker(string makerName)
        {
            return context.Makers.Find(makerName);
        }

        public IVehicleMake FindMaker(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            return context.Makers.Find(id);
        }

        public IVehicleModel FindModel(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            return context.Models.Find(id);
        }
    }
}
