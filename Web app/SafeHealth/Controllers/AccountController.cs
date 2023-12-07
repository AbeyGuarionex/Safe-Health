/*
 * File: AccountController.cs
 * Author: Abey Velez Class 
 * UPR Bayamon
 * Date: 12/02/2023
 * Purpose: This class represents a controller in the mvc, that 
 * handles the log in.
 */

using SafeHealth.Domain;
using SafeHealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Data.Common;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;


namespace SafeHealth.Controllers
{
    [Route("User")]
    public class AccountController : Controller
    {

        /// <summary>The user that is signing in.</summary>
        private User user;

        private AccountService accountService;




        public AccountController(AccountService _accountService)
        {
            accountService = _accountService;
        }



        /// <summary>Action to display the index page.</summary>
        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }



        /// <summary>Action to display the index page where the log in is done.</summary>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string userEmail, string userPassword)
        {
            var user = accountService.Login(userEmail, userPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("userEmail", user.UserEmailPk2);

                HttpContext.Session.SetString("userName", user.FirstName);

                HttpContext.Session.SetString("userLastName", user.PaternalLastName);

                HttpContext.Session.SetString("userOfficeCode", user.UserOfficeCodeFk);

                HttpContext.Session.SetString("userCode", user.UserCodePk1);

                string userType = user.UserType;

                if (userType == "D")
                {
                    HttpContext.Session.SetString("userType", "Doctor");
                }
                else
                {
                    HttpContext.Session.SetString("userType", "Patient");
                }

                this.user = user;

                return RedirectToAction("Welcome", "Account");
            }
            else
            {
                ViewBag.msg = "Invalid Email or Password";

                return View("Index");
            }
        }

        [Route("Welcome")]
        public IActionResult Welcome()
        {
            string checkIfLogedIn = HttpContext.Session.GetString("userEmail");

            if (checkIfLogedIn != null)
            {
                ViewBag.userEmail = HttpContext.Session.GetString("userEmail");
                ViewBag.userName = HttpContext.Session.GetString("userName");
                ViewBag.userLastName = HttpContext.Session.GetString("userLastName");
                ViewBag.userType = HttpContext.Session.GetString("userType");

                Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.Headers.Add("Pragma", "no-cache");
                Response.Headers.Add("Expires", "0");

                return View("Welcome", new { version = Guid.NewGuid().ToString() });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        /// <summary>Action to log out, it removes the current session and returns to log in 
        /// page (index).</summary>
        [Route("Logout")]
        public IActionResult Logout()
        {

            ViewBag.userType = HttpContext.Session.GetString("userType");

            HttpContext.Session.Remove("userEmail");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userType");
            HttpContext.Session.Remove("userLastName");
            HttpContext.Session.Remove("userOfficeCode");
            HttpContext.Session.Remove("userCode");


            

            return RedirectToAction("Index");
        }


        [Route("Settings")]
        public IActionResult Settings()
        {
            return View("Settings");
        }




    }
}

