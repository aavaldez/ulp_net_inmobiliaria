using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ulp_net_inmobiliaria.Models
{
	public class Contrato
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Inquilino")]
		public int InquilinoId { get; set; }
		[ForeignKey(nameof(InquilinoId))]
		public Inquilino? Inquilino { get; set; }
		[Display(Name = "Inmueble")]
		public int InmuebleId { get; set; }
		[ForeignKey(nameof(InmuebleId))]
		public Inmueble? Inmueble { get; set; }
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
		public DateTime Desde { get; set; }
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "La fecha de fin es obligatoria.")]
		public DateTime Hasta { get; set; }
		[Required(ErrorMessage = "El valor de alquiler es obligatorio.")]
		public Decimal Valor { get; set; } = 0;
		public int Estado { get; set; } = 1;

		public override string ToString()
		{
			return $"{Inmueble.ToString()} - {Inquilino.ToString()}";
		}
	}
}
