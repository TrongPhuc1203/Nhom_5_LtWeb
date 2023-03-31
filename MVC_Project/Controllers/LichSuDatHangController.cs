using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    
    public class LichSuDatHangController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: LichSuDatHang
        public ActionResult Index()
        {
            return View(data.CTHoaDons.Where(x => x.HoaDon.TaiKhoan.Equals(User.Identity.Name)));
        }
        
    }
}