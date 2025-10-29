using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Nettruyen.Models;

namespace Nettruyen.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

       
        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password.Trim());
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string Username, string Email, string Password, string ConfirmPassword)
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            if (Password != ConfirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View();
            }

            var existingUser = db.Users.FirstOrDefault(u => u.Email == Email);
            if (existingUser != null)
            {
                ViewBag.Error = "Email này đã được sử dụng.";
                return View();
            }

            try
            {
                string hashedPassword = HashPassword(Password);

                var newUser = new User
                {
                    Username = Username,
                    Email = Email,
                    PasswordHash = hashedPassword,
                    Role = 1,
                    CreatedAt = DateTime.Now,
                    PaidSubscription = false,
                    IdRole = 1
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi đăng ký: " + ex.Message;
                return View();
            }
        }

     
        public ActionResult Login()
        {
            if (Session["UserID"] != null)
                return RedirectToAction("Index", "Home");

            if (TempData["SuccessMessage"] != null)
                ViewBag.Success = TempData["SuccessMessage"].ToString();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.Error = "Vui lòng nhập email và mật khẩu.";
                return View();
            }

            string hashedPassword = HashPassword(Password);
            var user = db.Users.FirstOrDefault(u => u.Email == Email && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                Session["Email"] = user.Email;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai email hoặc mật khẩu.";
            return View();
        }

       
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

       
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string Email)
        {
            ViewBag.Message = $"Nếu {Email} tồn tại trong hệ thống, liên kết đặt lại mật khẩu sẽ được gửi.";
            return View();
        }

        
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            ViewBag.Message = "Chào mừng, " + Session["Username"] + "!";
            return View();
        }

     
        public ActionResult DebugUsers()
        {
            var users = db.Users.ToList();
            return View(users);
        }
    }
}
