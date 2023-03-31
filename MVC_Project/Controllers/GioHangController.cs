using Microsoft.AspNet.Identity;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        MyDataDataContext data = new MyDataDataContext();
       
       
        public List<GioHang> layGioHang()
        {
            
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //them sp vao gio hang
        [Authorize]
        public ActionResult themGioHang(int id, string strURL)
        {
            
            List<GioHang> lstGioHang = layGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.maSach == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.soLuong++;
                return Redirect(strURL);
            }
        }
        private int tongSoLuong()
        {
            int tong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tong = lstGioHang.Sum(n => n.soLuong);
            }
            return tong;
        }
        private int tongSoLuongSanPham()
        {
            int tong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tong = lstGioHang.Count;
            }
            return tong;
        }
        private double tongTien()
        {
            double tong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tong = lstGioHang.Sum(n => n.thanhTien);
            }
            return tong;
        }
        [Authorize]
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = layGioHang();
            ViewBag.Tongsoluong = tongSoLuong();
            ViewBag.Tongtien = tongTien();
            ViewBag.Tongsoluongsanpham = tongSoLuongSanPham();
            foreach (var item in lstGioHang)
            {
                var sach = data.Saches.SingleOrDefault(x => x.MaSach == item.maSach);
                ViewBag.SoLuongTon = sach.SoLuongSach;
            }
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = tongSoLuong();
            ViewBag.Tongtien = tongTien();
            ViewBag.Tongsoluongsanpham = tongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult xoaGioHang(int id)
        {
            List<GioHang> lstGioHang = layGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.maSach == id);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.maSach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult capNhapGioHang(int id, FormCollection collection)
        {
            List<GioHang> lstGioHang = layGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.maSach == id);
            if (sanpham != null)
            {
                sanpham.soLuong = int.Parse(collection["txtsoLg"].ToString());
            }
            /* capNhapSoLuongTon(id);*/
            return RedirectToAction("GioHang");
        }
        public ActionResult xoaTatCaGioHang()
        {
            List<GioHang> lstGioHang = layGioHang();
            lstGioHang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult dathang()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng
            List<GioHang> lstGioHang = layGioHang();

            // Kiểm tra nếu giỏ hàng trống thì quay về trang chủ
            //if (lstGioHang.Count == 0)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //data.SubmitChanges();
            ViewBag.Tongsoluongsanpham = tongSoLuongSanPham();
            ViewBag.Tongsoluong = tongSoLuong();
            ViewBag.Tongtien = tongTien();
            // Cập nhật số lượng sản phẩm trong cơ sở dữ liệu
            foreach (var item in lstGioHang)
            {
                var sach = data.Saches.SingleOrDefault(s => s.MaSach == item.maSach);
                if (sach != null)
                {
                    sach.SoLuongSach -= item.soLuong;
                }
            }
            data.SubmitChanges();
            HoaDon hoadon = new HoaDon();
            List<GioHang> giohang = layGioHang();
            hoadon.TaiKhoan = User.Identity.Name;
            hoadon.NgayTao = DateTime.Now;
            data.HoaDons.InsertOnSubmit(hoadon);
            data.SubmitChanges();
            foreach (var item in giohang)
            {
                CTHoaDon cthd = new CTHoaDon();
                cthd.MaHD = hoadon.MaHD;
                cthd.MaSach = item.maSach;
                cthd.SoLuongMua = item.soLuong;
                cthd.ThanhTien = item.thanhTien;
                data.SubmitChanges();
                data.CTHoaDons.InsertOnSubmit(cthd);
            }
            data.SubmitChanges();
            // Xóa giỏ hàng sau khi đặt hàng thành công
            Session["GioHang"] = null;
            return View(lstGioHang); ;
        }
        
        public ActionResult dathang(FormCollection collection)
        { 
            
            
            
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
    }
}