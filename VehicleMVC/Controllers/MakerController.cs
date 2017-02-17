using Project.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleMVC;

namespace VehicleMVC.Controllers
{
    public class MakerController : Controller 
    {

        public MakerController(IVehicleService service)
        {          
                this.Service = service;    
        }

        private  IVehicleService Service;

        public ActionResult Index()
        {
            IList<IVehicleMake> maker = Service.GetMakers();
            IList<MakerViewModel> Makers = AutoMapper.Mapper.Map<IList<MakerViewModel>>(maker);
            return View(Makers);
        }

        public ActionResult Details(int? id)
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

    }
}