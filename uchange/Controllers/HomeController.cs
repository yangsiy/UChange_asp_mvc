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
        RequestDBContext request = new RequestDBContext();
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                string id = User.Identity.Name;
                PersonDB stu = person.Persons.Find(id);
                ViewBag.student_id = stu.student_id;
                ViewBag.first_name = stu.first_name;
                ViewBag.last_name = stu.last_name;

                ItemDB it = item.Items.Find(stu.item_now);
                ViewBag.item_now_name = it.name;
                ViewBag.item_now_id = it.id;

                it = item.Items.Find(stu.item_original);
                ViewBag.item_original_name = it.name;
                ViewBag.item_original_id = it.id;

                int tmp = 0;
                foreach (var r in request.Requests.ToList())
                {
                    if (r.to == stu.item_now)
                        tmp++;
                }
                ViewBag.request_count = tmp;

                if (stu.item_now == stu.item_original)
                {
                    ViewBag.edit_flag = 1;
                }
                else
                {
                    ViewBag.edit_flag = 0;
                }
            }
            return View();
        }

        public ActionResult Init()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
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

        public ActionResult Result()
        {
            PersonDB stu = person.Persons.Find(User.Identity.Name);
            ViewBag.give = item.Items.Find(stu.item_original).name;
            ViewBag.get = item.Items.Find(stu.item_now).name;
            foreach (var p in person.Persons.ToList())
            {
                if (p.item_now == stu.item_original)
                {
                    ViewBag.giveto = p.student_id;
                    break;
                }
            }
            foreach (var p in person.Persons.ToList())
            {
                if (p.item_original == stu.item_now)
                {
                    ViewBag.getfrom = p.student_id;
                    break;
                }
            }

            if (stu.item_now == stu.item_original)
            {
                ViewBag.flag = 0;
            }
            else
            {
                ViewBag.flag = 1;
            }

            return View();
        }
    }
}
