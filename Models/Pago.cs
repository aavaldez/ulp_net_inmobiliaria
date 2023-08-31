using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ulp_net_inmobiliaria.Models
{
	public enum enPagoEstados
	{
		Pago = 1,
		Impago = 0
	}

	public class Pago
	{
		[Key]
		public int Id { get; set; }
		public decimal Valor { get; set; } = 0;
		public int Estado { get; set; } = 1;
		[Display(Name = "Contrato")]
		public int ContratoId { get; set; }
		[ForeignKey(nameof(ContratoId))]
		public Contrato? Contrato { get; set; }

		public static IDictionary<int, string> ObtenerPagoEstados()
		{
			SortedDictionary<int, string> estados = new SortedDictionary<int, string>();
			Type tipoEnumPagoEstado = typeof(enPagoEstados);
			foreach (var valor in Enum.GetValues(tipoEnumPagoEstado))
			{
				estados.Add((int)valor, Enum.GetName(tipoEnumPagoEstado, valor));
			}
			return estados;
		}
	}
}
