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
					VALUES (@PropietarioId, @InmuebleId, @Desde, @Hasta, @Valor);
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
		
		public int Baja(int id)
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
		
		public int Modificacion(Contrato c)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Inmuebles 
					SET Desde=@desde, Hasta=@hasta, Valor=@valor
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
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

		public IList<Contrato> ObtenerTodos()
		{
			IList<Contrato> res = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					c.Id, InquilinoId, InmuebleId, Desde, Hasta, Valor, i.Nombre, i.Apellido, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.inquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.inmuebleId = inm.Id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Contrato c = new Contrato
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Desde = reader.GetDateTime("Desde"),
							Hasta = reader.GetDateTime("Hasta"),
							Valor = reader.GetDecimal("Valor"),
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
								Id = reader.GetInt32("PropetarioId"),
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

		public Contrato ObtenerPorId(int id)
		{
			Contrato c = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					c.Id, InquilinoId, InmuebleId, Desde, Hasta, Valor, i.Nombre, i.Apellido, inm.Direccion
					FROM Contratos c 
					INNER JOIN Inquilinos i ON c.inquilinoId = i.Id
					INNER JOIN Inmuebles inm ON c.inmuebleId = inm.Id
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
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Desde = reader.GetDateTime("Desde"),
							Hasta = reader.GetDateTime("Hasta"),
							Valor = reader.GetDecimal("Valor"),
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
								Id = reader.GetInt32("PropetarioId"),
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