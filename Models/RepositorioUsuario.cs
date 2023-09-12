using System.Data;
using MySql.Data.MySqlClient;

namespace ulp_net_inmobiliaria.Models
{
	public class RepositorioUsuario
	{
		protected readonly string connectionString;
		public RepositorioUsuario()
		{
			connectionString = "Server=localhost;User=root;Password=;Database=inmo_aavaldez;SslMode=none";
		}

		public int Alta(Usuario p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"INSERT INTO Usuarios 
					(Rol, Nombre, Apellido, Email, Password, Avatar)
					VALUES (@rol, @nombre, @apellido, @email, @password, @avatar);
					SELECT LAST_INSERT_ID();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@rol", p.Rol);
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@email", p.Email);
					command.Parameters.AddWithValue("@password", p.Password);
					if (String.IsNullOrEmpty(p.Avatar))
					{
						command.Parameters.AddWithValue("@avatar", DBNull.Value);
					}
					else
					{
						command.Parameters.AddWithValue("@avatar", p.Avatar);
					}
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
				string sql = "DELETE FROM Usuarios WHERE Id = @id";
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

		public int Modificacion(Usuario u)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Usuarios 
					SET Rol=@rol, Nombre=@nombre, Apellido=@apellido, Email=@email, Password=@password, Avatar=@avatar
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@rol", u.Rol);
					command.Parameters.AddWithValue("@nombre", u.Nombre);
					command.Parameters.AddWithValue("@apellido", u.Apellido);
					command.Parameters.AddWithValue("@email", u.Email);
					command.Parameters.AddWithValue("@password", u.Password);
					command.Parameters.AddWithValue("@avatar", u.Avatar);
					command.Parameters.AddWithValue("@id", u.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public List<Usuario> ObtenerTodos()
		{
			List<Usuario> res = new List<Usuario>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT 
					Id, Rol, Nombre, Apellido, Email, Password, Avatar, Estado
					FROM Usuarios";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Usuario p = new Usuario
						{
							Id = reader.GetInt32(nameof(Usuario.Id)),
							Rol = reader.GetInt32("Rol"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Email = reader.GetString("Email"),
							Avatar = reader["Avatar"] == DBNull.Value ? "" : reader.GetString("Avatar"),
							Estado = reader.GetInt32("Estado")
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Usuario? ObtenerPorId(int id)
		{
			Usuario? u = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT Id, Rol, Nombre, Apellido, Email, Password, Avatar, Estado
					FROM Usuarios
					WHERE Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						u = new Usuario
						{
							Id = reader.GetInt32(nameof(Usuario.Id)),
							Rol = reader.GetInt32("Rol"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Email = reader.GetString("Email"),
							Avatar = reader["Avatar"] == DBNull.Value ? "" : reader.GetString("Avatar"),
							Estado = reader.GetInt32("Estado")
						};
					}
					connection.Close();
				}
			}
			return u;
		}

		public Usuario? ObtenerPorEmail(string? email)
		{
			Usuario? u = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT
					Id, Nombre, Apellido, Avatar, Email, Password, Rol, Estado
					FROM Usuarios
					WHERE Email=@email";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						u = new Usuario
						{
							Id = reader.GetInt32("Id"),
							Rol = reader.GetInt32("Rol"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Email = reader.GetString("Email"),
							Password = reader.GetString("Password"),
							Avatar = reader["Avatar"] == DBNull.Value ? "" : reader.GetString("Avatar"),
							Estado = reader.GetInt32("Estado")
						};
					}
					connection.Close();
				}
			}
			return u;
		}
	}
}