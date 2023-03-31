using MVC_Project.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(int ?page)
        {
            if (page == null) page = 1;
            var all_sach = (from s in data.Saches where s.SoLuongSach > 0 select s).OrderBy(m => m.MaSach);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_sach.ToPagedList(pageNum, pageSize));
            //return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}