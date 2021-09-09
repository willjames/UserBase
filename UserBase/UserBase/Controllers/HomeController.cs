using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserBase.Features.User;
using UserBase.Models;

namespace UserBase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            //var model = new UserModel();
            //return View(model);
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserModel model)
        {
            var userRepository = new UserRepository();

            userRepository.CreateUser(model.userRecord);

            model.UserCreationSuccessful = true;

            return View(model);
        }

        public ActionResult ListUsers()
        {
            var userRepository = new UserRepository();
            var userModel = new UserModel();

            userModel.userRecords = userRepository.GetAllUserRecords();

            return View(userModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}