using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class ViewTheLoaiController : Controller
    {
        // GET: ViewTheLoai
        public ActionResult Index()
        {
            return View();
        }
        MyDataDataContext data = new MyDataDataContext();
        // GET: TheLoai
        public ActionResult TheLoai()
        {
            var all_theloai = from tt in data.TheLoais select tt;
            return View(all_theloai.ToList());
        }

        public ViewResult SachTheoTheLoai(int MaTL = 0)
        {
            //Kiểm tra chủ đề tồn tại hay hông
            TheLoai tl = data.TheLoais.SingleOrDefault(n => n.MaTL == MaTL);
            if (tl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Truy suất danh sách các quyển sách theo chủ đề
            List<Sach> lstSach = data.Saches.Where(n => n.MaTL == MaTL).OrderBy(n => n.DonGia).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có sách nào thuộc thể loại này";
            }
            return View(lstSach);

        }
    }
}