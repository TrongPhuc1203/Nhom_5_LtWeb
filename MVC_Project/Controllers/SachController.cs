using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_sach = from tt in data.Saches select tt;
            return View(all_sach);
        }
        public ActionResult Detail(int id)
        {
            var D_sach = data.Saches.Where(m => m.MaSach == id).First();
            return View(D_sach);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection, Sach sach)
        {
            //List<SelectListItem> list = new List<SelectListItem>();
            //var all = from tt in data.TheLoais select tt;
            //var alltheloai = from tt in data.TheLoais select tt.TenTL;
            //foreach (var theloai in all)
            //    list.Add(new SelectListItem() { Value = theloai.TenTL, Text = theloai.TenTL });
            //ViewBag.LoaiTheLoai = list;
            var ten = collection["TenSach"];

            var hinh = collection["HinhAnh"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't Empty";
            }
            else
            {

                sach.TenSach = ten;

                sach.HinhAnh = hinh;
                data.Saches.InsertOnSubmit(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int? id)
        {
            var E_category = data.Saches.First(m => m.MaSach == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var sach = data.Saches.First(m => m.MaSach == id);
            var E_tenloai = collection["TenSach"];
            sach.MaTL = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sach.TenSach = E_tenloai;
                UpdateModel(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_sach = data.Saches.First(m => m.MaSach == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = data.Saches.Where(m => m.MaSach == id).First();
            data.Saches.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}