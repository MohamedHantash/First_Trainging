using System.ComponentModel.DataAnnotations;

namespace train1.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }=string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password iS requried")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
