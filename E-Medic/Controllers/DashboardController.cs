using E_Medic.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Medic.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = Guid.Parse(userIdStr);

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminDashboard");
            }
            else if (User.IsInRole("Doctor"))
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
                if (doctor != null)
                {
                    ViewBag.TodayAppointments = await _context.Appointments
                        .CountAsync(a => a.DoctorId == doctor.Id && (a.Status == "Pending" || a.Status == "Approved"));

                    ViewBag.CompletedConsultations = await _context.Appointments
                        .CountAsync(a => a.DoctorId == doctor.Id && a.Status == "Completed");
                }
                else
                {
                    ViewBag.TodayAppointments = 0;
                    ViewBag.CompletedConsultations = 0;
                }

                return View("DoctorDashboard");
            }
            else if (User.IsInRole("Patient"))
            {
                ViewBag.TotalBookings = await _context.Appointments.CountAsync(a => a.PatientId == userId);
                ViewBag.PendingApprovals = await _context.Appointments.CountAsync(a => a.PatientId == userId && a.Status == "Pending");
                ViewBag.CompletedVisits = await _context.Appointments.CountAsync(a => a.PatientId == userId && a.Status == "Completed");

                return View("PatientDashboard");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View("AdminDashboard");
        }
    }
}