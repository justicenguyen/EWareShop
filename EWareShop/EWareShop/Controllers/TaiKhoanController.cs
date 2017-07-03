using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EWareShop.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace EWareShop.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly EWareShopContext _context;

        public TaiKhoanController(EWareShopContext context)
        {
            _context = context;
        }
        public IActionResult DangNhap()
        {
            return View();
        }
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy([Bind("Email,MatKhau,MatKhauNhapLai,HoTen")]TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                taiKhoan.Quyen = 3;
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                var taiKhoanHT = _context.TaiKhoan.SingleOrDefault(tk => tk.Email == taiKhoan.Email);
                HttpContext.Session.SetSession("DangNhapSession", taiKhoanHT);
                return RedirectToAction("TaiKhoan");
            }
            return View();
        }

        public IActionResult TaiKhoan()
        {
            var taiKhoan = HttpContext.Session.GetSession<EWareShop.Models.TaiKhoan>("DangNhapSession");
            if (taiKhoan == null)
            {
                return RedirectToAction("DangNhap");
            }
            return View(taiKhoan);
        }

        public IActionResult ThongTinTaiKhoan()
        {
            var taiKhoan = HttpContext.Session.GetSession<EWareShop.Models.TaiKhoan>("DangNhapSession");
            if (taiKhoan == null)
            {
                return RedirectToAction("DangNhap");
            }
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap([Bind("Email,MatKhau")]TaiKhoan taiKhoan)
        {
            var dangNhap = await _context.TaiKhoan.Where(tk => tk.Email == taiKhoan.Email && tk.MatKhau == taiKhoan.MatKhau).SingleOrDefaultAsync();
            if (dangNhap != null)
            {
                if(dangNhap.DangHoatDong)
                {
                    ViewBag.LoiDangNhap = "Tài khoản này đang hoạt động";
                    return View(taiKhoan);
                }
                HttpContext.Session.SetSession("DangNhapSession", dangNhap);
                dangNhap.DangHoatDong = true;
                _context.Update(dangNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThongTinTaiKhoan");
            }
            ViewBag.LoiDangNhap = "Tài khoản không hợp lệ";
            return View(taiKhoan);
        }
        public IActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuenMatKhau(string tendangnhap, string matkhaucap2, string matkhaumoi1, string matkhaumoi2)
        {
            var taiKhoan = await _context.TaiKhoan.Where(tk => tk.Email == tendangnhap && tk.MatKhau == matkhaucap2).SingleOrDefaultAsync();
            if (taiKhoan != null)
            {
                taiKhoan.MatKhau = matkhaumoi1;
                _context.Update(taiKhoan);
                await _context.SaveChangesAsync();
                ViewBag.thanhCong = "Lấy lại mật khẩu thành công";
                return View();
            }
            ViewBag.thanhCong = "Tài khoản không hợp lệ";
            return View();
        }

        public IActionResult DoiMatKhauCap1()
        {
            ViewBag.tenTaiKhoan = HttpContext.Session.GetSession<string>("DangNhapSession");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiMatKhauCap1(string matkhauht, string matkhaumoi)
        {
            var tentaikhoan = HttpContext.Session.GetSession<string>("DangNhapSession");
            var taiKhoan = await _context.TaiKhoan.Where(tk => tk.Email == tentaikhoan && tk.MatKhau == matkhauht).SingleOrDefaultAsync();
            if (taiKhoan != null)
            {
                taiKhoan.MatKhau = matkhaumoi;
                _context.Update(taiKhoan);
                await _context.SaveChangesAsync();
                ViewBag.ketQua = "Thay đổi thành công";
                return View();
            }
            ViewBag.ketQua = "Mật khẩu cấp hiện tại không hợp lệ";
            return View();
        }

        public IActionResult DoiMatKhauCap2()
        {
            ViewBag.tenTaiKhoan = HttpContext.Session.GetSession<string>("DangNhapSession");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiMatKhauCap2(string matkhauht, string matkhaumoi)
        {
            var tentaikhoan = HttpContext.Session.GetSession<string>("DangNhapSession");
            var taiKhoan = await _context.TaiKhoan.Where(tk => tk.Email == tentaikhoan && tk.MatKhau == matkhauht).SingleOrDefaultAsync();
            if (taiKhoan != null)
            {
                taiKhoan.MatKhau = matkhaumoi;
                _context.Update(taiKhoan);
                await _context.SaveChangesAsync();
                ViewBag.ketQua = "Thay đổi thành công";
                return View();
            }
            ViewBag.ketQua = "Mật khẩu cấp hiện tại không hợp lệ";
            return View();
        }

        public IActionResult DoiMatKhau()
        {
            var taiKhoan = HttpContext.Session.GetSession<EWareShop.Models.TaiKhoan>("DangNhapSession");
            if (taiKhoan == null)
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiMatKhau(string matKhauCu, string MatKhauMoi)
        {
            var taiKhoan1 = HttpContext.Session.GetSession<TaiKhoan>("DangNhapSession");
            if (taiKhoan1.MatKhau != matKhauCu)
            {
                ViewBag.ketQua = 0;
                return View();
            }
            taiKhoan1.MatKhau = MatKhauMoi;
            _context.Update(taiKhoan1);
            await _context.SaveChangesAsync();
            ViewBag.ketQua = 1;
            return View();
        }

        public async Task<IActionResult> DangXuat()
        {
            var taiKhoan = HttpContext.Session.GetSession<EWareShop.Models.TaiKhoan>("DangNhapSession");
            if(taiKhoan!=null)
            {
                taiKhoan.DangHoatDong = false;
                _context.Update(taiKhoan);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetSession("DangNhapSession", null);
                return RedirectToAction("DangNhap");
            }
            return RedirectToAction("DangNhap");
        }
        // GET: TaiKhoan
        public async Task<IActionResult> Index(string quyen,string tukhoa)
        {
            var dsTaiKhoan = from tk in _context.TaiKhoan
                             select tk;
            ViewBag.quyen = "tatca";
            ViewBag.tukhoa = "";
            if (!String.IsNullOrEmpty(quyen))
            {
                if(quyen!="tatca")
                {
                    dsTaiKhoan = from tkq in dsTaiKhoan
                                 where tkq.Quyen == Int32.Parse(quyen)
                                 select tkq;
                    ViewBag.quyen = quyen;
                }
                
            }
            if(!String.IsNullOrEmpty(tukhoa))
            {
                dsTaiKhoan = from tktk in dsTaiKhoan
                             where tktk.HoTen.Contains(tukhoa)
                             select tktk;
                ViewBag.tukhoa = tukhoa;
            }
            return View(await dsTaiKhoan.OrderByDescending(tkcc=>tkcc.TaiKhoanID).ToListAsync());
        }

        // GET: TaiKhoan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan
                .SingleOrDefaultAsync(m => m.TaiKhoanID == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: TaiKhoan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaiKhoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoanID,TenDangNhap,MatKhauCap1,MatKhauCap2")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan.SingleOrDefaultAsync(m => m.TaiKhoanID == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaiKhoanID,TenDangNhap,MatKhauCap1,MatKhauCap2")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanID))
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
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan
                .SingleOrDefaultAsync(m => m.TaiKhoanID == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoan = await _context.TaiKhoan.SingleOrDefaultAsync(m => m.TaiKhoanID == id);
            _context.TaiKhoan.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoan.Any(e => e.TaiKhoanID == id);
        }

        public async Task<IActionResult> GuiMaXacThuc(string email)
        {
            Random ran = new Random();
            var maXacThuc = ran.Next(100000,999999);
            HttpContext.Session.SetSession("XacThucSession", maXacThuc);
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("SHOP ĐIỆN TỬ", "shopdientu9@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "XÁC THỰC TÀI EMAIL TÀI KHOẢN THÀNH VIÊN";
            emailMessage.Body = new TextPart("plain") { Text = "Mã xác thực cho tài khoản của bạn là : "+ maXacThuc };
            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("shopdientu9@gmail.com", "cuahangdientu123");

                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            });
            return Content("Đã gửi mã xác thực, vui lòng kiểm tra email");
        }

        //Ajax kiem tra ma xac thuc
        public IActionResult KiemTraXacThuc(string maxacthuc)
        {
            var maXacThuc = HttpContext.Session.GetSession<string>("XacThucSession");
            var ketqua = 1;
            if(maxacthuc!=maXacThuc)
            {
                ketqua = 0;
            }
            return Content(""+ketqua);
        }
        public async Task<IActionResult> GuiMatKhau(string email)
        {
            Random ran = new Random();
            var matKhauMoi = ran.Next(100000, 999999);
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("SHOP ĐIỆN TỬ", "shopdientu9@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "LẤY LẠI MẬT KHẨU";
            emailMessage.Body = new TextPart("plain") { Text = "Mật khẩu mới của bạn là : " + matKhauMoi };
            var taiKhoan = _context.TaiKhoan.SingleOrDefault(tk => tk.Email == email);
            taiKhoan.MatKhau = ""+matKhauMoi;
            _context.Update(taiKhoan);
            await _context.SaveChangesAsync();
            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("shopdientu9@gmail.com", "cuahangdientu123");

                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            });
            return Content("Đã gửi mật khẩu. Vui lòng kiểm tra Email !");
        }

        public IActionResult KiemTraTonTai(string email)
        {
            var taiKhoan = _context.TaiKhoan.SingleOrDefault(tk => tk.Email == email);
            if(taiKhoan!=null)
            {
                return Content("" + 0);
            }
            return Content(""+1);
        }
    }
}
