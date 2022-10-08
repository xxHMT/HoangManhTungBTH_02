using HoangManhTungBTH_02.Data;
using HoangManhTungBTH_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoangManhTungBTH_02.Controllers
{
    public class StudentController : Controller
    {
        //Khai bao DBcontext de lam viec voi database
        private readonly ApplicationDbContext _context;
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }

        //Action tra ve view hien thi danh sach sinh vien
        public async Task<IActionResult> Index()
        {
            var model = await _context.Students.ToListAsync();
            return View(model);
        }

        //Action tra ve view them moi sinh vien
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        //Action xu ly du lieu sinh vien va gui len tu view va luu vao database
        public async Task<IActionResult> Create(Student std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
        
    }
}