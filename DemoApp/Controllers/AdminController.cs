using DemoApp.Models;
using DemoApp1.COMMON;
using DemoApp1.UNIT_OF_WORK.INTERFACE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Validate username and password (this is just a placeholder)

            // In a real-world scenario, you would authenticate against your database or identity provider
            // For simplicity, let's assume authentication is successful
            if (username == "admin" && EncriptionService.EncryptPassword(password) == EncriptionService.EncryptPassword("Ganesha@123"))
            {
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("role", "Admin"); // Assuming the user has the Admin role
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == "Admin")
            {
                var resumes = _unitOfWork.ResumeRepo.GetAll(); // Assuming _unitOfWork.ResumeRepo.GetAll() returns IEnumerable<ResumeEntity>

                var viewModel = new ResumeListViewModel();
                if (resumes != null)
                {
                    viewModel.Resumes = resumes.ToList();
                }
                return View(viewModel);

            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }


    }
}
