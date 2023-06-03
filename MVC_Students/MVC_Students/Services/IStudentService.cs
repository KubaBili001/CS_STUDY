using MVC_Students.Models;

namespace MVC_Students.Services
{
    public interface IStudentService
    {
        public Task AddStudent(Student student);
        public Task RemoveStudent(int id);
        public Task<List<Student>> GetStudents();
    }
}
