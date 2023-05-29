using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_Prescriptions.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login {get; set;}

        [Required]
        public string Password { get; set;}

        [Required]
        public string RefreshToken { get; set;}

        [Required]
        public DateTime RefreshTokenExp { get; set;}

        [Required]
        public string Salt { get; set;}

        public User() { }

        public User(string login, string password, string refreshToken, DateTime refreshTokenExp, string salt)
        {
            Login=login;
            Password=password;
            RefreshToken=refreshToken;
            RefreshTokenExp=refreshTokenExp;
            Salt=salt;
        }
    }
}
