using LabTwentyOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabTwentyOne.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to the Frothy Clouds Coffee application. After registering, we'll be able to take your order.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to reach the Clouds";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult AddUser(User u)
        {
            //use a viewbag similar to writeline when testing
            ViewBag.Pronoun = u.Pronoun;
            ViewBag.FirstName = u.FirstName;
            ViewBag.LastName = u.LastName;
            ViewBag.Phone = u.Phone;
            ViewBag.Email = u.Email;

            return View();
        }
    }
}