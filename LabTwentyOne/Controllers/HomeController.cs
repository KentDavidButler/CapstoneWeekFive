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

        readonly List<Item> ItemList = new List<Item>
        {
            new Item("Latte", "Milke Water Espress", 2.50),
            new Item("Hot Chocolate", "Hot Beverage", 1.99),
            new Item("Espresso", "Shot of caffine", 3.40),
            new Item("Coffee", "Coffee", 1.99),
            new Item("Frap", "Stuff", 7.99),
        };

        List<Item> ShoppingCart = new List<Item>();
        List<User> UserList = new List<User>();

        int CartItemsTotal = 0;
        double CartTotalCost = 0.0;

        public ActionResult Index()
        {
            ViewBag.ItemsList = ItemList;
            ViewBag.CurrentUser = (User)Session["CurrentUser"];
            if (Session["CurrentUser"] != null)
            {
                ViewBag.CartItemsTotal = (int)Session["CartItemsTotal"];
                ViewBag.CartTotalCost = (double)Session["CartTotalCost"];
            }


            return View();
        }

        public ActionResult RegisteredUser(User newUser)
        {   //check to see if session of current user has user
            //check to see if session of UserList has users
            if (Session["CurrentUser"] != null)
            {
                newUser = (User)Session["CurrentUser"];
            }
            if (Session["AllUsers"] != null)
            {
                UserList = (List<User>)Session["AllUsers"];
            }


            if (newUser.Password != newUser.ConfirmPassword)
            {
                return RedirectToAction("Register", new { message = "Error: Passwords did not match" });
            }
            foreach (User user in UserList)
            {// searches list for email address entered and checks if it exisists.
                if (user.Email == newUser.Email)
                {
                    return RedirectToAction("Register", new { message = "Error: Email Already Exists" });
                }
            }

            if (ModelState.IsValid)
            {
                //if newUser information is valid from the Register page, this
                //code gets triggered. In this code we update our session info
                //for current user and making it the newUser we pass it, and we
                //add that new user to our user list, and then update our session
                //User list with our newlist of users with our last added user.
                ViewBag.CurrentUser = newUser;
                Session["CurrentUser"] = newUser;
                UserList.Add(newUser);
                Session["AllUsers"] = UserList;
                Session["CartItemsTotal"] = CartItemsTotal;
                Session["CartTotalCost"] = CartTotalCost;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Registration failed. Try again.";
                return RedirectToAction("Register");
            }

        }

        public ActionResult Register(string Message)
        {
            return View();
        }

        public ActionResult LoggedInUser(User newUser)
        {
            if (Session["CurrentUser"] != null)
            {
                newUser = (User)Session["CurrentUser"];
            }
            if (Session["AllUsers"] != null)
            {
                UserList = (List<User>)Session["AllUsers"];
            }

            foreach (User user in UserList)
            {// searches list for email address entered and checks if it exisists.
                if (user.Email == newUser.Email)
                {
                    if (user.Password == newUser.Password)
                    {
                        ViewBag.CurrentUser = user;
                        Session["CurrentUser"] = user;
                        Session["CartItemsTotal"] = CartItemsTotal;
                        Session["CartTotalCost"] = CartTotalCost;

                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Login", new { message = "Login failed. Try again." });
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult AddItem(string itemName, int quantity)
        {


            if (Session["ShoppingCart"] != null)
            {
                ShoppingCart = (List<Item>)Session["ShoppingCart"];
            }
            if (Session["CartItemsTotal"] != null)
            {
                CartItemsTotal = (int)Session["CartItemsTotal"];
            }
            if (Session["CartTotalCost"] != null)
            {
                CartTotalCost = (double)Session["CartTotalCost"];
            }


            foreach (Item item in ItemList)
            {              //find item in list
                if (item.ItemName == itemName)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        ShoppingCart.Add(item);
                        CartItemsTotal++;
                        CartTotalCost += item.Price;
                    }

                }
            }


            Session["CartItemsTotal"] = CartItemsTotal;
            Session["CartTotalCost"] = CartTotalCost;
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }

        public ActionResult Cart()
        {
            ViewBag.CurrentUser = (User)Session["CurrentUser"];
            ViewBag.ItemsList = (List<Item>)Session["ShoppingCart"];

            double total = 0.0;
            if (Session["ShoppingCart"] != null)
            {
                ShoppingCart = (List<Item>)Session["ShoppingCart"];
            }

            foreach (Item item in ShoppingCart)
            {
                total += item.Price;
            }

            ViewBag.Total = string.Format("{0:N2}", total);
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Remove("CurrentUser");
            Session.Remove("ShoppingCart");
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.CurrentUser = (User)Session["CurrentUser"];
            ViewBag.Message = "Welcome to the Frothy Clouds Coffee application. After registering, we'll be able to take your order.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.CurrentUser = (User)Session["CurrentUser"];
            ViewBag.Message = "How to reach the Clouds";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

    }
}