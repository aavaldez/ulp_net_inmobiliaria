using System.Data;
using MySql.Data.MySqlClient;

namespace ulp_net_inmobiliaria.Models
{
	public class RepositorioPago
	{
		protected readonly string connectionString;
		public RepositorioPago()
		{
			connectionString = "Server=localhost;User=root;Password=;Database=inmo_aavaldez;SslMode=none";
		}

		public int Alta(Pago p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"INSERT INTO Pagos 
					(ContratoId, Numero, Fecha, Importe)
					VALUES (@contratoId, @numero, @fecha, @importe);
					SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@contratoId", p.ContratoId);
					command.Parameters.AddWithValue("@numero", p.Numero);
					command.Parameters.AddWithValue("@fecha", p.Fecha);
					command.Parameters.AddWithValue("@importe", p.Importe);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					p.Id = res;
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
				string sql = "DELETE FROM Pagos WHERE Id = @id";
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
		
		public int Modificacion(Pago p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Inmuebles 
					SET Numero=@numero, Fecha=@fecha, Importe=@importe
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@numero", p.Numero);
					command.Parameters.AddWithValue("@fecha", p.Fecha);
					command.Parameters.AddWithValue("@importe", p.Importe);
					command.Parameters.AddWithValue("@id", p.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Pago> ObtenerTodos()
		{
			IList<Pago> res = new List<Pago>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					Id, ContratoId, Numero, Fecha, Importe
					FROM Pagos p";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Pago p = new Pago
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Numero = reader.GetInt32("Numero"),
							Fecha = reader.GetDateTime("Hasta"),
							Importe = reader.GetDecimal("Importe"),
							ContratoId = reader.GetInt32("ContratoId"),
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public IList<Pago> ObtenerTodosContrato(int contratoId)
		{
			IList<Pago> res = new List<Pago>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					Id, ContratoId, Numero, Fecha, Importe
					FROM Pagos p
					WHERE ContratoId = @contratoId";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = contratoId;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Pago p = new Pago
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Numero = reader.GetInt32("Numero"),
							Fecha = reader.GetDateTime("Hasta"),
							Importe = reader.GetDecimal("Importe"),
							ContratoId = reader.GetInt32("ContratoId")
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Pago ObtenerPorId(int id)
		{
			Pago p = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					Id, ContratoId, Numero, Fecha, Importe
					FROM Pagos p
					WHERE c.Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Pago
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							Numero = reader.GetInt32("Numero"),
							Fecha = reader.GetDateTime("Hasta"),
							Importe = reader.GetDecimal("Importe"),
							ContratoId = reader.GetInt32("ContratoId")
						};
					}
					connection.Close();
				}
			}
			return p;
		}

	}
}