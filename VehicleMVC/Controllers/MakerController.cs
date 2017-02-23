using Project.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
namespace VehicleMVC.Controllers
{
    public class MakerController : Controller 
    {
        public MakerController(IVehicleService service)
        {          
                this.Service = service;    
        }

        private  IVehicleService Service;

        public ActionResult Index(string sortOrder, string currentFilter,string searchString,int? page)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null) { page = 1; }
            else {  searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;

            IList<IVehicleMake> makers =Service.GetSortedMakers(sortOrder,searchString);
            IList<MakerViewModel> Makers = AutoMapper.Mapper.Map<IList<MakerViewModel>>(makers);

            return View(Makers.ToPagedList(page ?? 1,3));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMake maker = Service.FindMaker(id);
            maker.MakersModels = Service.FindModelsFromMaker(maker.MakeID);
            if (maker == null)
            {
                return HttpNotFound();
            }

            return View(AutoMapper.Mapper.Map<MakerViewModel>( maker));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakerName, MakerDescription")]VehicleMake maker)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.AddMaker(maker);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

          
            return View(maker);
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

            IVehicleMake maker = Service.FindMaker(id);
    
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(AutoMapper.Mapper.Map<MakerViewModel>(maker));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteMaker(id);
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
            IVehicleMake maker = Service.FindMaker(id);

            if (maker == null)
            {
                return HttpNotFound();
            }

            return View(AutoMapper.Mapper.Map<MakerViewModel>(maker));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IVehicleMake MakerToUpdate = Service.FindMaker(id);
            if (TryUpdateModel(MakerToUpdate, "",
                       new string[] { "MakerName", "MakerDescription" }))
            {
                try
                {
                    Service.UpdateMaker(MakerToUpdate);

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(AutoMapper.Mapper.Map<MakerViewModel>(MakerToUpdate));
        }


    }
}