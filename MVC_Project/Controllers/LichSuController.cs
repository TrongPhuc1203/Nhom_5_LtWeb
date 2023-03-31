using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class LichSuController : Controller
    {
        // GET: LichSu
        MyDataDataContext data = new MyDataDataContext();

        public ActionResult Index()
        {
            var all_hoadon = from tt in data.CTHoaDons select tt;
            return View(all_hoadon);
        }
    }
}