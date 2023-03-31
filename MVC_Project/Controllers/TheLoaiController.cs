using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        MyDataDataContext data = new MyDataDataContext();
        // GET: TheLoai
        public ActionResult Index()
        {
            var all = from tl in data.TheLoais select tl;
            return View(all);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, TheLoai tl)
        {
            var ten = collection["TenTL"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.TenTL = ten;
                data.TheLoais.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        //-------------Edit-------------------
        public ActionResult Edit(int id)
        {
            var E_category = data.TheLoais.First(m => m.MaTL == id);
            return PartialView(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theloai = data.TheLoais.First(m => m.MaTL == id);
            var E_tenloai = collection["TenTL"];
            theloai.MaTL = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                theloai.TenTL = E_tenloai;
                UpdateModel(theloai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_theloai = data.TheLoais.First(m => m.MaTL == id);
            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_theloai = data.TheLoais.Where(m => m.MaTL == id).First();
            data.TheLoais.DeleteOnSubmit(D_theloai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}