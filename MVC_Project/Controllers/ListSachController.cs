using MVC_Project.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class ListSachController : Controller
    {

       
        // GET: ListSach
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_sach = (from s in data.Saches where s.SoLuongSach > 0 select s).OrderBy(m => m.MaSach);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_sach.ToPagedList(pageNum, pageSize));
            //return View();

        }
    }
}