using System.ComponentModel.DataAnnotations;

namespace GUI_Index.ViewModels
{
    public class RegistorViewModel
    {

		[Required]
	    [MinLength(8, ErrorMessage = "value cannot be less than 8 characters")]
	    [MaxLength(20, ErrorMessage = "value cannot be more than 20 characters")]
		public  string Username { get; set; }

	    [Required]
		[MinLength(2, ErrorMessage = "Initials is not allowed")]
	    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		public string GivenName{ get; set; }

	    [Required]
		[MinLength(2, ErrorMessage = "Initials is not allowed")]
	    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		public string LastName { get; set; }

	    [Required]
		[EmailAddress, MaxLength(256)]
	    [MinLength(5, ErrorMessage = "value cannot be less than 5 characters")]
		public string Email { get; set; }

	    [Required]
		[MinLength(8, ErrorMessage = "value cannot be less than 8 characters")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	    [Required]
	    [MinLength(8, ErrorMessage = "value cannot be less than 8 characters")]
	    [DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password not the same")]
	    public string ConfirmPassword { get; set; }
	}
}
