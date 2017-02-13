using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autoshop.Models.Autoshop;

namespace Autoshop.Controllers
{
    public class AutoshopController : Controller
    {
        private AutoshopManager AutoshopManager { get; set; }

        public AutoshopController()
        {
            AutoshopManager = new AutoshopManager();
        }

        // GET: Autoshop
        public ActionResult Index()
        {
            IEnumerable<Car> list = AutoshopManager.GetCars();
            return View(list);
        }

        public ActionResult Car(int id)
        {
            Car car = AutoshopManager.GetCarById(id);
            if (car == null) return HttpNotFound();
            return View(car);
        }
    }
}