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
    public int Ambientes { get; set; } = 1;
		[Required]
    public int Superficie { get; set; } = 0;
	public decimal Latitud { get; set; } = 0;
	public decimal Longitud { get; set; } = 0;
	public decimal Valor { get; set; } = 0;
	public int estado { get; set; } = 1;
    [Display(Name = "Propietario")]
    public int PropietarioId { get; set; }
    [ForeignKey(nameof(PropietarioId))]
    public Propietario? Propietario { get; set; }
	}
}
