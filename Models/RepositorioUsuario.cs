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
					(Rol, Nombre, Apellido, Email, Password, Avatar, Estado)
					VALUES (@rol, @nombre, @apellido, @email, @password, @avatar, @estado);
					SELECT LAST_INSERT_ID();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@rol", p.Rol);
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@email", p.Email);
					command.Parameters.AddWithValue("@password", p.Password);
					if(String.IsNullOrEmpty(p.Avatar)){
						command.Parameters.AddWithValue("@avatar", DBNull.Value);
					} else {
						command.Parameters.AddWithValue("@avatar", p.Avatar);
					}
					command.Parameters.AddWithValue("@estado", p.Estado);
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
		
		public int Modificacion(Usuario p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Usuarios 
					(Rol, Nombre, Apellido, Email, Password, Avatar, Estado)
					VALUES (@rol, @nombre, @apellido, @email, @password, @avatar, @estado);
					SET Rol=@rol, Nombre=@nombre, Apellido=@apellido, Email=@email, Password=@password, Avatar=@avatar, Estado=@estado
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@rol", p.Rol);
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@email", p.Email);
					command.Parameters.AddWithValue("@password", p.Password);
					command.Parameters.AddWithValue("@avatar", p.Avatar);
					command.Parameters.AddWithValue("@estado", p.Estado);
					command.Parameters.AddWithValue("@id", p.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		
		public IList<Usuario> ObtenerTodos()
		{
			IList<Usuario> res = new List<Usuario>();
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
							Password = reader.GetString("Password"),
							Avatar = reader.GetString("Avatar"),
							Estado = reader.GetInt32("Estado"),
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}
		
		public Usuario ObtenerPorId(int id)
		{
			Usuario u = null;
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
							Avatar = reader.GetString("Avatar"),
							Estado = reader.GetInt32("Estado")
						};
					}
					connection.Close();
				}
			}
			return u;
		}

		public Usuario ObtenerPorEmail(string email)
		{
			Usuario? u = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT
					Id, Nombre, Apellido, Avatar, Email, Clave, Rol FROM Usuarios
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
							Avatar = reader.GetString("Avatar"),
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