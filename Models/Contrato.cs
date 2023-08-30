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
		public DateTime Desde { get; set; }
		public DateTime Hasta { get; set; }
		public Decimal Valor { get; set; } = 0;
	}
}
