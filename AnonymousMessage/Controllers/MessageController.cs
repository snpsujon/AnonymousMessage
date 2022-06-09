using AnonymousMessage.Data;
using AnonymousMessage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnonymousMessage.Controllers
{
    public class MessageController : Controller
    {
        private readonly DataContext _context;
        public MessageController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string user)
        {
            
            if (user != null)
            {
                var username = _context.Users.Where(x => x.UserName == user).FirstOrDefault();
                if (username.IsActive == true)
                {
                    ViewBag.userlink = HttpContext.Session.GetString("UserID");
                    ViewBag.user = user;
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
            var user = HttpContext.Session.GetString("UserID");
            var usertype = HttpContext.Session.GetString("UserType");
            if(usertype == "Admin")
            {
                ViewBag.user = usertype;
                var list = _context.messeges.OrderByDescending(x => x.Id).ToList();
                return View(list);
            }
            else
            {
                var list = _context.messeges.Where(x => x.username == user).OrderByDescending(x => x.Id).ToList();
                return View(list);
            }
            
        }
    }
}
