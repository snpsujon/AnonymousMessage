using AnonymousMessage.Data;
using AnonymousMessage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AnonymousMessage.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string user)
        {
            if(user != null)
            {
                var username = _context.Users.Where(x => x.UserName == user).FirstOrDefault();
                if (username.IsActive == true)
                {
                    return View();
                }
                else
                {
                    
                    return RedirectToAction("Login", "Home");
                }

            }
            else
            {
                return View();
            }
            
            
        }
        [HttpPost]
        public IActionResult Index(Messege data)
        {
            _context.Add(data);
            _context.SaveChanges();
            return Json(1);
        }


        public IActionResult dbv()
        {
            var list = _context.messeges.OrderByDescending(x => x.Id).ToList();
            return View(list);
        }

        public JsonResult CheckUsernameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SeachData = _context.Users.Where(x => x.UserName == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(Users users)
        {
            var UserType = "User";
            Users user = new Users
            {
                UserName = users.UserName,
                Password = users.Password,
                UserType = UserType,
                IsActive = true
            };
            _context.Add(user);
            _context.SaveChanges();
            return View();
        }


        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToAction("Index", "Message");
            }
            ViewBag.error = "I";
            return View();
        }
        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            var adm = _context.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);
            ViewBag.pp = adm;

            if (adm != null)
            {
                if (adm.IsActive != false)
                {
                    HttpContext.Session.SetString("UserID", adm.UserName.ToString());
                    HttpContext.Session.SetString("UserType", adm.UserType);
                    ViewBag.error = "I";
                    return RedirectToAction("Index", "Message", new { user = adm.UserName});

                }
                else
                {
                    ViewBag.error = "You Are Blocked By Admin";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "User or Password Not Found";
                return View();
            }
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Index));
        }


    }
}
