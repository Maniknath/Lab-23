using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalCafe.Models;
using System.Collections.Generic;
using System.Linq;

namespace RoyalCafe.Controllers
{

    public class ShopController : Controller
    {
        public StoreContext db = new StoreContext();
        // GET: Shop
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Menu()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Purchase()
        {
            return View();
        }

        public ActionResult Buy(int id)
        {
            var products = db.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Shop/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Shop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string description, float price)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                var inputs = new Products { Name = name, Description = description, Price = price };
                db.Add(inputs);
                db.SaveChanges();
                ViewData["Message"] = "Saved";
                return View();
            }
            else
            {
                ViewData["Message"] = "Saved";
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
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

        public ActionResult Receipt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Receipt(float price, string answer, string userName)
        {

            var users = db.Users.Where(x => x.UserName == userName);
            if (answer == "yes")
            {
                List<User> usr = db.Users.ToList();

                for (int i = 0; i < usr.Count; i++)
                {
                    User u = usr[i];
                    if (u.UserName == userName && u.Money < price)
                    {
                        ViewData["Message"] = "Sorry! You don't have enough Money!";
                        return View();
                    }
                }


                foreach (var user in users)
                {
                    user.Money -= price;
                }

                db.SaveChanges();

                ViewData["Message"] = "Thank you! ";
                ViewData["Price"] = "$" + price + " Thanks for your Business";
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
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