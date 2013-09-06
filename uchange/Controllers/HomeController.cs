using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uchange.Models;

namespace uchange.Controllers
{
    public class HomeController : Controller
    {
        ItemDBContext item = new ItemDBContext();
        PersonDBContext person = new PersonDBContext();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                string id = User.Identity.Name;
                PersonDB stu = person.Persons.Find(id);
                ViewBag.first_name = stu.first_name;
                ViewBag.last_name = stu.last_name;
                ItemDB it = item.Items.Find(stu.item_now);
                ViewBag.item_now_name = it.name;
                ViewBag.item_now_id = it.id;
            }
            return View();
        }

        public ActionResult Init()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Init(string item_name, string item_description)
        {
            ItemDB it = new ItemDB();
            it.name = item_name;
            it.description = item_description;
            item.Items.Add(it);
            item.SaveChanges();

            PersonDB stu = person.Persons.Find(User.Identity.Name);
            stu.item_now = it.id;
            stu.item_original = it.id;
            person.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
