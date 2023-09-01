using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ulp_net_inmobiliaria.Models
{
	public enum enTipos
	{
		Casa = 1,
		Departamento = 2
	}

	public enum enEstados
	{
		Habilitado = 1,
		Inhabilitado = 2
	}

	public class Inmueble
	{
		[Key]
		public int Id { get; set; }
		public int Tipo { get; set; } = 1;
		[NotMapped]
		public string TipoNombre => Tipo > 0 ? ((enTipos)Tipo).ToString() : "";
		[Required(ErrorMessage= "La dirección es obligatoria.")]
		[Display(Name = "Dirección")]
		public string Direccion { get; set; } = "";
		[Required(ErrorMessage= "La cantidad de ambientes es obligatorio.")]
		public int Ambientes { get; set; } = 1;
		[Required(ErrorMessage= "La superficie es obligatoria.")]
		public int Superficie { get; set; } = 0;
		public decimal Latitud { get; set; } = 0;
		public decimal Longitud { get; set; } = 0;
		public decimal Valor { get; set; } = 0;
		public int Estado { get; set; } = 1;
		[Display(Name = "Propietario")]
		public int PropietarioId { get; set; }
		[ForeignKey(nameof(PropietarioId))]
		public Propietario? Propietario { get; set; }

		public static IDictionary<int, string> ObtenerTipos()
		{
			SortedDictionary<int, string> tipos = new SortedDictionary<int, string>();
			Type tipoEnumTipo = typeof(enTipos);
			foreach (var valor in Enum.GetValues(tipoEnumTipo))
			{
				tipos.Add((int)valor, Enum.GetName(tipoEnumTipo, valor));
			}
			return tipos;
		}

		public static IDictionary<int, string> ObtenerEstados()
		{
			SortedDictionary<int, string> estados = new SortedDictionary<int, string>();
			Type tipoEnumEstado = typeof(enEstados);
			foreach (var valor in Enum.GetValues(tipoEnumEstado))
			{
				estados.Add((int)valor, Enum.GetName(tipoEnumEstado, valor));
			}
			return estados;
		}

		public override string ToString()
		{
			return $"{TipoNombre} - {Direccion}";
		}
	}
}
