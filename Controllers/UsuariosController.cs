using ulp_net_inmobiliaria.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class UsuariosController : Controller
	{
		private readonly IWebHostEnvironment environment;
		private readonly RepositorioUsuario repo;

		public UsuariosController(IWebHostEnvironment environment)
		{
			this.environment = environment;
			this.repo = new RepositorioUsuario();
		}

		// GET: UsuariosController
		[Authorize(Policy = "Administrador")]
		public ActionResult Index()
		{
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		[Authorize(Policy = "Administrador")]
		public ActionResult Details(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpGet]
		[Authorize(Policy = "Administrador")]
		public ActionResult Create()
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public ActionResult Create(Usuario usuario)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
			}
			try
			{
				// Comprobar que el email ingresado no existe en la BD
				var usuarioDB = repo.ObtenerPorEmail(usuario.Email);
				if (usuarioDB != null)
				{
					ModelState.AddModelError("Email", "El email ingresado ya existe");
					ViewBag.Error = "El email ingresado ya existe";
					ViewBag.Roles = Usuario.ObtenerRoles();
					return View();
				}
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: usuario.Password,
					salt: System.Text.Encoding.ASCII.GetBytes("1234567890"),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 10000,
					numBytesRequested: 256 / 8
				));
				usuario.Password = hashed;
				usuario.Rol = User.IsInRole("Administrador") ? usuario.Rol : (int)enRoles.Empleado;
				var res = repo.Alta(usuario);
				if (usuario.AvatarFile != null && usuario.Id > 0)
				{
					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "Uploads");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					//Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
					string fileName = "avatar_" + usuario.Id + Path.GetExtension(usuario.AvatarFile.FileName);
					string pathCompleto = Path.Combine(path, fileName);
					usuario.Avatar = Path.Combine("/Uploads", fileName);
					// Esta operación guarda la foto en memoria en la ruta que necesitamos
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						usuario.AvatarFile.CopyTo(stream);
					}
					repo.Modificacion(usuario);
				}
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		[Authorize]
		public ActionResult Perfil()
		{
			ViewData["Title"] = "Mi perfil";
			var usuario = repo.ObtenerPorEmail(User?.Identity?.Name);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View("Edit", usuario);
		}

		[HttpGet]
		[Authorize(Policy = "Administrador")]
		public ActionResult Edit(int id)
		{
			Usuario u = repo.ObtenerPorId(id);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(u);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult Update(int id, Usuario usuario)
		{
			/*
			Usuario p;
			try
			{
				p = repo.ObtenerPorId(id);
				p.Rol = usuario.Rol;
				p.Nombre = usuario.Nombre;
				p.Apellido = usuario.Apellido;
				p.Email = usuario.Email;
				repo.Modificacion(p);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
			*/

			var usuarioDB = repo.ObtenerPorId(usuario.Id);

			if (!ModelState.IsValid)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View(usuarioDB);
			}

			try
			{
				var usuarioEmailCheck = repo.ObtenerPorEmail(usuario.Email);
				if (usuarioEmailCheck.Id > 0 && usuarioEmailCheck.Id != usuario.Id)
				{
					ModelState.AddModelError("Email", "El email ingresado ya existe");
					ViewBag.Error = "Email no puede coincidir con otro usuario";
					ViewBag.Roles = Usuario.ObtenerRoles();
					return View(usuarioDB);
				}

				usuarioDB.Rol = User.IsInRole("Administrador") ? usuario.Rol : (int)enRoles.Empleado;
				usuarioDB.Nombre = usuario.Nombre;
				usuarioDB.Apellido = usuario.Apellido;
				usuarioDB.Email = usuario.Email;
				repo.Modificacion(usuarioDB);

				if (usuario.AvatarFile != null && usuario.Id > 0)
				{
					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "Uploads");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					if (usuarioDB.Avatar != null) {
						// Borrar la foto si existe
						string existingFilePath = Path.Combine(path, $"avatar_{usuario.Id}{Path.GetExtension(usuarioDB.Avatar)}");
						if (System.IO.File.Exists(existingFilePath))
						{
							System.IO.File.Delete(existingFilePath);
						}
					}

					string fileName = $"avatar_{usuario.Id + Path.GetExtension(usuario.AvatarFile.FileName)}";
					string pathCompleto = Path.Combine(path, fileName);
					usuario.Avatar = Path.Combine("/Uploads", fileName);

					// Guardar la foto en la memoria
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						usuario.AvatarFile.CopyTo(stream);
					}
					repo.ModificarAvatar(usuario);
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw;
			}
		}

		public ActionResult CambiarPassword(){
			return RedirectToAction(nameof(Index));
		}

		public ActionResult CambiarAvatar(){
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Usuario usuario)
		{
			try
			{
				var lista = repo.Baja(id);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		[Authorize]
		public ActionResult Avatar()
		{
			var u = repo.ObtenerPorEmail(User?.Identity?.Name);
			string fileName = "avatar_" + u.Id + Path.GetExtension(u.Avatar);
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			string pathCompleto = Path.Combine(path, fileName);

			//leer el archivo
			byte[] fileBytes = System.IO.File.ReadAllBytes(pathCompleto);
			//devolverlo
			return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
		}

		[Authorize]
		public string AvatarBase64()
		{
			var u = repo.ObtenerPorEmail(User?.Identity?.Name);
			string fileName = "avatar_" + u.Id + Path.GetExtension(u.Avatar);
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			string pathCompleto = Path.Combine(path, fileName);

			//leer el archivo
			byte[] fileBytes = System.IO.File.ReadAllBytes(pathCompleto);
			//devolverlo
			return Convert.ToBase64String(fileBytes);
		}

		[Authorize]
		[HttpPost("[controller]/[action]/{fileName}")]
		public ActionResult FromBase64([FromBody] string imagen, [FromRoute] string fileName)
		{
			//arma el path
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			string pathCompleto = Path.Combine(path, fileName);
			//convierto a arreglo de bytes
			var bytes = Convert.FromBase64String(imagen);
			//lo escribe
			System.IO.File.WriteAllBytes(pathCompleto, bytes);
			return Ok();
		}

		[Authorize]
		public ActionResult Foto()
		{
			try
			{
				var u = repo.ObtenerPorEmail(User?.Identity?.Name);
				var stream = System.IO.File.Open(
						Path.Combine(environment.WebRootPath, u.Avatar.Substring(1)),
						FileMode.Open,
						FileAccess.Read);
				var ext = Path.GetExtension(u.Avatar);
				return new FileStreamResult(stream, $"image/{ext.Substring(1)}");
			}
			catch (Exception)
			{
				throw;
			}
		}

		[Authorize]
		public ActionResult Datos()
		{
			try
			{
				var u = repo.ObtenerPorEmail(User.Identity.Name);
				string buffer = "Nombre;Apellido;Email" + Environment.NewLine +
						$"{u.Nombre};{u.Apellido};{u.Email}";
				var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(buffer));
				var res = new FileStreamResult(stream, "text/plain");
				res.FileDownloadName = "Datos.csv";
				return res;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[AllowAnonymous]
		// GET: Usuarios/Login/
		public ActionResult Login(string returnUrl)
		{
			TempData["returnUrl"] = returnUrl;
			return View();
		}

		// POST: Usuarios/Login/
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginView login)
		{
			try
			{
				var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();
				if (ModelState.IsValid)
				{
					string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Password,
						salt: System.Text.Encoding.ASCII.GetBytes("1234567890"),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 10000,
						numBytesRequested: 256 / 8));

					var usuario = repo.ObtenerPorEmail(login.Email);
					if (usuario == null || usuario.Password != hashed)
					{
						ModelState.AddModelError("", "El email o la contraseña son incorrectos");
						TempData["returnUrl"] = returnUrl;
						return View();
					}

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, usuario.Email),
						new Claim("FullName", $"{usuario.Nombre} {usuario.Apellido}"),
						new Claim(ClaimTypes.Role, usuario.RolNombre),
						new Claim("Id", usuario.Id.ToString()),
						new Claim("Avatar", usuario.Avatar ?? "")
					};

					var claimsIdentity = new ClaimsIdentity(
							claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
							CookieAuthenticationDefaults.AuthenticationScheme,
							new ClaimsPrincipal(claimsIdentity));
					TempData.Remove("returnUrl");
					return Redirect(returnUrl);
				}
				TempData["returnUrl"] = returnUrl;
				return View();
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}

		// GET: /salir
		[Route("salir", Name = "logout")]
		public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}