using System.ComponentModel.DataAnnotations;

namespace train1.ViewModel
{
    public class AddRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }=string.Empty;
    }
}
