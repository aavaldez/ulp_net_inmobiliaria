using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ulp_net_inmobiliaria.Models
{
	public class Inmueble
	{
    [Key]
		public int Id { get; set; }
		[Required]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = "";
		[Required]
    public int Ambientes { get; set; }
		[Required]
    public int Superficie { get; set; }
		public decimal Latitud { get; set; }
		public decimal Longitud { get; set; }
    [Display(Name = "Propietario")]
    public int PropietarioId { get; set; }
    [ForeignKey(nameof(PropietarioId))]
    public Propietario? Propietario { get; set; }
	}
}
