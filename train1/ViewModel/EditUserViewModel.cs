using System.ComponentModel.DataAnnotations;

namespace train1.ViewModel
{
    public class EditUserViewModel
    {
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string UserName { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
