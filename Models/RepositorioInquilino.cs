using System.Data;
using MySql.Data.MySqlClient;

namespace ulp_net_inmobiliaria.Models
{
	public class RepositorioInquilino
	{
		protected readonly string connectionString;
		public RepositorioInquilino()
		{
			connectionString = "Server=localhost;User=root;Password=;Database=inmo_aavaldez;SslMode=none";
		}

		public int Alta(Inquilino p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"INSERT INTO Inquilinos 
					(Nombre, Apellido, Dni, Telefono, Email)
					VALUES (@nombre, @apellido, @dni, @telefono, @email);
					SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@telefono", p.Telefono == null? DBNull.Value : p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email == null? DBNull.Value : p.Email);
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
				string sql = "DELETE FROM Inquilinos WHERE Id = @id";
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
		
		public int Modificacion(Inquilino p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Inquilinos 
					SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@telefono", p.Telefono == null? DBNull.Value : p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email == null? DBNull.Value : p.Email);
					command.Parameters.AddWithValue("@id", p.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Inquilino> ObtenerTodos()
		{
			IList<Inquilino> res = new List<Inquilino>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					Id, Nombre, Apellido, Dni, Telefono, Email
					FROM Inquilinos";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inquilino p = new Inquilino
						{
							Id = reader.GetInt32(nameof(Inquilino.Id)),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Dni = reader.GetString("Dni"),
							Telefono = reader["Telefono"] == DBNull.Value? "" : reader.GetString("Telefono"),
							Email = reader["Email"] == DBNull.Value? "" : reader.GetString("Email"),
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Inquilino ObtenerPorId(int id)
		{
			Inquilino p = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT Id, Nombre, Apellido, Dni, Telefono, Email
					FROM Inquilinos
					WHERE Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Inquilino
						{
							Id = reader.GetInt32(nameof(Inquilino.Id)),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Dni = reader.GetString("Dni"),
							Telefono = reader["Telefono"] == DBNull.Value? "" : reader.GetString("Telefono"),
							Email = reader["Email"] == DBNull.Value? "" : reader.GetString("Email"),
						};
					}
					connection.Close();
				}
			}
			return p;
		}
	}
}