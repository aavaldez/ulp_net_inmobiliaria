using System.Data;
using MySql.Data.MySqlClient;

namespace ulp_net_inmobiliaria.Models
{
	public class RepositorioContrato
	{
		protected readonly string connectionString;
		public RepositorioContrato()
		{
			connectionString = "Server=localhost;User=root;Password=;Database=inmo_aavaldez;SslMode=none";
		}

		public int Alta(Contrato c)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"INSERT INTO Contratos 
					(InquilinoId, InmuebleId, Desde, Hasta, Valor)
					VALUES (@inquilinoId, @inmuebleId, @desde, @hasta, @valor);
					SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@inquilinoId", c.InquilinoId);
					command.Parameters.AddWithValue("@inmuebleId", c.InmuebleId);
					command.Parameters.AddWithValue("@desde", c.Desde);
					command.Parameters.AddWithValue("@hasta", c.Hasta);
					command.Parameters.AddWithValue("@valor", c.Valor);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					c.Id = res;
					connection.Close();
				}
			}
			return res;
		}
		
		public int Renovar(Contrato c)
		{
			int res = 0;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				String sql = @"INSERT INTO Contratos
					(InquilinoId, InmuebleId, Desde, Hasta, Valor)
					VALUES (@inquilinoId, @inmuebleId, @desde, @hasta, @valor);
					SELECT LAST_INSERT_ID();";

				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id_inquilino", c.InquilinoId);
					command.Parameters.AddWithValue("@id_inmueble", c.InmuebleId);
					command.Parameters.AddWithValue("@desde", c.Desde);
					command.Parameters.AddWithValue("@hasta", c.Hasta);
					command.Parameters.AddWithValue("@valor", c.Valor);

					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					c.Id = res;
					connection.Close();
				}
			}
			return res;
		}
		
		public int BajaFisica(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "DELETE FROM Contratos WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		
		public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Contratos 
					SET Estado=@estado
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@estado", 0);
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int Modificacion(Contrato c)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Contratos 
					SET 
						InmuebleId = @inmuebleId, 
						InquilinoId = @inquilinoId, 
						Desde = @desde, 
						Hasta = @hasta, 
						Valor = @valor
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@inmuebleId", c.InmuebleId);
					command.Parameters.AddWithValue("@inquilinoId", c.InquilinoId);
					command.Parameters.AddWithValue("@desde", c.Desde);
					command.Parameters.AddWithValue("@hasta", c.Hasta);
					command.Parameters.AddWithValue("@valor", c.Valor);
					command.Parameters.AddWithValue("@id", c.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int Terminar(Contrato c)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Contratos 
					SET 
						Hasta = @hasta, 
						Estado = @estado
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@hasta", c.Hasta);
					command.Parameters.AddWithValue("@estado", c.Estado);
					command.Parameters.AddWithValue("@id", c.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Contrato> ObtenerTodos()
		{
			IList<Contrato> res = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					c.Id, c.InquilinoId, c.InmuebleId, c.Desde, c.Hasta, c.Valor, c.Estado, i.Nombre, i.Apellido, inm.Tipo, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.InquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id
					WHERE c.Estado = 1";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Contrato c = new Contrato
						{
							Id = reader.GetInt32(nameof(Contrato.Id)),
							Desde = reader.GetDateTime("Desde"),
							Hasta = reader.GetDateTime("Hasta"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							InquilinoId = reader.GetInt32("InquilinoId"),
							Inquilino = new Inquilino
							{
								Id = reader.GetInt32("InquilinoId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
							},
							InmuebleId = reader.GetInt32("InmuebleId"),
							Inmueble = new Inmueble
							{
								Id = reader.GetInt32("InmuebleId"),
								Tipo = reader.GetInt32("Tipo"),
								Direccion = reader.GetString("Direccion")
							}
						};
						res.Add(c);
					}
					connection.Close();
				}
			}
			return res;
		}

		public List<Contrato> ObtenerTodosInmueble(int id)
		{
			List<Contrato> res = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					c.Id, c.InquilinoId, c.InmuebleId, c.Desde, c.Hasta, c.Valor, c.Estado, i.Nombre, i.Apellido, inm.Tipo, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.InquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id
					WHERE c.InmuebleId=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Contrato c = new Contrato
						{
							Id = reader.GetInt32(nameof(Contrato.Id)),
							Desde = reader.GetDateTime("Desde"),
							Hasta = reader.GetDateTime("Hasta"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							InquilinoId = reader.GetInt32("InquilinoId"),
							Inquilino = new Inquilino
							{
								Id = reader.GetInt32("InquilinoId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
							},
							InmuebleId = reader.GetInt32("InmuebleId"),
							Inmueble = new Inmueble
							{
								Id = reader.GetInt32("InmuebleId"),
								Tipo = reader.GetInt32("Tipo"),
								Direccion = reader.GetString("Direccion")
							}
						};
						res.Add(c);
					}
					connection.Close();
				}
			}
			return res;
		}

		public List<Contrato> ObtenerVigentes(DateTime? desde, DateTime? hasta)
		{
			List<Contrato> res = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string query = @"SELECT 
					c.Id, c.InquilinoId, c.InmuebleId, c.Desde, c.Hasta, c.Valor, c.Estado, i.Nombre, i.Apellido, inm.Tipo, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.InquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id
					WHERE c.estado = 1 ";

				if (desde != DateTime.MinValue && hasta != DateTime.MinValue) // Si estan ambos valores
				{
					query += "AND desde >= @desde AND hasta <= @hasta;";
				}
				else if (desde != DateTime.MinValue && hasta == DateTime.MinValue) // Si esta solo el "desde"
				{
					query += "AND desde >= @desde;";
				}
				else if (hasta != DateTime.MinValue && desde == DateTime.MinValue) // Si esta solo el "hasta"
				{
					query += "AND hasta <= @hasta;";
				}

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					if (desde.HasValue)
					{
						command.Parameters.AddWithValue("@desde", desde.Value);
					}

					if (hasta.HasValue)
					{
						command.Parameters.AddWithValue("@hasta", hasta.Value);
					}

					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Contrato c = new Contrato
							{
								Id = reader.GetInt32(nameof(Contrato.Id)),
								Desde = reader.GetDateTime("Desde"),
								Hasta = reader.GetDateTime("Hasta"),
								Valor = reader.GetDecimal("Valor"),
								Estado = reader.GetInt32("Estado"),
								InquilinoId = reader.GetInt32("InquilinoId"),
								Inquilino = new Inquilino
								{
									Id = reader.GetInt32("InquilinoId"),
									Nombre = reader.GetString("Nombre"),
									Apellido = reader.GetString("Apellido"),
								},
								InmuebleId = reader.GetInt32("InmuebleId"),
								Inmueble = new Inmueble
								{
									Id = reader.GetInt32("InmuebleId"),
									Tipo = reader.GetInt32("Tipo"),
									Direccion = reader.GetString("Direccion")
								}
							};
							res.Add(c);
						}
					}
				}
				connection.Close();
			}
			return res;
		}

		public List<Contrato> ObtenerVigentesInmueble(DateTime? desde, DateTime? hasta, int inmuebleId)
		{
			List<Contrato> res = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string query = @"SELECT 
					c.Id, c.InquilinoId, c.InmuebleId, c.Desde, c.Hasta, c.Valor, c.Estado, i.Nombre, i.Apellido, inm.Tipo, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.InquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id
					WHERE c.estado = 1
					AND c.InmuebleId = @inmuebleId 
					AND (desde >= @desde AND desde <= @hasta 
					OR hasta >= @desde AND hasta <= @hasta);";

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.Add("@inmuebleId", MySqlDbType.Int32).Value = inmuebleId;
					if (desde.HasValue)
					{
						command.Parameters.AddWithValue("@desde", desde.Value);
					}

					if (hasta.HasValue)
					{
						command.Parameters.AddWithValue("@hasta", hasta.Value);
					}

					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Contrato c = new Contrato
							{
								Id = reader.GetInt32(nameof(Contrato.Id)),
								Desde = reader.GetDateTime("Desde"),
								Hasta = reader.GetDateTime("Hasta"),
								Valor = reader.GetDecimal("Valor"),
								Estado = reader.GetInt32("Estado"),
								InquilinoId = reader.GetInt32("InquilinoId"),
								Inquilino = new Inquilino
								{
									Id = reader.GetInt32("InquilinoId"),
									Nombre = reader.GetString("Nombre"),
									Apellido = reader.GetString("Apellido"),
								},
								InmuebleId = reader.GetInt32("InmuebleId"),
								Inmueble = new Inmueble
								{
									Id = reader.GetInt32("InmuebleId"),
									Tipo = reader.GetInt32("Tipo"),
									Direccion = reader.GetString("Direccion")
								}
							};
							res.Add(c);
						}
					}
				}
				connection.Close();
			}
			return res;
		}
		
		public List<Contrato> ObtenerExpirados()
		{
			List<Contrato> res = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string query = @"SELECT 
					c.Id, c.InquilinoId, c.InmuebleId, c.Desde, c.Hasta, c.Valor, c.Estado, i.Nombre, i.Apellido, inm.Tipo, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.InquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id
					WHERE hasta < CURDATE() OR c.estado = 2 ";


				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Contrato c = new Contrato
							{
								Id = reader.GetInt32(nameof(Contrato.Id)),
								Desde = reader.GetDateTime("Desde"),
								Hasta = reader.GetDateTime("Hasta"),
								Valor = reader.GetDecimal("Valor"),
								Estado = reader.GetInt32("Estado"),
								InquilinoId = reader.GetInt32("InquilinoId"),
								Inquilino = new Inquilino
								{
									Id = reader.GetInt32("InquilinoId"),
									Nombre = reader.GetString("Nombre"),
									Apellido = reader.GetString("Apellido"),
								},
								InmuebleId = reader.GetInt32("InmuebleId"),
								Inmueble = new Inmueble
								{
									Id = reader.GetInt32("InmuebleId"),
									Tipo = reader.GetInt32("Tipo"),
									Direccion = reader.GetString("Direccion")
								}
							};
							res.Add(c);
						}
					}
				}
				connection.Close();
			}
			return res;
		}

		public Contrato? ObtenerPorId(int id)
		{
			Contrato? c = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					c.Id, c.InquilinoId, c.InmuebleId, c.Desde, c.Hasta, c.Valor, c.Estado, i.Nombre, i.Apellido, inm.Tipo, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.InquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id
					WHERE c.Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						c = new Contrato
						{
							Id = reader.GetInt32(nameof(Contrato.Id)),
							Desde = reader.GetDateTime("Desde"),
							Hasta = reader.GetDateTime("Hasta"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							InquilinoId = reader.GetInt32("InquilinoId"),
							Inquilino = new Inquilino
							{
								Id = reader.GetInt32("InquilinoId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
							},
							InmuebleId = reader.GetInt32("InmuebleId"),
							Inmueble = new Inmueble
							{
								Id = reader.GetInt32("InmuebleId"),
								Tipo = reader.GetInt32("Tipo"),
								Direccion = reader.GetString("Direccion")
							}
						};
					}
					connection.Close();
				}
			}
			return c;
		}

	}
}