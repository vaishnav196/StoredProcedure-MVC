using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoredProcMVC.Data;
using StoredProcMVC.Models;

namespace StoredProcMVC.Controllers
{
    public class EmpController : Controller
    {

        private readonly ApplicationDbContext db;
        public EmpController( ApplicationDbContext db)
        {
            this.db = db;
          
        }
        public IActionResult Index()
        {
            var emp = db.emps.FromSqlRaw("exec GetAllEmps").ToList();
            return View(emp);
        }

        public IActionResult AddEmp()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(Emp e)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlRaw($"exec AddEmp '{e.Name}','{e.Salary}'");
                TempData["Success"] = "Employee Added Successfully";
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
            
        }

        public IActionResult DeleteEmp(int id)
        {
            db.Database.ExecuteSqlRaw($"exec DeleteEmp {id}");
            return RedirectToAction("Index");
        }


        public  IActionResult UpdateEmp(int id)
        {
            var updatedEmp = db.emps.FromSqlRaw($"exec GetEmpById {id}").ToList().FirstOrDefault();
            return View(updatedEmp);
        }

        [HttpPost]
        public IActionResult UpdateEmp(Emp e)
        {
            db.Database.ExecuteSqlRaw($"exec updateEmp {e.Id},{e.Name},{e.Salary}");
            TempData["update"] = "Employee Updated Succesfully";
            return RedirectToAction("Index");
            
        }


        //[HttpPost]
        //public IActionResult DeleteSelected(int[] selectedIds)
        //{
        //    if (selectedIds != null && selectedIds.Length > 0)
        //    {
        //        var ids = string.Join(",", selectedIds);
        //        db.Database.ExecuteSqlRaw($"exec DeleteMultipleEmps '{ids}'");
        //        TempData["delete"] = "Selected employees deleted successfully";
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [Route("Emp/DeleteSelected")]
        public IActionResult DeleteSelected(int[] selectedIds)
        {
            if (selectedIds != null && selectedIds.Length > 0)
            {
                var ids = string.Join(",", selectedIds);
                db.Database.ExecuteSqlRaw($"exec DeleteMultipleEmps '{ids}'");
                TempData["delete"] = "Selected employees deleted successfully";
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("")]
        [Route("Emp")]
        [Route("Emp/Index")]
        public IActionResult Index(string searchp)
        {
            List<Emp> emp;
            if (!string.IsNullOrEmpty(searchp))
            {
                emp = db.emps.FromSqlRaw("exec SearchEmps @p0", searchp).ToList();
            }
            else
            {
                emp = db.emps.FromSqlRaw("exec GetAllEmps").ToList();
            }
            return View(emp);
        }

    }
}
