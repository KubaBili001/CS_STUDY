using System.ComponentModel.DataAnnotations;

namespace MVC_Students.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IndexNumber { get; set; }

        public Student (int id, string firstName, string lastName, string indexNumber)         {
            Id=id;
            FirstName=firstName;
            LastName=lastName;
            IndexNumber=indexNumber;
        }

        public Student () { }

    }
}
