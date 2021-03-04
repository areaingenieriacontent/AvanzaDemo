using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;

namespace Plenamente.Controllers
{
	/// <summary>
	/// Controlador encargado del crud de plan de trabajo 
	/// </summary>
	public class PlandeTrabajoController : Controller
	{
		/// <summary>
		/// Variables estandar para la paginacion
		/// </summary>
		private readonly int _RegistrosPorPagina = 10;
		private PaginadorGenerico<PlandeTrabajo> _PaginadorCustomers;
		private readonly int _RegistrosPorPaginaActividades = 5;
		private PaginadorGenerico<ActividadesAsignadasPlanDeTrabajoViewModel> _PaginadorCustomersActividades;
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: PlandeTrabajo
		/// <summary>
		/// Lista y pagina los planes de trabajo de una empresa 
		/// </summary>
		/// <param name="pagina">la pagina actual en la que esta parado el usuario</param>
		/// <returns>retorna un objeto de tipo paginadorcustomer que contiene la paginacion y el objeto a paginar</returns>
		public ActionResult Index(int pagina = 1)
		{
			int _TotalRegistros = 0;
			var tb_PlandeTrabajo = db.Tb_PlandeTrabajo.Where(p => p.Emp_Id == AccountData.NitEmpresa).ToList();
			_TotalRegistros = tb_PlandeTrabajo.Count();
			tb_PlandeTrabajo = tb_PlandeTrabajo.Skip((pagina - 1) * _RegistrosPorPagina)
											   .Take(_RegistrosPorPagina)
											   .ToList();
			int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
			_PaginadorCustomers = new PaginadorGenerico<PlandeTrabajo>()
			{
				RegistrosPorPagina = _RegistrosPorPagina,
				TotalRegistros = _TotalRegistros,
				TotalPaginas = _TotalPaginas,
				PaginaActual = pagina,
				Resultado = tb_PlandeTrabajo
			};
			return View(_PaginadorCustomers);
		}
		/// <summary>
		/// Lista los planes de trabajos con su correspondiente actividades que tiene una empresa
		/// </summary>
		/// <param name="id">Recibe el id del plan de trabajo a buscar </param>			
		/// <returns>Retorna el viewmodel PlandetrabajoActividadesViewModel </returns>
		// GET: PlandeTrabajo/Details/5
		public ActionResult Detalles(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var plantrabajo = db.Tb_PlandeTrabajo.Find(id);
			var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
			List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
			List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
			foreach (var item in actividadesEmpresa)
			{
				var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
				if (useractividadpt.Count <= 0)
				{
					actiCumplimientoSinAsignar.Add(item);
				}
				else
				{
					var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == id && c.Acum_Id == item.Acum_Id).ToList();
					if (cumplimientoPlanDetrabajo.Count > 0)
					{

						var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
						var nombre = db.Users.Find(user.Id);

						ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
						{
							IdUserPlanDeTrabajoActividad = user.Uspl_Id,
							NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
							IdPlantTrabajo = plantrabajo.Plat_Id,
							IdActiCumplimiento = item.Acum_Id,
							DescripcionCumplimiento = item.Acum_Desc,
							NombrePlanTrabajo = plantrabajo.Plat_Nom

						};
						actiCumplimientoAsignados.Add(temp);
					}

				}
			}
			PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
			{
				NombrePlanTrabajo = plantrabajo.Plat_Nom,
				IdPlantTrabajo = plantrabajo.Plat_Id,
				FechaInicio = plantrabajo.FechaInicio,
				FechaFin = plantrabajo.FechaFin
			};
			ViewBag.actividadesAsignadas = actiCumplimientoAsignados;
			return View(plandetrabajoActividades);
		}
		/// <summary>
		/// Lista y selecciona la empresa a la que se esta logeada
		/// </summary>
		/// <returns>retorna a la vista</returns>
		// GET: PlandeTrabajo/Create
		public ActionResult Create()
		{
			ViewBag.Emp_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
			return View();
		}

		// POST: PlandeTrabajo/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
		/// <summary>
		/// Crea el plan de trabajo recibiendo un objeto de tipo de plandetrabajo validando que no se encuentro repetido el plan de tabajo
		/// </summary>
		/// <param name="plandeTrabajo">recibe un objeto de tipo plandetrabajo para crearlo en la bd </param>
		/// <returns>retorna el objeto si no se crea adecuadamente o direcciona al index si logra crearlo</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Plat_Id,Plat_Nom,Emp_Id,FechaInicio,FechaFin")] PlandeTrabajo plandeTrabajo)
		{
			if (ModelState.IsValid)
			{
				var a = plandeTrabajo.FechaInicio.Date;
				plandeTrabajo.FechaCreacion = DateTime.Now;
				plandeTrabajo.FechaActualizacion = DateTime.Now;
				var planesTrabajo = db.Tb_PlandeTrabajo.Where(c => c.Emp_Id == plandeTrabajo.Emp_Id).ToList();
				var nombreplan = db.Tb_PlandeTrabajo.Where(c => c.Emp_Id == plandeTrabajo.Emp_Id && c.Plat_Nom == plandeTrabajo.Plat_Nom).ToList();
				if (nombreplan.Count > 0)
				{
					ViewBag.TextError = "Nombre del plan de trabajo repetido";
					return View(plandeTrabajo);
				}
				if (plandeTrabajo.FechaFin < plandeTrabajo.FechaInicio)
				{
					ViewBag.TextError = "La fecha de fin es menor que la fecha de incio , por favor seleccionar una fecha valida";
					return View(plandeTrabajo);
				}
				foreach (var item in planesTrabajo)
				{
					if (Valrangofecha(item.FechaInicio.Date, item.FechaFin.Date, plandeTrabajo.FechaInicio.Date, plandeTrabajo.FechaFin.Date))
					{
						ViewBag.TextError = "Esta fecha ya esta siendo usada para el plan de trabajo = " + item.Plat_Nom;
						return View(plandeTrabajo);
					}
				}
				db.Tb_PlandeTrabajo.Add(plandeTrabajo);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(plandeTrabajo);
		}
		/// <summary>
		/// Metodo encargado de verificar que una fecha no este en un rango de fechas 
		/// </summary>
		/// <param name="FechaInicialRango"> fecha de inicio de rango </param>
		/// <param name="FechaFinalRango">fecha de fin de rango</param>
		/// <param name="FechaIncioPlan">fecha inicio a verificar</param>
		/// <param name="FechaFinalPlan">fecha fin a verficiar</param>
		/// <returns></returns>
		private bool Valrangofecha(DateTime FechaInicialRango, DateTime FechaFinalRango, DateTime FechaIncioPlan, DateTime FechaFinalPlan)
		{
			while (FechaInicialRango <= FechaFinalRango) //Asumiendo un ámbito inclusivo de ambos valores.
			{
				if (FechaInicialRango == FechaIncioPlan)
				{
					return true;
				}
				else if (FechaInicialRango == FechaFinalPlan)
				{
					return true;
				}
				FechaInicialRango += new TimeSpan(1, 0, 0, 0);
			}
			return false;
		}

		// GET: PlandeTrabajo/Edit/5
		/// <summary>
		/// Lista el plan de trabajo si es encontrado mediante el id que recibe si no es nullable
		/// </summary>
		/// <param name="id">id del plan de trabajo si no es nullable</param>
		/// <returns>retorna el plan de trabajo a editar</returns>
		public ActionResult Editar(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
			if (plandeTrabajo == null)
			{
				return HttpNotFound();
			}
			return View(plandeTrabajo);
		}

		// POST: PlandeTrabajo/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
		/// <summary>
		/// Mediante el objeto plandetrabajo que recibe se hacen las modificaciones por el usuario si el objeto es valido
		/// </summary>
		/// <param name="plandeTrabajo">recibe el objeto plandetrabajo a modificar</param>
		/// <returns>retorna el plan de trabajo si no fue modificado correctamente , si fue modificado redirecciona al index</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Editar([Bind(Include = "Plat_Id,Plat_Nom,Emp_Id,FechaCreacion,FechaInicio,FechaFin")] PlandeTrabajo plandeTrabajo)
		{
			if (ModelState.IsValid)
			{

				plandeTrabajo.FechaActualizacion = DateTime.Now;
				var Planesdetrabajo = db.Tb_PlandeTrabajo.Where(c => c.Plat_Nom == plandeTrabajo.Plat_Nom && c.Emp_Id == AccountData.NitEmpresa && c.Plat_Id != plandeTrabajo.Plat_Id).ToList();
				var planesTrabajo = db.Tb_PlandeTrabajo.Where(c => c.Emp_Id == plandeTrabajo.Emp_Id && c.Plat_Id != plandeTrabajo.Plat_Id).ToList();
				if (Planesdetrabajo.Count <= 0)
				{
					if (plandeTrabajo.FechaFin < plandeTrabajo.FechaInicio)
					{
						ViewBag.TextError = "La fecha de fin es menor que la fecha de incio , por favor seleccionar una fecha valida";
						return View(plandeTrabajo);
					}
					foreach (var item in planesTrabajo)
					{
						if (Valrangofecha(item.FechaInicio.Date, item.FechaFin.Date, plandeTrabajo.FechaInicio.Date, plandeTrabajo.FechaFin.Date))
						{
							ViewBag.TextError = "Esta fecha ya esta siendo usada para el plan de trabajo = " + item.Plat_Nom;
							return View(plandeTrabajo);
						}
					}
					db.Entry(plandeTrabajo).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.TextError = "Nombre del plan de trabajo repetido";
					return View(plandeTrabajo);
				}


			}
			return View(plandeTrabajo);
		}

		// GET: PlandeTrabajo/Delete/5
		/// <summary>
		/// lista el pland e trabajo y las actividades que tiene a eliminar 
		/// </summary>
		/// <param name="id">recibe el id del objeto a eliminar</param>
		/// <returns>retorna el viewmodel PlandetrabajoActividadesViewModel a eliminar </returns>
		public ActionResult Eliminar(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var plantrabajo = db.Tb_PlandeTrabajo.Find(id);
			var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
			List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
			List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
			foreach (var item in actividadesEmpresa)
			{
				var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
				if (useractividadpt.Count <= 0)
				{
					actiCumplimientoSinAsignar.Add(item);
				}
				else
				{
					var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == id && c.Acum_Id == item.Acum_Id).ToList();
					if (cumplimientoPlanDetrabajo.Count > 0)
					{

						var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
						var nombre = db.Users.Find(user.Id);

						ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
						{
							IdUserPlanDeTrabajoActividad = user.Uspl_Id,
							NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
							IdPlantTrabajo = plantrabajo.Plat_Id,
							IdActiCumplimiento = item.Acum_Id,
							DescripcionCumplimiento = item.Acum_Desc,
							NombrePlanTrabajo = plantrabajo.Plat_Nom

						};
						actiCumplimientoAsignados.Add(temp);
					}

				}
			}
			PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
			{
				NombrePlanTrabajo = plantrabajo.Plat_Nom,
				IdPlantTrabajo = plantrabajo.Plat_Id,
				FechaInicio = plantrabajo.FechaInicio,
				FechaFin = plantrabajo.FechaFin
			};
			ViewBag.actividadesAsignadas = actiCumplimientoAsignados;
			return View(plandetrabajoActividades);
		}

		// POST: PlandeTrabajo/Delete/5
		/// <summary>
		/// Mediante el id del plan de trabajo elimina de la bd el plan de trabajo y borra la relacion con las actividades que tenia 
		/// </summary>
		/// <param name="id">recibe el id del plan de trabajo</param>
		/// <returns>retorna al index despues de remover el objeto plandetrabajo</returns>
		[HttpPost, ActionName("Eliminar")]
		[ValidateAntiForgeryToken]
		public ActionResult ElimitarConfirmado(int id)
		{
			PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
			var usuariosPlandetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == id && c.Emp_Id == plandeTrabajo.Emp_Id).ToList();
			foreach (var item in usuariosPlandetrabajo)
			{
				db.Tb_UsersPlandeTrabajo.Remove(item);
			}
			db.Tb_PlandeTrabajo.Remove(plandeTrabajo);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		/// <summary>
		/// lista y pagina las actividades que tiene un plan de trabajo dependiendo de la pagina 
		/// </summary>
		/// <param name="IdPlantTrabajo">recibe el id del plan de trabajo</param>
		/// <param name="pagina">recibe la pagina actual en la que se esta paginando</param>
		/// <returns>retorna un objeto de tipo paginadorcustomer que contiene la paginacion y el objeto a paginar</returns>
		public ActionResult ActividadesPlanTrabajo(int IdPlantTrabajo, int pagina = 1)
		{
			var plantrabajo = db.Tb_PlandeTrabajo.Find(IdPlantTrabajo);
			ViewBag.users = new SelectList(db.Users.Where(c => c.Empr_Nit == AccountData.NitEmpresa), "Id", "Pers_Nom1");
			var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
			List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
			List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
			foreach (var item in actividadesEmpresa)
			{
				var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
				if (useractividadpt.Count <= 0)
				{
					actiCumplimientoSinAsignar.Add(item);
				}
				else
				{
					var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == IdPlantTrabajo && c.Acum_Id == item.Acum_Id).ToList();
					if (cumplimientoPlanDetrabajo.Count > 0)
					{

						var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
						var nombre = db.Users.Find(user.Id);

						ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
						{
							IdUserPlanDeTrabajoActividad = user.Uspl_Id,
							NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
							IdPlantTrabajo = plantrabajo.Plat_Id,
							IdActiCumplimiento = item.Acum_Id,
							DescripcionCumplimiento = item.Acum_Desc,
							NombrePlanTrabajo = plantrabajo.Plat_Nom

						};
						actiCumplimientoAsignados.Add(temp);
					}

				}
			}
			ViewBag.actividades = new SelectList(actiCumplimientoSinAsignar, "Acum_Id", "Acum_Desc");
			PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
			{
				NombrePlanTrabajo = plantrabajo.Plat_Nom,
				IdPlantTrabajo = plantrabajo.Plat_Id
			};
			int _TotalRegistros = 0;
			_TotalRegistros = actiCumplimientoAsignados.Count();
			actiCumplimientoAsignados = actiCumplimientoAsignados.Skip((pagina - 1) * _RegistrosPorPaginaActividades)
											   .Take(_RegistrosPorPaginaActividades)
											   .ToList();
			int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPaginaActividades);
			_PaginadorCustomersActividades = new PaginadorGenerico<ActividadesAsignadasPlanDeTrabajoViewModel>()
			{
				RegistrosPorPagina = _RegistrosPorPaginaActividades,
				TotalRegistros = _TotalRegistros,
				TotalPaginas = _TotalPaginas,
				PaginaActual = pagina,
				Resultado = actiCumplimientoAsignados
			};

			ViewBag.actividadesAsignadas = _PaginadorCustomersActividades;
			return View(plandetrabajoActividades);
		}
		/// <summary>
		/// relaciona un usuario a una actividad de un plan de trabajo
		/// </summary>
		/// <param name="model">recibe un objeto de tipo PlandetrabajoActividadesViewModel para lelacionarlo a un usuario</param>
		/// <returns>redirecciona a actividadesplantrabajo con el id del plan de trabajo</returns>
		[HttpPost]

		public ActionResult ActividadesPlanTrabajo([Bind(Include = "IdPlantTrabajo,IdActiCumplimiento,IdUser")]PlandetrabajoActividadesViewModel model)
		{
			if (ModelState.IsValid)
			{

				UsuariosPlandetrabajo user = new UsuariosPlandetrabajo
				{
					Acum_Id = model.IdActiCumplimiento,
					Plat_Id = model.IdPlantTrabajo,
					Emp_Id = AccountData.NitEmpresa,
					Id = model.IdUser
				};
				PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(model.IdPlantTrabajo);
				plandeTrabajo.FechaActualizacion = DateTime.Now;
				db.Entry(plandeTrabajo).State = EntityState.Modified;
				db.Tb_UsersPlandeTrabajo.Add(user);
				db.SaveChanges();
			}

			return RedirectToAction("ActividadesPlanTrabajo", new { model.IdPlantTrabajo });
		}
		/// <summary>
		/// Elimina una actividad a un plan de trabajo y el usuario relacionado a esa actividad
		/// </summary>
		/// <param name="IdUserPlanTrabajo">recibe el id del usuario que tiene asiganada la tarea </param>
		/// <returns>redirecciona a actividadesplandetrabajo con el id del plan de trabajo/returns>
		public ActionResult EliminarActividadPlanTrabajo(int IdUserPlanTrabajo)
		{

			UsuariosPlandetrabajo usuariosPlandetrabajo = db.Tb_UsersPlandeTrabajo.Find(IdUserPlanTrabajo);
			db.Tb_UsersPlandeTrabajo.Remove(usuariosPlandetrabajo);
			PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(usuariosPlandetrabajo.Plat_Id);
			plandeTrabajo.FechaActualizacion = DateTime.Now;
			db.Entry(plandeTrabajo).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("ActividadesPlanTrabajo", new { IdPlantTrabajo = usuariosPlandetrabajo.Plat_Id });

		}

        public ActionResult IndexM(int pagina = 1)
        {
            int _TotalRegistros = 0;
            var tb_PlandeTrabajo = db.Tb_PlandeTrabajo.Where(p => p.Emp_Id == AccountData.NitEmpresa).ToList();
            _TotalRegistros = tb_PlandeTrabajo.Count();
            tb_PlandeTrabajo = tb_PlandeTrabajo.Skip((pagina - 1) * _RegistrosPorPagina)
                                               .Take(_RegistrosPorPagina)
                                               .ToList();
            int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _PaginadorCustomers = new PaginadorGenerico<PlandeTrabajo>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = tb_PlandeTrabajo
            };
            return View(_PaginadorCustomers);
        }
        /// <summary>
		/// lista y pagina las actividades que tiene un plan de trabajo dependiendo de la pagina 
		/// </summary>
		/// <param name="IdPlantTrabajo">recibe el id del plan de trabajo</param>
		/// <param name="pagina">recibe la pagina actual en la que se esta paginando</param>
		/// <returns>retorna un objeto de tipo paginadorcustomer que contiene la paginacion y el objeto a paginar</returns>
		public ActionResult Actividadesplantra(int IdPlantTrabajo, int pagina = 1)
        {
            var plantrabajo = db.Tb_PlandeTrabajo.Find(IdPlantTrabajo);
            ViewBag.users = new SelectList(db.Users.Where(c => c.Empr_Nit == AccountData.NitEmpresa), "Id", "Pers_Nom1");
            var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
            List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
            List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
            foreach (var item in actividadesEmpresa)
            {
                var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
                if (useractividadpt.Count <= 0)
                {
                    actiCumplimientoSinAsignar.Add(item);
                }
                else
                {
                    var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == IdPlantTrabajo && c.Acum_Id == item.Acum_Id).ToList();
                    if (cumplimientoPlanDetrabajo.Count > 0)
                    {

                        var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
                        var nombre = db.Users.Find(user.Id);

                        ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
                        {
                            IdUserPlanDeTrabajoActividad = user.Uspl_Id,
                            NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
                            IdPlantTrabajo = plantrabajo.Plat_Id,
                            IdActiCumplimiento = item.Acum_Id,
                            DescripcionCumplimiento = item.Acum_Desc,
                            NombrePlanTrabajo = plantrabajo.Plat_Nom

                        };
                        actiCumplimientoAsignados.Add(temp);
                    }

                }
            }
            ViewBag.actividades = new SelectList(actiCumplimientoSinAsignar, "Acum_Id", "Acum_Desc");
            PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
            {
                NombrePlanTrabajo = plantrabajo.Plat_Nom,
                IdPlantTrabajo = plantrabajo.Plat_Id
            };
            int _TotalRegistros = 0;
            _TotalRegistros = actiCumplimientoAsignados.Count();
            actiCumplimientoAsignados = actiCumplimientoAsignados.Skip((pagina - 1) * _RegistrosPorPaginaActividades)
                                               .Take(_RegistrosPorPaginaActividades)
                                               .ToList();
            int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPaginaActividades);
            _PaginadorCustomersActividades = new PaginadorGenerico<ActividadesAsignadasPlanDeTrabajoViewModel>()
            {
                RegistrosPorPagina = _RegistrosPorPaginaActividades,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = actiCumplimientoAsignados
            };

            ViewBag.actividadesAsignadas = _PaginadorCustomersActividades;
            return View(plandetrabajoActividades);
        }
        /// <summary>
        /// relaciona un usuario a una actividad de un plan de trabajo
        /// </summary>
        /// <param name="model">recibe un objeto de tipo PlandetrabajoActividadesViewModel para lelacionarlo a un usuario</param>
        /// <returns>redirecciona a actividadesplantrabajo con el id del plan de trabajo</returns>
        [HttpPost]

        public ActionResult Actividadesplantra([Bind(Include = "IdPlantTrabajo,IdActiCumplimiento,IdUser")]PlandetrabajoActividadesViewModel model)
        {
            if (ModelState.IsValid)
            {

                UsuariosPlandetrabajo user = new UsuariosPlandetrabajo
                {
                    Acum_Id = model.IdActiCumplimiento,
                    Plat_Id = model.IdPlantTrabajo,
                    Emp_Id = AccountData.NitEmpresa,
                    Id = model.IdUser
                };
                PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(model.IdPlantTrabajo);
                plandeTrabajo.FechaActualizacion = DateTime.Now;
                db.Entry(plandeTrabajo).State = EntityState.Modified;
                db.Tb_UsersPlandeTrabajo.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("ActividadesPlanTrabajo", new { model.IdPlantTrabajo });
        }

        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
