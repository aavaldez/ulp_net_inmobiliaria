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
					(Tipo, Direccion, Ambientes, Superficie, Latitud, Longitud, Valor, PropietarioId)
					VALUES (@tipo, @direccion, @ambientes, @superficie, @latitud, @longitud, @valor, @PropietarioId);
					SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
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
					SET Tipo=@tipo, Direccion=@direccion, Ambientes=@ambientes, Superficie=@superficie, Latitud=@latitud, Longitud=@longitud, Valor=@Valor, Estado=@Estado
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
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

		public IList<Inmueble> ObtenerTodos()
		{
			IList<Inmueble> res = new List<Inmueble>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					{nameof(i.Id)}, Tipo, Direccion, Ambientes, Superficie, Latitud, Longitud, Valor, Estado, PropietarioId, p.Nombre, p.Apellido, p.Dni
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

		public Inmueble ObtenerPorId(int id)
		{
			Inmueble p = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					{nameof(i.Id)}, Tipo, Direccion, Ambientes, Superficie, Latitud, Longitud, Valor, Estado, PropietarioId, p.Nombre, p.Apellido, p.Dni
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
							Direccion = reader.GetString("Direccion"),
							Ambientes = reader.GetInt32("Ambientes"),
							Superficie = reader.GetInt32("Superficie"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
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