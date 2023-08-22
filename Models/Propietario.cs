using System.ComponentModel.DataAnnotations;
namespace ulp_net_inmobiliaria.Models
{
	public class Propietario
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage= "El Nombre es obligatorio.")]
		public string Nombre { get; set; } = "";
		[Required(ErrorMessage= "El Apellido es obligatorio.")]
		public string Apellido { get; set; } = "";
		[Required(ErrorMessage= "El DNI es obligatorio.")]
		public string Dni { get; set; } = "";
		[Display(Name = "Teléfono")]
		public string? Telefono { get; set; }
		public string? Email { get; set; }

		public override string ToString()
		{
			return $"{Apellido}, {Nombre}";
		}
	}
}