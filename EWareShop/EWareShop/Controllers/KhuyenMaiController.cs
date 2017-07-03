using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using EWareShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace EWareShop.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly EWareShopContext _context;
        private IHostingEnvironment hostingEnv;
        public KhuyenMaiController(EWareShopContext context, IHostingEnvironment env)
        {
            _context = context;
            this.hostingEnv = env;
        }

        public async Task<IActionResult> CTKhuyenMai(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.KhuyenMai.SingleOrDefaultAsync(m => m.KhuyenMaiID == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }
            return View(khuyenMai);
        }
        public async Task<IActionResult> CTKhuyenMai1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.KhuyenMai.SingleOrDefaultAsync(m => m.KhuyenMaiID == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }
            return View(khuyenMai);
        }



        // GET: KhuyenMai
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhuyenMai.ToListAsync());
        }
        public IActionResult DanhSachSanPhamKhuyenMai()
        {
            return View();
        }

        //Danh sách sản phẩm khuyến mãi
        public IActionResult LoadSanPham()
        {

            var dsloaisanpham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dsidloaisanphamkhuyenmai = from sp in _context.SanPham
                                           where sp.GiaGiam != sp.GiaGoc
                                           group sp by sp.LoaiSanPhamID;
            var dsloaisanphamkhuyenmai = from lsp in _context.LoaiSanPham
                                         join k in dsidloaisanphamkhuyenmai on lsp.LoaiSanPhamID equals k.Key
                                         select lsp;
            var result = "";
            foreach (var lsp in dsloaisanphamkhuyenmai)
            {
                var dssanphamkhuyenmai = from sp in _context.SanPham
                                         where sp.GiaGiam != sp.GiaGoc && sp.LoaiSanPhamID == lsp.LoaiSanPhamID
                                         select sp;
                int soluong = dssanphamkhuyenmai.Count();
                string xemthemhtml = "";
                string xemthemjs = "";
                if (soluong > 3)
                {
                    xemthemjs = xemthemjs +
                        "<script>" +
                               "$(document).ready(function() {" +
                                 "$('#xemthem-" + lsp.TenLoaiSPKhongDau + "').click(function () {" +
                                   " $.ajax({" +
                                        "url: '/KhuyenMai/LoadThemSanPham'," +
                                        "type: 'GET'," +
                                        "data:{'loaisp':" + lsp.LoaiSanPhamID + "}," +
                                        "success: function(result) {" +
                                            "var s = document.getElementById('" + lsp.TenLoaiSPKhongDau + "').innerHTML;" +
                                            "s = s + result ;" +
                                            "$('#" + lsp.TenLoaiSPKhongDau + "').html(s);" +
                                            " $('#xemthem-" + lsp.TenLoaiSPKhongDau + "').hide();" +
                                                    "}" +
                                               " });" +
                                            "});" +
                                   "});" +
                        "</script> ";
                    result = result + xemthemjs;
                    xemthemhtml = xemthemhtml + "<div style='text-align:center;'><button type='button' class='btn btn-info' id='xemthem-" + lsp.TenLoaiSPKhongDau + "'>Xem thêm</button></div>";
                }
                result = result + "<div class='ne'><div style='text-align:center; font-weight:bold;background-color:#66FFCC;font-size:20px; padding-top:5px;padding-bottom:5px;'>" + lsp.TenLoaiSPCoDau + "</div><div style='margin-bottom:30px;' > <ul class='thumbnails' id='" + lsp.TenLoaiSPKhongDau + "'>";
                foreach (var sp in dssanphamkhuyenmai.Skip(0).Take(3))
                {
                    string giagiam = "";
                    if (sp.GiaGoc != sp.GiaGiam)
                    {
                        int? phantram;
                        phantram = 100 - (int?)sp.GiaGiam * 100 / sp.GiaGoc;
                        giagiam = giagiam + "<p>" +
                                           " <h5> <span style = 'text-decoration:line-through' class='giagoc'> " + sp.GiaGoc + " vnđ</span> &nbsp;<span style = 'background-color:red;color:white;' > " + phantram + " %</ span ></ h5 >" +
                                       "</p>";
                    }
                    result = result +

                            "<li class ='span3'>" +
                                "<div class='thumbnail'>" +
                                    "<a  href='/SanPham/ChiTiet/" + sp.TenSPKhongDau + "' ><img src='/" + sp.HinhAnh + "'/></a>" +
                                   " <div class='caption'>" +
                                       " <h5 style = 'color:blue;text-align:center;' > " + sp.TenSPCoDau + "</h5>" +
                                       " <p>" +
                                           " <h4 style='color:red;text-align:center;'>" + sp.GiaGiam + " vnđ</h4>" +
                                        "</p>" + giagiam
                                        + "<h4 style='text-align:center'><a class='btn' href='/SanPham/ChiTiet/DAU-DIA-DVD-ARIRANG-AR-36MmD'> <i class='icon-zoom-in'></i></a> <a class='btn' href='#'>Add to<i class='icon-shopping-cart'></i></a> </h4>" +
                                    "</div>" +
                               "</div>" +
                            "</li>";
                }
                result = result + "</ul>" + xemthemhtml + "</div></div>";

            }

            return Content(result, "text/plain");
        }

        //Ajax Load thêm sản phẩm khuyến mãi
        public IActionResult LoadThemSanPham(int loaisp)
        {

            var dssanpham = (from sp in _context.SanPham
                             where sp.GiaGoc != sp.GiaGiam && sp.LoaiSanPhamID == loaisp
                             select sp).Skip(3);
            string result = "";
            foreach (var sp in dssanpham)
            {
                string giagiam = "";
                if (sp.GiaGoc != sp.GiaGiam)
                {
                    int? phantram;
                    phantram = 100 - sp.GiaGiam * 100 / sp.GiaGoc;
                    giagiam = giagiam + "<p>" +
                                       " <h5> <span style = 'text-decoration:line-through' class='giagoc'> " + sp.GiaGoc + " vnđ</span> &nbsp;<span style = 'background-color:red;color:white;' > " + phantram + " %</ span ></ h5 >" +
                                   "</p>";
                }
                result = result +

                            "<li class ='span3'>" +
                                "<div class='thumbnail'>" +
                                    "<a  asp-controller='SanPham' asp-action='ChiTiet' asp-route-id='" + sp.SanPhamID + "' ><img src='/" + sp.HinhAnh + "'/></a>" +
                                   " <div class='caption'>" +
                                       " <h5 style = 'color:blue;text-align:center;' > " + sp.TenSPCoDau + "</h5>" +
                                       " <p>" +
                                           " <h4 style='color:red;text-align:center;'>" + sp.GiaGiam + " vnđ</h4>" +
                                        "</p>" + giagiam
                                        + "<h4 style='text-align:center'><a class='btn' href='/SanPham/ChiTiet/DAU-DIA-DVD-ARIRANG-AR-36MmD'> <i class='icon-zoom-in'></i></a> <a class='btn' href='#'>Add to<i class='icon-shopping-cart'></i></a> </h4>" +
                                    "</div>" +
                               "</div>" +
                            "</li>";
            }

            return Content(result, "text/plain");
        }

        // GET: KhuyenMai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.KhuyenMai
                .SingleOrDefaultAsync(m => m.KhuyenMaiID == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            return View(khuyenMai);
        }

        // GET: KhuyenMai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhuyenMai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KhuyenMaiID,TieuDeKhuyenMai,HinhAnh,NoiDung,HienThi")] KhuyenMai khuyenMai, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {

                long size = 0;
                string tenHinh = "";
                var filename = ContentDispositionHeaderValue
                                .Parse(HinhAnh.ContentDisposition)
                                .FileName
                                .Trim('"');
                tenHinh = $@"images\banner\" + filename;
                filename = hostingEnv.WebRootPath + $@"\images\banner\{filename}";
                size += HinhAnh.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    HinhAnh.CopyTo(fs);
                    fs.Flush();
                }
                khuyenMai.HinhAnh = tenHinh;
                _context.Add(khuyenMai);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(khuyenMai);
        }

        // GET: KhuyenMai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.KhuyenMai.SingleOrDefaultAsync(m => m.KhuyenMaiID == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KhuyenMaiID,TieuDeKhuyenMai,HinhAnh,NoiDung,HienThi")] KhuyenMai khuyenMai, IFormFile Hinh_Anh)
        {
            if (id != khuyenMai.KhuyenMaiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (khuyenMai.HinhAnh.Equals("changed"))
                {
                    long size = 0;
                    string tenHinh = "";
                    var filename = ContentDispositionHeaderValue
                                    .Parse(Hinh_Anh.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    tenHinh = $@"images\banner\" + filename;
                    filename = hostingEnv.WebRootPath + $@"\images\banner\{filename}";
                    size += Hinh_Anh.Length;
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        Hinh_Anh.CopyTo(fs);
                        fs.Flush();
                    }
                    khuyenMai.HinhAnh = tenHinh;
                }
                try
                {
                    _context.Update(khuyenMai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhuyenMaiExists(khuyenMai.KhuyenMaiID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(khuyenMai);
        }

        // GET: KhuyenMai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.KhuyenMai
                .SingleOrDefaultAsync(m => m.KhuyenMaiID == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            return View(khuyenMai);
        }

        // POST: KhuyenMai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khuyenMai = await _context.KhuyenMai.SingleOrDefaultAsync(m => m.KhuyenMaiID == id);
            _context.KhuyenMai.Remove(khuyenMai);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KhuyenMaiExists(int id)
        {
            return _context.KhuyenMai.Any(e => e.KhuyenMaiID == id);
        }

        //Ajax thay đổi trạng thái hiển thị của khuyến mãi trên trang chủ
        public async Task<IActionResult> ThayDoiHienThi(int idkm)
        {
            var khuyenMai = await _context.KhuyenMai.SingleOrDefaultAsync(km => km.KhuyenMaiID == idkm);
            khuyenMai.HienThi = !khuyenMai.HienThi;
            _context.Update(khuyenMai);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công !");
        }
    }
}
