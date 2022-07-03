using System.ComponentModel.DataAnnotations;

namespace TimeSheet.DTO_Models
{
    public class MemberLoginDTO
    {
        [Required(ErrorMessage ="Please enter your username/email.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter your password.")]
        public string password { get; set; }
    }
}