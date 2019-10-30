using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalCafe.Models;
using System.Linq;
namespace RoyalCafe.Controllers
{
    public class LoginController : Controller
    {
        public StoreContext db = new StoreContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogin(string userName, string password)
        {
            var loginData = from user in db.Users
                            where user.UserName.Equals(userName)
                            select user;

            foreach (var user in loginData)
            {
                if (userName.Equals(user.UserName) && password.Equals(user.Password))
                {
                    ViewData["Message"] = "You Are Logged In";
                    Response.Cookies.Append("userNameCookie", user.UserName);
                    return RedirectToAction("Index", "Products");
                    //return RedirectToAction(nameof("Index"));
                }
                else
                {
                    ViewData["Message"] = "Invalid User Name or Password";
                    return View();
                }

            }
            return View();
        }
        public ActionResult Login(string userName, string password)
        {
            var loginData = from user in db.Users
                            where user.UserName.Equals(userName)
                            select user;

            foreach (var user in loginData)
            {
                if (userName.Equals(user.UserName) && password.Equals(user.Password))
                {
                    ViewData["Message"] = "You Are Logged In";
                    Response.Cookies.Append("userNameCookie", user.UserName);
                    return RedirectToAction("Index", "Shop");
                    //return RedirectToAction(nameof("Index"));
                }
                else
                {
                    ViewData["Message"] = "Incorrect User Name or Password or Try Register";
                    return View();
                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            Response.Cookies.Delete("userNameCookie");

            return RedirectToAction("Thanks", "Login");
        }
        public ActionResult Thanks()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        // POST: Login/Create
        [HttpPost]
        public ActionResult Register(string userName, string password, float money)
        {
            if (ModelState.IsValid)
            {
                var inputs = new User { UserName = userName, Password = password, Money = money };
                db.Add(inputs);
                db.SaveChanges();
                ViewData["Message"] = "Thanks For Register. Please Login!";
                return View();
            }
            else
            {
                ViewData["Message"] = "Saved";
                return View();
            }

            //  return View();

        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}