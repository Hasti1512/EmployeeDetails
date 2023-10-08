using EmployeeDetails.Models;
using EmployeeDetails.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly APIGateway _apiGatway;
        public EmployeeController(APIGateway apiGatway)
        {
            _apiGatway = apiGatway;
        }

        public IActionResult Index()
        {
            List<Employee> emp = new List<Employee>();
            emp = _apiGatway.ListEmployee();
            return View(emp);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            Employee employee = new Employee();
            if (id == 0)
            {
                return View(employee);
            }
            else
            {
                employee = _apiGatway.GetEmployeeById(id);
                return View(employee);
            }
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
           emp =  _apiGatway.CreateEmployee(emp);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            Employee employee  = new Employee();
            employee = _apiGatway.GetEmployeeById(id);
            return View(employee);
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employee = new Employee();
            employee = _apiGatway.GetEmployeeById(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(Employee emp)
        {
            _apiGatway.DeleteEmployee(emp.EmpId);
            return RedirectToAction("Index");
        }
    }
}
