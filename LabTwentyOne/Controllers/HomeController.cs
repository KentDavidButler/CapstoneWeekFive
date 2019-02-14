using LabTwentyOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LabTwentyOne.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Welcome to the Frothy Clouds Coffee application. After registering, we'll be able to take your order.";

            return View();
        }

        public ActionResult AddUser(User u)
        {
       
            if(!(Regex.IsMatch(u.FirstName, @"^[a-zA-Z]+$")) || !(Regex.IsMatch(u.LastName, @"^[a-zA-Z]+$")))
                {
                return RedirectToAction("Register", new { message = "Error: In First or Last Name!" });
                }
            
            if (!(Regex.IsMatch(u.Phone, (@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$"))))
            {
                return RedirectToAction("Register", new { message = "Error: Phone number is invalid!" });
            }

            if(u.Password != u.ConfirmPassword)
            {
                return RedirectToAction("Register", new { message = "Error: Passwords did not match" });
            }

            //use a viewbag similar to writeline when testing
            ViewBag.Pronoun = u.Pronoun;
            ViewBag.FirstName = u.FirstName;
            ViewBag.LastName = u.LastName;
            ViewBag.Phone = u.Phone;
            ViewBag.Email = u.Email;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to reach the Clouds";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register(string Message)
        {
            TempData["message"] = Message;
            return View();
        }
    }
}