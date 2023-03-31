using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(string searching)
        {
           
          return View(data.Saches.Where(x=>x.TenSach.ToLower().Contains(searching.ToLower())|| searching == null));
            
        }
    }
}