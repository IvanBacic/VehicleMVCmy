using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service;
using VehicleMVC;
using System.Net;
using System.Data;
using PagedList;
namespace VehicleMVC.Controllers
{
    public class ModelController : Controller
    {
        public ModelController(IVehicleService service)
        {
            this.Service = service;
        }

        private readonly IVehicleService Service;


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null) { page = 1; }
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;

            IList<IVehicleModel> model = Service.GetSortedModels(sortOrder,searchString);
            IList<ModelViewModel> Models = AutoMapper.Mapper.Map<IList<ModelViewModel>>(model);

            return View(Models.ToPagedList(page ?? 1,3));
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           IVehicleModel model =Service.FindModel(id);
           ModelViewModel Model = AutoMapper.Mapper.Map<ModelViewModel>(model);
            Model.ModelsMaker = Service.FindMaker(model.MakeID);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        public ActionResult Create()
        {
            ViewBag.MakeID = new SelectList(Service.GetMakers(), "MakeID", "MakerName"); 
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =  "ModelID, ModelName,MakeID ")]VehicleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.AddModel(model);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.MakeID = new SelectList(Service.GetMakers(),"MakeID","MakerName",model.MakeID);
            
            return View(model);
        }
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            IVehicleModel model = Service.FindModel(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(AutoMapper.Mapper.Map<ModelViewModel>(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteModel(id);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleModel model = Service.FindModel(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeID = new SelectList(Service.GetMakers(), "MakeID", "MakerName", model.MakeID);
            ModelViewModel Model = AutoMapper.Mapper.Map<ModelViewModel>(model);
            return View(Model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IVehicleModel ModelToUpdate = Service.FindModel(id);
           

            if (TryUpdateModel(ModelToUpdate, "",
                       new string[] { "ModelName", "ModelsMaker" }))
            {
                try
                {
                    Service.UpdateModel(ModelToUpdate);

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.MakeID = new SelectList(Service.GetMakers(), "MakeID", "MakerName", ModelToUpdate.MakeID);
            return View(AutoMapper.Mapper.Map<MakerViewModel>(ModelToUpdate));
        }

    }
}