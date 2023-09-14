using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers
{
	public class ContratosController : Controller
	{
		private readonly RepositorioContrato repo;
		public ContratosController()
		{
			repo = new RepositorioContrato();
		}

		// GET: ContratosController
		[Authorize]
		public ActionResult Index()
		{
			ViewBag.Error = TempData["Error"];
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Vigentes()
		{
			var lista = repo.ObtenerTodos();
			ViewBag.desde = false;
			ViewBag.hasta = false;
			return View("Index", lista);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Vigentes(DateTime desde, DateTime hasta)
		{
			if (desde == DateTime.MinValue && hasta == DateTime.MinValue)
			{
				TempData["Error"] = "Tiene que completar al menos una fecha.";
				return RedirectToAction("Index");
			}

			if (desde != DateTime.MinValue && hasta != DateTime.MinValue && desde > hasta)
			{
				TempData["Error"] = "La fecha inicial no puede ser mayor a la fecha final.";
				return RedirectToAction("Index");
			}

			var lista = repo.ObtenerVigentes(desde, hasta);
			ViewBag.desde = desde;
			ViewBag.hasta = hasta;
			return View("Index", lista);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Expirados()
		{
			var lista = repo.ObtenerExpirados();
			return View("Index", lista);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Details(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			RepositorioPago repoPago = new RepositorioPago();
			ViewBag.Pagos = repoPago.ObtenerTodosContrato(id);
			return View(entidad);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Create()
		{
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
			return View();
		}

		[HttpGet]
		[Authorize]
		public ActionResult Renovar(int id)
		{
			var contrato = repo.ObtenerPorId(id);
			return View(contrato);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Terminar(int id)
		{
			var contrato = repo.ObtenerPorId(id);
			RepositorioPago repoPago = new RepositorioPago();
			ViewBag.Pagos = repoPago.ObtenerTodosContrato(id);
			//Verificar la cantidad de meses que duraba el contrato
			var mesesContrato = ((contrato.Hasta.Year - contrato.Desde.Year) * 12) + contrato.Hasta.Month - contrato.Desde.Month;
			var mesesActuales = ((DateTime.Today.Year - contrato.Desde.Year) * 12) + DateTime.Today.Month - contrato.Desde.Month;
			
			if( mesesActuales < mesesContrato / 2 )
				ViewBag.multa = contrato.Valor*2;
			else 
				ViewBag.multa = contrato.Valor;

			decimal deuda = 0;
			if( mesesActuales - ViewBag.Pagos.Count > 0 )
				deuda = contrato.Valor * ( mesesActuales - ViewBag.Pagos.Count );

			ViewBag.multa = ViewBag.multa + deuda;

			return View(contrato);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Terminar(int id, Contrato Contrato)
		{
			Contrato? c = null;
			try
			{
				c = repo.ObtenerPorId(id);
				c.Hasta = DateTime.Today;
				c.Estado = 2;
				repo.Terminar(c);
				return RedirectToAction("Details", new { id });
			}
			catch (System.Exception)
			{
				throw;
			}

		}

		[HttpPost]
		[Authorize]
		public ActionResult Create(Contrato contrato)
		{
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			if (!ModelState.IsValid)
			{
				ViewBag.Error = "Faltan datos";
				ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
				ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
				return View(contrato);
			}

			if (contrato.Desde >= contrato.Hasta)
			{
				ViewBag.Error = "La fecha de inicio no puede ser mayor o igual a la fecha final.";
				ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
				ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
				return View(contrato);
			}

			var existenContratos = repo.ObtenerVigentesInmueble(contrato.Desde, contrato.Hasta, contrato.InmuebleId);
			if (existenContratos.Count > 0)
			{
				ViewBag.Error = "El inmueble ya tiene uno o más contratos vigente/s en las fechas seleccionadas.";
				ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
				ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
				return View(contrato);
			}			

			try
			{
				var lista = repo.Alta(contrato);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{

				throw;
			}
		}

		[HttpGet]
		[Authorize]
		public ActionResult Edit(int id)
		{
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
			Contrato? c = repo.ObtenerPorId(id);
			return View(c);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Update(int id, Contrato contrato)
		{
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			if (!ModelState.IsValid)
			{
				ViewBag.Error = "Faltan datos";
				ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
				ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
				return View(contrato);
			}

			if (contrato.Desde >= contrato.Hasta)
			{
				ViewBag.Error = "La fecha de inicio no puede ser mayor o igual a la fecha final.";
				ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
				ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
				return View(contrato);
			}

			var existenContratos = repo.ObtenerVigentesInmueble(contrato.Desde, contrato.Hasta, contrato.InmuebleId);
			if (existenContratos.Count > 0)
			{
				ViewBag.Error = "El inmueble ya tiene uno o más contratos vigente/s en las fechas seleccionadas.";
				ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
				ViewBag.Inmuebles = repoInmueble.ObtenerDisponibles();
				return View(contrato);
			}

			Contrato? c = null;
			try
			{
				c = repo.ObtenerPorId(id);
				c.InmuebleId = contrato.InmuebleId;
				c.InquilinoId = contrato.InquilinoId;
				c.Desde = contrato.Desde;
				c.Hasta = contrato.Hasta;
				c.Valor = contrato.Valor;
				repo.Modificacion(c);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		[HttpGet]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Contrato contrato)
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
	}
}