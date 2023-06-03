using Microsoft.EntityFrameworkCore;
using MVC_Students.Models;

namespace MVC_Students.Services
{
    public class StudentService : IStudentService
    {

        public readonly DatabaseContext _databaseContext;

        public StudentService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddStudent(Student student)
        {
            _databaseContext.Students.Add(student);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Student>> GetStudents()
        { 
            var result = await _databaseContext.Students.ToListAsync();

            return result;
        }

        public async Task RemoveStudent(int id)
        {
            var res = await _databaseContext.Students.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (res != null)
            {
                _databaseContext.Students.Remove(res);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
