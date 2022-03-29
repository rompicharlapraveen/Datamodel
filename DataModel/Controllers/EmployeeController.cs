using DataModel.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using static DataModel.Models.Employee;

namespace DataModel.Controllers
{
    public class EmployeeController : Controller
    {
        private EmpDBContext db = new EmpDBContext();

        // GET: Employee
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.JoiningDateSortParm = sortOrder == "JoiningDate" ? "JoiningDate_desc" : "JoiningDate";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_desc" : "Age";

            var employees = from employee in db.Employees
                           select employee;
            switch (sortOrder)
            {
                case "Name":
                    employees = employees.OrderByDescending(employee => employee.Name);
                    break;
                case "JoiningDate":
                    employees = employees.OrderBy(employee => employee.JoiningDate);
                    break;
                case "JoiningDate_desc":
                    employees = employees.OrderByDescending(employee => employee.JoiningDate);
                    break;
                case "Age":
                    employees = employees.OrderBy(employee => employee.Age);
                    break;
                case "Age_desc":
                    employees = employees.OrderByDescending(employee => employee.Age);
                    break;
                default:
                    employees = employees.OrderBy(employee => employee.Name);
                    break;
            }
            return View(employees.ToList());
        }
 
        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = db.Employees.Single(m => m.ID == id);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var employee = db.Employees.Single(m => m.ID == id);
                if (TryUpdateModel(employee))
                {
                    //To Do:- database code
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch
            {
                return View();
            }
        }
        // GET: Employee/Details/5
        public ActionResult Details(int ? id)
        {
            var employee = db.Employees.Single(m => m.ID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(employee);
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var employee = db.Employees.Single(m => m.ID == id);
                db.Employees.Remove(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}