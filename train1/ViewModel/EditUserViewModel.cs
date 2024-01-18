using System.ComponentModel.DataAnnotations;

namespace train1.ViewModel
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage ="this faild is Required")]
        public string FirstName { get; set; }=string.Empty;


        [Required(ErrorMessage = "this faild is Required")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "this faild is Required")]
        public int Age { get; set; }


        [Required(ErrorMessage = "this faild is Required")]
        public string UserName { get; set; }=string.Empty;



        [Required(ErrorMessage = "this faild is Required")]
        public string Email {  get; set; }=string.Empty;

    }
}
