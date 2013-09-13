using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uchange.Models;

namespace uchange.Controllers
{
    public class ItemController : Controller
    {
        ItemDBContext item = new ItemDBContext();
        PersonDBContext person = new PersonDBContext();
        RequestDBContext request = new RequestDBContext();
        DealDBContext deal = new DealDBContext();

        public ActionResult Index()
        {
            ViewBag.item_now = person.Persons.Find(User.Identity.Name).item_now;
            return View(item.Items.ToList());
        }

        public ActionResult Detail(int id)
        {
            ItemDB it = item.Items.Find(id);
            PersonDB stu=person.Persons.Find(User.Identity.Name);
            foreach (var p in person.Persons.ToList())
            {
                if (p.item_now == id)
                {
                    ViewBag.owner = p.student_id;
                    ViewBag.owner_name = p.first_name + " " + p.last_name;
                    break;
                }
            }

            int count = 0;
            foreach (var r in request.Requests.ToList())
            {
                if (r.to == id)
                    count++;
            }
            ViewBag.request_count = count;

            ViewBag.flag = 0;
            if (it.id == stu.item_now)
                ViewBag.flag = 1;
            else
            {
                foreach (var r in request.Requests.ToList())
                {
                    if (r.to == stu.item_now)
                    {
                        PersonDB p = person.Persons.Find(r.from);
                        if (id == p.item_now)
                        {
                            ViewBag.flag = 2;
                            break;
                        }
                    }
                }
            }
            foreach (var r in request.Requests.ToList())
            {
                if (r.from == stu.student_id && r.to == id)
                {
                    ViewBag.flag = 3;
                    break;
                }
            }
            return View(it);
        }

        public ActionResult Edit(int id)
        {
            ItemDB it = item.Items.Find(id);
            return View(it);
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string description)
        {
            ItemDB it = item.Items.Find(id);
            it.name = name;
            it.description = description;
            item.SaveChanges();
            return RedirectToAction("Detail", new { id = id });
        }

        public ActionResult History(int id)
        {
            List<DealDB> tmp = new List<DealDB>();
            foreach (var d in deal.Deals.OrderBy(c => c.deal_time).ToList())
            {
                if (d.item == id)
                    tmp.Add(d);
            }
            ViewBag.item_id = item.Items.Find(id).id;
            ViewBag.item_name = item.Items.Find(id).name;
            return View(tmp);
        }
    }
}
