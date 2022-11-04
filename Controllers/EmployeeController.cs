using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HoangManhTungBTH_02.Models.Process;
using HoangManhTungBTH_02.Models;
using HoangManhTungBTH_02.Data;

namespace HoangManhTungBTH_02.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProcess _excelProcess = new ExcelProcess();


        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get: context
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.EmpID == id);
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    // rename file when upload to sever
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);

                        //read data from file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // create a new employee obj

                            var emp = new Employee();

                            //set values for attributes
                            emp.EmpID = dt.Rows[i][0].ToString();
                            emp.EmpName = dt.Rows[i][1].ToString();
                            emp.Address = dt.Rows[i][2].ToString();

                            //add obj  to context
                            _context.Employee.Add(emp);
                        }

                        //save to DB

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}