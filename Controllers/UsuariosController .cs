using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class UsuariosController : Controller
	{
		// GET: UsuariosController
		public ActionResult Index()
		{
			RepositorioUsuario repo = new RepositorioUsuario();
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			RepositorioUsuario repo = new RepositorioUsuario();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Usuario usuario)
		{
			try
			{
				RepositorioUsuario repo = new RepositorioUsuario();
				var lista = repo.Alta(usuario);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			RepositorioUsuario repo = new RepositorioUsuario();
			Usuario u = repo.ObtenerPorId(id);
			return View(u);
		}

		[HttpPost]
		public ActionResult Update(int id, Usuario usuario)
		{
			Usuario p;
			try
			{
				RepositorioUsuario repo = new RepositorioUsuario();
				p = repo.ObtenerPorId(id);
				p.Rol = usuario.Rol;
				p.Nombre = usuario.Nombre;
				p.Apellido = usuario.Apellido;
				p.Email = usuario.Email;
				p.Password = usuario.Password;
				p.Avatar = usuario.Avatar;
				p.Estado = usuario.Estado;
				repo.Modificacion(p);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			RepositorioUsuario repo = new RepositorioUsuario();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				RepositorioUsuario repo = new RepositorioUsuario();
				var lista = repo.Baja(id);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}
	}
}