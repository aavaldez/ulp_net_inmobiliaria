﻿namespace ulp_net_inmobiliaria.Models
{
	public class Inquilino
	{
		public int Id { get; set; }
		public string Nombre { get; set; } = "";
		public string Apellido { get; set; } = "";
		public string Dni { get; set; } = "";
		public string? Telefono { get; set; }
		public string? Email { get; set; }
	}
}
