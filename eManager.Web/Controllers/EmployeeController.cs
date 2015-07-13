using eManager.Domain;
using eManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eManager.Web.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IDepartmentDataSource _db;

        public EmployeeController(IDepartmentDataSource db)
        {
            _db = db;
        }

       
        [HttpGet]
        public ActionResult Create(int departmentId)
        {
            var model = new CreateEmployeeViewModel();
            model.DepartmentId = departmentId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var department = _db.Departments.Single(d => d.Id == viewModel.DepartmentId);
                var employee = new Employee();
                employee.name = viewModel.Name;
                employee.HireDate = viewModel.HireDate;
                department.Employees.Add(employee);

                _db.Save();

                return RedirectToAction("detail", "department", new {id=viewModel.DepartmentId });
                   
            }
            return View(viewModel);
        }

    }
}
