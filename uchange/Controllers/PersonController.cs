using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uchange.Models;

namespace uchange.Controllers
{
    public class PersonController : Controller
    {
        ItemDBContext item = new ItemDBContext();
        PersonDBContext person = new PersonDBContext();

        public ActionResult Index()
        {
            return RedirectToAction("Detail", new { id = User.Identity.Name });
        }

        public ActionResult Detail(string id)
        {
            PersonDB stu = person.Persons.Find(id);
            ViewBag.first_name = stu.first_name;
            ViewBag.last_name = stu.last_name;
            ViewBag.email = stu.email;
            ItemDB it = item.Items.Find(stu.item_now);
            return View(it);
        }

    }
}
