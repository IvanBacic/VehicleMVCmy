using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service;
using VehicleMVC;
using System.Net;
using System.Data;

namespace VehicleMVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleService Service;


        public ModelController(IVehicleService service)
        {
            this.Service = service;
        }


        public ActionResult Index()
        {
            IList<IVehicleModel> model = Service.GetModels();
            IList<ModelViewModel> Models = AutoMapper.Mapper.Map<IList<ModelViewModel>>(model);
            return View(Models);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           IVehicleModel model =Service.FindModel(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Create()
        {
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

    }
}