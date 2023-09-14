using System.Data;
using MySql.Data.MySqlClient;

namespace ulp_net_inmobiliaria.Models
{
	public class RepositorioInmueble
	{
		protected readonly string connectionString;
		public RepositorioInmueble()
		{
			connectionString = "Server=localhost;User=root;Password=;Database=inmo_aavaldez;SslMode=none";
		}

		public int Alta(Inmueble i)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"INSERT INTO Inmuebles 
					(Uso, Tipo, Direccion, Ambientes, Superficie, Latitud, Longitud, Valor, PropietarioId)
					VALUES (@uso, @tipo, @direccion, @ambientes, @superficie, @latitud, @longitud, @valor, @PropietarioId);
					SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@uso", i.Tipo);
					command.Parameters.AddWithValue("@tipo", i.Tipo);
					command.Parameters.AddWithValue("@direccion", i.Direccion);
					command.Parameters.AddWithValue("@ambientes", i.Ambientes);
					command.Parameters.AddWithValue("@superficie", i.Superficie);
					command.Parameters.AddWithValue("@latitud", i.Latitud);
					command.Parameters.AddWithValue("@longitud", i.Longitud);
					command.Parameters.AddWithValue("@valor", i.Valor);
					command.Parameters.AddWithValue("@propietarioId", i.PropietarioId);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					i.Id = res;
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
				string sql = "DELETE FROM Inmuebles WHERE Id = @id";
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

		public int Modificacion(Inmueble i)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Inmuebles 
					SET Uso=@uso, Tipo=@tipo, Direccion=@direccion, Ambientes=@ambientes, Superficie=@superficie, Latitud=@latitud, Longitud=@longitud, Valor=@Valor, Estado=@Estado
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@uso", i.Uso);
					command.Parameters.AddWithValue("@tipo", i.Tipo);
					command.Parameters.AddWithValue("@direccion", i.Direccion);
					command.Parameters.AddWithValue("@ambientes", i.Ambientes);
					command.Parameters.AddWithValue("@superficie", i.Superficie);
					command.Parameters.AddWithValue("@latitud", i.Latitud);
					command.Parameters.AddWithValue("@longitud", i.Longitud);
					command.Parameters.AddWithValue("@valor", i.Valor);
					command.Parameters.AddWithValue("@estado", i.Estado);
					command.Parameters.AddWithValue("@id", i.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public List<Inmueble> ObtenerTodos()
		{
			List<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					{nameof(i.Id)}, i.Uso, i.Tipo, i.Direccion, i.Ambientes, i.Superficie, i.Latitud, i.Longitud, i.Valor, i.Estado, i.PropietarioId, p.Nombre, p.Apellido, p.Dni
					FROM Inmuebles i 
					INNER JOIN Propietarios p ON p.Id = i.PropietarioId";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble p = new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Uso = reader.GetInt32("Uso"),
							Tipo = reader.GetInt32("Tipo"),
							Direccion = reader.GetString("Direccion"),
							Ambientes = reader.GetInt32("Ambientes"),
							Superficie = reader.GetInt32("Superficie"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							PropietarioId = reader.GetInt32("PropietarioId"),
							Propietario = new Propietario
							{
								Id = reader.GetInt32("PropietarioId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
								Dni = reader.GetString("Dni")
							}
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public List<Inmueble> ObtenerDisponibles()
		{
			List<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					{nameof(i.Id)}, i.Uso, i.Tipo, i.Direccion, i.Ambientes, i.Superficie, i.Latitud, i.Longitud, i.Valor, i.Estado, i.PropietarioId, p.Nombre, p.Apellido, p.Dni
					FROM Inmuebles i 
					INNER JOIN Propietarios p ON p.Id = i.PropietarioId
					WHERE i.Estado = 1";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble p = new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Uso = reader.GetInt32("Uso"),
							Tipo = reader.GetInt32("Tipo"),
							Direccion = reader.GetString("Direccion"),
							Ambientes = reader.GetInt32("Ambientes"),
							Superficie = reader.GetInt32("Superficie"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							PropietarioId = reader.GetInt32("PropietarioId"),
							Propietario = new Propietario
							{
								Id = reader.GetInt32("PropietarioId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
								Dni = reader.GetString("Dni")
							}
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public List<Inmueble> ObtenerDisponiblesFecha(DateTime? desde, DateTime? hasta)
		{
			List<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"SELECT 
					{nameof(i.Id)}, i.Uso, i.Tipo, i.Direccion, i.Ambientes, i.Superficie, i.Latitud, i.Longitud, i.Valor, i.Estado, i.PropietarioId, p.Nombre, p.Apellido, p.Dni
				FROM inmuebles i
				INNER JOIN Propietarios p ON p.Id = i.PropietarioId
				WHERE i.Estado = 1
				AND NOT EXISTS (
					SELECT 1
					FROM contratos c
					WHERE c.InmuebleId = i.Id
					AND c.Estado = 1 ";

				if (desde != DateTime.MinValue && hasta != DateTime.MinValue) // Si estan ambos valores
				{
					query += "AND c.desde <= @hasta AND c.hasta >= @desde";
				}
				else if (desde != DateTime.MinValue && hasta == DateTime.MinValue) // Si esta solo el "desde"
				{
					query += "AND c.hasta >= @desde";
				}
				else if (hasta != DateTime.MinValue && desde == DateTime.MinValue) // Si esta solo el "hasta"
				{
					query += "AND c.desde <= @hasta";
				}

				query += ");";

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
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble inmueble = new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Uso = reader.GetInt32("Uso"),
							Tipo = reader.GetInt32("Tipo"),
							Direccion = reader.GetString("Direccion"),
							Ambientes = reader.GetInt32("Ambientes"),
							Superficie = reader.GetInt32("Superficie"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							PropietarioId = reader.GetInt32("PropietarioId"),
							Propietario = new Propietario
							{
								Id = reader.GetInt32("PropietarioId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
								Dni = reader.GetString("Dni")
							}
						};
						res.Add(inmueble);
					}
				}
				connection.Close();
			}
			return res;
		}
		
		public List<Inmueble> ObtenerTodosPropietario(int id)
		{
			List<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					{nameof(i.Id)}, i.Uso, i.Tipo, i.Direccion, i.Ambientes, i.Superficie, i.Latitud, i.Longitud, i.Valor, i.Estado, i.PropietarioId, p.Nombre, p.Apellido, p.Dni
					FROM Inmuebles i 
					INNER JOIN Propietarios p ON p.Id = i.PropietarioId
					WHERE i.PropietarioId=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble p = new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Uso = reader.GetInt32("Uso"),
							Tipo = reader.GetInt32("Tipo"),
							Direccion = reader.GetString("Direccion"),
							Ambientes = reader.GetInt32("Ambientes"),
							Superficie = reader.GetInt32("Superficie"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							PropietarioId = reader.GetInt32("PropietarioId")
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Inmueble? ObtenerPorId(int id)
		{
			Inmueble? p = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					{nameof(i.Id)}, i.Uso, i.Tipo, i.Direccion, i.Ambientes, i.Superficie, i.Latitud, i.Longitud, i.Valor, i.Estado, i.PropietarioId, p.Nombre, p.Apellido, p.Dni
					FROM Inmuebles i 
					INNER JOIN Propietarios p ON p.Id = i.PropietarioId
					WHERE i.Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Uso = reader.GetInt32("Uso"),
							Tipo = reader.GetInt32("Tipo"),
							Direccion = reader.GetString("Direccion"),
							Ambientes = reader.GetInt32("Ambientes"),
							Superficie = reader.GetInt32("Superficie"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
							Valor = reader.GetDecimal("Valor"),
							Estado = reader.GetInt32("Estado"),
							PropietarioId = reader.GetInt32("PropietarioId"),
							Propietario = new Propietario
							{
								Id = reader.GetInt32("PropietarioId"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
								Dni = reader.GetString("Dni")
							}
						};
					}
					connection.Close();
				}
			}
			return p;
		}

	}
}