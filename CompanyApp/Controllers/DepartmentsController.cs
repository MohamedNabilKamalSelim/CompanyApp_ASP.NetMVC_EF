using CompanyApp.Models;
using CompanyApp.Models.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CompanyApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ICompanyRepo<Department> departmentRepo;
        private readonly ICompanyRepo<Employee> employeeRepo;

        public DepartmentsController(ICompanyRepo<Department> departmentRepo,
            ICompanyRepo<Employee> employeeRepo)
        {
            this.departmentRepo = departmentRepo;
            this.employeeRepo = employeeRepo;
        }
        public IActionResult Index()
        {
            var departments = departmentRepo.GetAll();

            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department entity)
        {
            if (ModelState.IsValid)
            {
                var existingDepartment = departmentRepo.CheckForExistingEntity(entity);

                if(existingDepartment != null)
                {
                    ViewBag.Massege = "This Department (" + existingDepartment.DepartmentName.ToString() + ") is already exist.";

                    return View();
                }
                departmentRepo.Add(entity);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = departmentRepo.GetById(id);

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department entity)
        {
            if (ModelState.IsValid)
            {
                departmentRepo.Update(entity);

                return RedirectToAction("Index");
            }
            return View("Create", entity);
        }

        public IActionResult Delete(int id)
        {
            bool busyDepartment = IsDpartmentEmpty(id);

            if (busyDepartment)
                TempData["alert"] = "You cannot delete a busy department";
            else 
                departmentRepo.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool IsDpartmentEmpty(int id)
        {
            var departmentName = departmentRepo.GetById(id).DepartmentName;

            IList<Employee>? existingEmployee = employeeRepo.Search(departmentName);

            return existingEmployee.Count() > 0;
        }

        public IActionResult Search(string term)
        {
            var result = departmentRepo.Search(term);

            return View("Index", result);
        }
    }
}
