﻿using doma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace doma.Controllers
{
    public class CartController : Controller
    {
        ProjectDMEntities db = new ProjectDMEntities();
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult thongtin()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult thongtin(ThongTinNguoiDungMuaHang model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("xacnhan", model);
            }
            return View(model);
        }

        [Authorize]
        public ActionResult xacnhan(ThongTinNguoiDungMuaHang model)
        {
            return View(model);
        }

        [Authorize]
        [HttpPost]       
        public string xacnhan(string diachi, string dienthoai, string thoigian,
            List<SanPhamTrongGioHang> chitiet)
        {
            if (chitiet != null)
            {
                if (chitiet.Count <= 0)
                {
                    return null;
                }
                DonHang dh = db.DonHangs.Create();
                dh.UserID = User.Identity.GetUserId();
                dh.SoDienThoai = dienthoai;
                dh.ThoiGianGiao = DateTime.Parse(thoigian);
                dh.TinhTrang = 1;
                dh.DiaChiGiao = diachi;
                dh.NgayTao = DateTime.Now;
                db.DonHangs.Add(dh);

                for (int i = 0; i < chitiet.Count; i++)
                {
                    ChiTietDonHang item = new ChiTietDonHang();
                    item.IDDonHang = dh.ID;
                    item.IDSanPham = chitiet[i].id;
                    item.SoLuong = chitiet[i].soluong;
                    item.IDBoSanPham = chitiet[i].idbosanpham;
                    db.ChiTietDonHangs.Add(item);
                }

                db.SaveChanges();
                return dh.ID.ToString();
            }         

            return null;
        }

        public ActionResult thankyou(string ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        [Authorize]
        public ActionResult history() 
        {
            string userid = User.Identity.GetUserId();
            List<DonHang> donhang = db.DonHangs.Where(t => t.UserID == userid).ToList();
            return View(donhang);        
        }
    }
}