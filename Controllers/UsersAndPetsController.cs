using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OwnersPets.Controllers
{
    public class UsersAndPetsController : Controller
    {
        [HttpGet]
        public ActionResult Pets(int id)
        {
            string name = Request.Params["name"];
            return View(new Models.SimpsonPets { IdOwner = id, NameSimpsons = name });
        }
        public ActionResult Owners()
        {
            return View();
        }
    }
}