using Microsoft.AspNetCore.Mvc;
using MVC_Students.Models;
using MVC_Students.Services;

namespace MVC_Students.Controllers
{
    public class StudentsController : Controller
    {

        public readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Get()
        {
            var res = await _studentService.GetStudents();

            return View(res);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student newStudent)
        {

            if (!ModelState.IsValid) 
            {
                return View(newStudent);
            }

            await _studentService.AddStudent(newStudent);

            return RedirectToAction("Get");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.RemoveStudent(id);

            return RedirectToAction("Get");
        }
    }
}
