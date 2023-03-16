using System.ComponentModel.DataAnnotations;

namespace PRN221_Secondhand.Models.Admin
{
    public class PasswordValidate: Repository.Models.Admin
    {

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordRequire { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("PasswordRequire", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        
    }
}
