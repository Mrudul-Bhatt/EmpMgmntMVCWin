using EmpMgmnt.Data;
using EmpMgmnt.Models;
using EmpMgmnt.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpMgmnt.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MvcDemoDbContext _demoDbContext;

        public EmployeesController(MvcDemoDbContext mvcDemoDbContext)
        {
            this._demoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _demoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel request)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Salary = request.Salary,
                DateOfBirth = request.DateOfBirth,
                Department = request.Department,
            };

            await _demoDbContext.Employees.AddAsync(employee);
            await _demoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await _demoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };

                return await Task.Run(() => View("View",viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _demoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await _demoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await _demoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                //await _demoDbContext.Employees.Remove(employee);
                _demoDbContext.Employees.Remove(employee);
                await _demoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }  
}
