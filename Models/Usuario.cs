using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ulp_net_inmobiliaria.Models
{
	public enum enRoles
	{
		Administrador = 1,
		Empleado = 2,
	}

	public class Usuario
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Rol { get; set; } = 2;
		[NotMapped]
		public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";
		public string Nombre { get; set; } = "";
		[Required(ErrorMessage = "El Apellido es obligatorio.")]
		public string Apellido { get; set; } = "";
		[Required(ErrorMessage = "El email es obligatorio."), EmailAddress]
		public string Email { get; set; } = "";
		[Required(ErrorMessage = "La contraseña es obligatoria."), DataType(DataType.Password)]
		public string Password { get; set; }
		public string? Avatar { get; set; }
		[NotMapped]
		public IFormFile AvatarFile { get; set; }
		public int Estado { get; set; } = 1;

		public override string ToString()
		{
			return $"{Apellido}, {Nombre}";
		}

		public static IDictionary<int, string> ObtenerRoles()
		{
			SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
			Type tipoEnumRol = typeof(enRoles);
			foreach (var valor in Enum.GetValues(tipoEnumRol))
			{
				roles.Add((int)valor, Enum.GetName(tipoEnumRol, valor));
			}
			return roles;
		}
	}
}