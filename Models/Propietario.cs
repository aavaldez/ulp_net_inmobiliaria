using System.ComponentModel.DataAnnotations;
namespace ulp_net_inmobiliaria.Models
{
	public class Propietario
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "El Nombre es obligatorio.")]
		public string Nombre { get; set; } = "";
		[Required(ErrorMessage = "El Apellido es obligatorio.")]
		public string Apellido { get; set; } = "";
		[Required(ErrorMessage = "El DNI es obligatorio.")]
		public string Dni { get; set; } = "";
		[Display(Name = "Teléfono")]
		[DisplayFormat(NullDisplayText = "Sin teléfono")]
		public string? Telefono { get; set; }
		[DisplayFormat(NullDisplayText = "Sin email")]
		public string? Email { get; set; }
		public int Estado { get; set; } = 1;

		public override string ToString()
		{
			return $"{Apellido}, {Nombre}";
		}
	}
}