using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Medic.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminDashboard");
            }
            else if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("DoctorDashboard");
            }
            else
            {
                return RedirectToAction("PatientDashboard");
            }
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult DoctorDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Patient")]
        public IActionResult PatientDashboard()
        {
            return View();
        }
    }
}
