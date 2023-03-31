using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class DanhSachYeuThichController : Controller
    {
        // GET: DanhSachYeuThich
        public ActionResult Index()
        {
            return PartialView();
        }
        MyDataDataContext data = new MyDataDataContext();
        public List<DanhSachYeuThich> layDanhSach()
        {
            List<DanhSachYeuThich> lstYeuThich = Session["DanhSachYeuThich"] as List<DanhSachYeuThich>;
            if (lstYeuThich == null)
            {
                lstYeuThich = new List<DanhSachYeuThich>();
                Session["DanhSachYeuThich"] = lstYeuThich;
            }
            return lstYeuThich;
        }
        [Authorize]
        public ActionResult themDanhSachYeuThich(int id, string strURL)
        {

            List<DanhSachYeuThich> lstYeuThich = layDanhSach();
            DanhSachYeuThich danhsach = lstYeuThich.Find(n => n.maSach == id);
            if (danhsach == null)
            {
                danhsach = new DanhSachYeuThich(id);
                lstYeuThich.Add(danhsach);
                return Redirect(strURL);
            }
            else
            {
                return Redirect(strURL);
            }
        }
        [Authorize]
        public ActionResult DanhSachYeuThich()
        {
            List<DanhSachYeuThich> lstYeuThich = layDanhSach();
            //ViewBag.Tongsoluong = tongSoLuong();
            //ViewBag.Tongtien = tongTien();
            //ViewBag.Tongsoluongsanpham = tongSoLuongSanPham();
            foreach (var item in lstYeuThich)
            {
                var sach = data.Saches.SingleOrDefault(x => x.MaSach == item.maSach);
                //ViewBag.SoLuongTon = sach.SoLuongSach;
            }
            return View(lstYeuThich);
        }
        public ActionResult DanhSachYeuThichPartial()
        {
            //ViewBag.Tongsoluong = tongSoLuong();
            //ViewBag.Tongtien = tongTien();
            //ViewBag.Tongsoluongsanpham = tongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult xoaDanhSachYeuThich(int id)
        {
            List<DanhSachYeuThich> lstYeuThich = layDanhSach();
            DanhSachYeuThich danhsach = lstYeuThich.SingleOrDefault(n => n.maSach == id);
            if (danhsach != null)
            {
                lstYeuThich.RemoveAll(n => n.maSach == id);
                return RedirectToAction("DanhSachYeuThich");
            }
            return RedirectToAction("DanhSachYeuThich");
        }
        public ActionResult capNhapDanhSachYeuThich(int id, FormCollection collection)
        {
            List<DanhSachYeuThich> lstYeuThich = layDanhSach();
            DanhSachYeuThich danhsach = lstYeuThich.SingleOrDefault(n => n.maSach == id);
            //if (danhsach != null)
            //{
            //    danhsach.soLuong = int.Parse(collection["txtsoLg"].ToString());
            //}
            /* capNhapSoLuongTon(id);*/
            return RedirectToAction("DanhSachYeuThich");
        }
        public ActionResult xoaTatCaDanhSachYeuThich()
        {
            List<DanhSachYeuThich> lstYeuThich = layDanhSach();
            lstYeuThich.Clear();
            return RedirectToAction("DanhSachYeuThich");
        }

    }
}