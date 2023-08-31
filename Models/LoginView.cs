using System.ComponentModel.DataAnnotations;

namespace ulp_net_inmobiliaria.Models
{
	public class LoginView
	{
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}