using System.ComponentModel.DataAnnotations;
namespace ulp_net_inmobiliaria.Models
{
	public class Usuario
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Rol { get; set; }
		public string Nombre { get; set; } = "";
		[Required(ErrorMessage= "El Apellido es obligatorio.")]
		public string Apellido { get; set; } = "";
		[Required(ErrorMessage= "El email es obligatorio."), EmailAddress]
		public string Email { get; set; } = "";
		[Required(ErrorMessage= "La contraseña es obligatoria."), DataType(DataType.Password)]
		public string Password { get; set; }
		public string Avatar { get; set; } = "";
		public int Estado { get; set; } = 1;

		public override string ToString()
		{
			return $"{Apellido}, {Nombre}";
		}
	}
}