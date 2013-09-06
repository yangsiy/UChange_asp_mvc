using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;
using uchange.Models;

namespace uchange.Controllers
{
    public class RequestsController : Controller
    {
        ItemDBContext item = new ItemDBContext();
        PersonDBContext person = new PersonDBContext();
        RequestDBContext request = new RequestDBContext();
        DealDBContext deal = new DealDBContext();

        public ActionResult Index()
        {
            List<ItemDB> tmp = new List<ItemDB>();
            PersonDB stu = person.Persons.Find(User.Identity.Name);
            foreach (var r in request.Requests.ToList())
            {
                if (r.to == stu.item_now)
                {
                    PersonDB p = person.Persons.Find(r.from);
                    ItemDB i = item.Items.Find(p.item_now);
                    tmp.Add(i);
                }
            }
            return View(tmp);
        }

        public ActionResult Send(int id)
        {
            RequestDB re = new RequestDB();
            re.from = User.Identity.Name;
            re.to = id;
            request.Requests.Add(re);
            request.SaveChanges();
            return RedirectToAction("Detail", "Item", new { id = id });
        }

        public ActionResult Accept(int id)
        {
            PersonDB stu1, stu2;
            string tmp="";
            stu1 = person.Persons.Find(User.Identity.Name);
            foreach (var p in person.Persons.ToList())
            {
                if (p.item_now == id)
                {
                    tmp = p.student_id;
                    break;
                }
            }
            stu2 = person.Persons.Find(tmp);

            DealDB t1 = new DealDB();
            DealDB t2 = new DealDB();
            t1.from = stu1.student_id;
            t1.to = stu2.student_id;
            t1.item = stu1.item_now;
            t1.deal_time = DateTime.Now;
            deal.Deals.Add(t1);
            t2.from = stu2.student_id;
            t2.to = stu1.student_id;
            t2.item = stu2.item_now;
            t2.deal_time = DateTime.Now;
            deal.Deals.Add(t2);
            deal.SaveChanges();

            int k;
            k = stu1.item_now;
            stu1.item_now = stu2.item_now;
            stu2.item_now = k;
            person.SaveChanges();

            foreach (var r in request.Requests.ToList())
            {
                if (r.from == stu1.student_id || r.from == stu2.student_id)
                    request.Requests.Remove(r);
            }
            request.SaveChanges();
            return RedirectToAction("Detail", "Item", new { id = id });
        }
    }
}
