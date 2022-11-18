using DAL.Models;
using JDBC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JDBC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public DAL.Models.CompanyContext db = new DAL.Models.CompanyContext();
        static List<Employee> employees;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index()
        {      
            return View(employees);
        }

        public IActionResult Index(int IdArea)
        {
            employees = db.Employees.ToList();
            Employee employee = db.Employees.Where(e => e.Id == IdArea).FirstOrDefault(); //De aqui no pasa 
            if (employee != null)
            {
                ViewBag.Area = "Employee: " + employee.Name + " Area: " + employee.EmployeeAreaNavigation.Area1;
            }
            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}