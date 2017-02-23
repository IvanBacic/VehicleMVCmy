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
            IList<VehicleModel> models = context.Models.ToList();
            IList<IVehicleModel> Models = Mapper.Map<IList<IVehicleModel>>(models);
            BindModelsWithMaker();
            return Models;
        }

        public IList<IVehicleMake> GetMakers()
        {
            IList<VehicleMake> makers = context.Makers.ToList();
            IList<IVehicleMake> Makers = Mapper.Map<IList<IVehicleMake>>(makers);
            BindMakersWithModels();
            return Makers;
        }

        public void BindModelsWithMaker()
        {
            foreach (IVehicleModel item in context.Models)
            {
                    item.ModelsMaker = FindMaker(item.MakeID);
                
            }
            context.SaveChanges();
        }

        public void BindMakersWithModels()
        {
            foreach (IVehicleMake item in context.Makers)
            {
                item.MakersModels = FindModelsFromMaker(item.MakeID);
            }
            context.SaveChanges();
        }

        public IList<IVehicleMake> GetSortedMakers(string sortOrder,string searchString)
        {
            BindMakersWithModels();

            var Makers = from s in context.Makers
                           select s;
         
            if (!String.IsNullOrEmpty(searchString))
            {
                Makers = Makers.Where(s => s.MakerName.Contains(searchString)
                                       || s.MakerDescription.Contains(searchString));
            }

            if (sortOrder == "name_desc")
            {
                Makers = Makers.OrderByDescending(s => s.MakerName);
            }
            else
            {
                Makers = Makers.OrderBy(s => s.MakerName);
            }

            IList<IVehicleMake> makers = Mapper.Map<IList<IVehicleMake>>(Makers);
       

            return makers;
        }

        public IList<IVehicleModel> GetSortedModels(string sortOrder, string searchString)
        {

            BindModelsWithMaker();

            var Models = from s in context.Models
                         select s;

       

            if (!String.IsNullOrEmpty(searchString))
            {
                Models = Models.Where(s => s.ModelName.Contains(searchString));
                                      
            }

            if (sortOrder == "name_desc")
            {
                Models = Models.OrderByDescending(s => s.ModelName);
            }
            else
            {
                Models = Models.OrderBy(s => s.ModelName);
            }

            IList<IVehicleModel> models = Mapper.Map<IList<IVehicleModel>>(Models);
          
            return models;
        }


        public void DeleteMaker(int? makerID)
        {
            if (makerID.HasValue)
            {      
                    foreach (IVehicleModel model in FindModelsFromMaker(makerID))
                    {
                        context.Models.Remove(context.Models.Find(model.ModelID));
                    }
                
                context.Makers.Remove(context.Makers.Find(makerID));
                context.SaveChanges();
            }
        }

        public void DeleteModel(int? modelId)
        {
            if (modelId.HasValue)
            {
                context.Models.Remove(context.Models.Find(modelId));
                context.SaveChanges();
            }
        }

        public void UpdateMaker(IVehicleMake vehicleMake)
        {
            context.Entry(vehicleMake).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void UpdateModel(IVehicleModel vehicleModel)
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

        public IList<IVehicleModel> FindModelsFromMaker(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            IList<IVehicleModel> findAll=new List<IVehicleModel>();
            foreach (IVehicleModel find in context.Models )
            {
                if (find.MakeID == id)
                {
                    findAll.Add(find);
                }
            }
            return findAll;
        }

    }
}
