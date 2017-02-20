﻿using AutoMapper;
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
        }

        public void BindMakersWithModels()
        {
            foreach (IVehicleMake item in context.Makers)
            {
                item.MakersModels = FindModelsFromMaker(item.MakeID);
            }
        }

        public void SortMakers(string sortOrder)
        {

            var students = from s in context.Makers
                           select s;

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.MakerName);
                    break;
                default:
                    students = students.OrderBy(s => s.MakerName);
                    break;
            }

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
