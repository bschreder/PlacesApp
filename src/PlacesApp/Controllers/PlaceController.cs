using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlacesApp.Controllers
{
    public class PlaceController : Controller
    {
        // GET: Place
        public ActionResult Index()
        {
            return View();
        }

        // GET: Place/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Place/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Place/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




    }
}
