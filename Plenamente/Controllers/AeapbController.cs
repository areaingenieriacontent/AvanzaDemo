using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    /// <summary>
    /// Controlador destinado a la administración del acceso a los datos de las vistas relacionadas con las autoevaluaciones.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AeapbController : Controller
    {
        /// <summary>
        /// El número de registros por página.
        /// </summary>
        private readonly int _RegistrosPorPagina = 10;
        /// <summary>
        /// El páginador
        /// </summary>
        private PaginadorGenerico<AutoEvaluacionViewModel> _PaginadorCustomers;
        /// <summary>
        /// La base de datos
        /// </summary>
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Carga la vista de inicio con la empresa asociada al usuario actual.
        /// </summary>
        /// <returns>
        /// Retorna la lista de empresas cargadas en la vista.
        /// </returns>
        // GET: Aeapb
        public ActionResult Index()
        {
            IList<Empresa> listempresa = new List<Empresa>();
            IQueryable<Empresa> numbertrab = from empresa in db.Tb_Empresa where empresa.Empr_Nit == 1 select empresa;
            List<Empresa> empresas = numbertrab.ToList();
            foreach (Empresa empre in empresas)
            {
                listempresa.Add(new Empresa()
                {
                    Empr_Nit = empre.Empr_Nit,
                    Empr_Nom = empre.Empr_Nom,
                    Empr_Dir = empre.Empr_Dir,
                    Empr_Ttrabaja = empre.Empr_Ttrabaja,
                    Empr_Afiarl = empre.Empr_Afiarl,
                    Empr_Registro = DateTime.Now
                });
            }
            return View(listempresa);
        }
        /// <summary>
        /// Carga de la vista de autoevaluación con el listado de elementos asociados a esta.
        /// </summary>
        /// <param name="textError">
        /// Texto de los errores que se quieren mostrar.
        /// </param>
        /// <returns>
        /// Retorna la autoevaluacion cargada en la vista.
        /// </returns>
        [Authorize]
        public ActionResult AutoevaluacionApb(string textError = "")
        {
            List<CicloPHVAViewModel> list = new List<CicloPHVAViewModel>();
            try
            {
                ViewBag.TextError = textError;
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                TipoEmpresa tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
                AutoEvaluacion autoevaluacion = db.Tb_AutoEvaluacion.FirstOrDefault(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada);
                if (autoevaluacion == null)
                {
                    db.Tb_AutoEvaluacion.Add(
                          new AutoEvaluacion
                          {
                              Empr_Nit = AccountData.NitEmpresa,
                              Auev_Inicio = DateTime.Now,
                              Auev_Nom = "Autoevaluación"
                          });
                    db.SaveChanges();
                }
                list =
                   db.Tb_CicloPHVA
                       .Where(cp => tipoEmpresa.Categoria == 0 || cp.Categoria == 0 || cp.Categoria <= tipoEmpresa.Categoria)
                       .Select(cp =>
                       new CicloPHVAViewModel
                       {
                           Id = cp.Id,
                           Nombre = cp.Nombre,
                           Description = cp.Description,
                           Criterios = cp.Criterios
                                .Where(c => cp.Id == c.CicloPHVA_Id && tipoEmpresa.Categoria == 0 || c.Categoria == 0 || (c.Categoria <= tipoEmpresa.Categoria && c.CategoriaExcepcion != tipoEmpresa.Categoria))
                                .Select(c =>
                                new CriteriosViewModel
                                {
                                    Id = c.Crit_Id,
                                    Nombre = c.Crit_Nom,
                                    Porcentaje = c.Crit_Porcentaje,
                                    Registro = c.Crit_Registro,
                                    Estandares =
                                    c.Estandars
                                     .Where(e => tipoEmpresa.Categoria == 0 || e.Categoria == 0 || (e.Categoria <= tipoEmpresa.Categoria && e.CategoriaExcepcion != tipoEmpresa.Categoria))
                                     .Select(e =>
                                        new EstandaresViewModel
                                        {
                                            Id = e.Esta_Id,
                                            Nombre = e.Esta_Nom,
                                            Porcentaje = e.Esta_Porcentaje,
                                            Registro = e.Esta_Registro,
                                            Elementos =
                                                e.itemEstandars
                                                 .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                                                 .Select(i =>
                                                    new ElementoViewModel
                                                    {
                                                        Id = i.Iest_Id,
                                                        Descripcion = i.Iest_Desc,
                                                        Observaciones = i.Iest_Observa,
                                                        Porcentaje = i.Iest_Porcentaje,
                                                        Recurso = i.Iest_Recurso,
                                                        Registro = i.Iest_Registro,
                                                        Reursob = i.Iest_Rescursob,
                                                        Verificar = i.Iest_Verificar,
                                                        Video = i.Iest_Video,
                                                        Periodo = i.Iest_Peri,
                                                        MasInformacion = i.Iest_MasInfo,
                                                        Cumplimientos = i.Cumplimientos.Where(cu => cu.Empr_Nit == AccountData.NitEmpresa && !cu.AutoEvaluacion.Finalizada).ToList()
                                                    }).ToList()
                                        }).ToList()
                                }).ToList()
                       }).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }
            return View(list);
        }
        /// <summary>
        /// Agrega o actualiza un cumplimiento para el elemento asociado al elemento seleccionado.
        /// </summary>
        /// <param name="idItem">
        /// Identificador del elemento seleccionado.
        /// </param>
        /// <returns>
        /// Retorna el cumplimiento asociado cargado en la vista.
        /// </returns>
        [Authorize]
        public ActionResult CumplimientoApb(int idItem)
        {
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.FirstOrDefault(c => c.Empr_Nit == AccountData.NitEmpresa && c.Iest_Id == idItem && !c.AutoEvaluacion.Finalizada);
            ItemEstandar item = db.Tb_ItemEstandar.Find(idItem);

            if (cumplimiento == null)
            {
                return View(
                    new CumplimientoViewModel
                    {
                        ItemEstandarId = idItem,
                        Cumple = true,
                        Justifica = true,
                        Nit = AccountData.NitEmpresa,
                        Registro = DateTime.Now,
                        ItemEstandar =
                            new ElementoViewModel
                            {
                                Id = item.Iest_Id,
                                Descripcion = item.Iest_Desc,
                                Observaciones = item.Iest_Observa,
                                Porcentaje = item.Iest_Porcentaje,
                                Recurso = item.Iest_Recurso,
                                Registro = item.Iest_Registro,
                                Reursob = item.Iest_Rescursob,
                                Verificar = item.Iest_Verificar,
                                Video = item.Iest_Video,
                                Periodo = item.Iest_Peri,
                                MasInformacion = item.Iest_MasInfo
                            }
                    });
            }
            return View(
                new CumplimientoViewModel
                {
                    AcumMes = cumplimiento.AcumMes?.ToList(),
                    AutoEvaluacionId = cumplimiento.Auev_Id,
                    NoAplica = cumplimiento.Cump_NoAplica,
                    Cumple = cumplimiento.Cump_Cumple,
                    Evidencias = cumplimiento.Evidencias?.ToList(),
                    Id = cumplimiento.Cump_Id,
                    ItemEstandarId = cumplimiento.Iest_Id,
                    ItemEstandar =
                            new ElementoViewModel
                            {
                                Id = item.Iest_Id,
                                Descripcion = item.Iest_Desc,
                                Observaciones = item.Iest_Observa,
                                Porcentaje = item.Iest_Porcentaje,
                                Recurso = item.Iest_Recurso,
                                Registro = item.Iest_Registro,
                                Reursob = item.Iest_Rescursob,
                                Verificar = item.Iest_Verificar,
                                Video = item.Iest_Video,
                                Periodo = item.Iest_Peri,
                                MasInformacion = item.Iest_MasInfo
                            },
                    Justifica = cumplimiento.Cump_Justifica,
                    Nit = AccountData.NitEmpresa,
                    Nocumple = cumplimiento.Cump_Nocumple,
                    Nojustifica = cumplimiento.Cump_Nojustifica,
                    Observaciones = cumplimiento.Cump_Observ,
                    Registro = cumplimiento.Cump_Registro
                });
        }
        /// <summary>
        /// Agrega o actualiza un cumplimiento para el elemento asociado al elemento seleccionado.
        /// </summary>
        /// <param name="idItem">
        /// Cumplimiento cargado con modificaciones realizadas en la vista.
        /// </param>
        /// <returns>
        /// Retorna el cumplimiento asociado cargado en la vista.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CumplimientoApb([Bind(Include = "AutoEvaluacionId,Cumple,Nocumple,Justifica,Nojustifica,Id,Registro,Observaciones,ItemEstandarId,Nit")] CumplimientoViewModel model)
        {
            try
            {
                AutoEvaluacion autoevaluacion = db.Tb_AutoEvaluacion.FirstOrDefault(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada);
                Cumplimiento cumplimiento;
                if (model.Id == 0)
                {
                    cumplimiento =
                        new Cumplimiento
                        {
                            Cump_Id = model.Id,
                            Cump_NoAplica = model.NoAplica,
                            Cump_Cumple = model.Cumple,
                            Cump_Nocumple = model.Nocumple,
                            Cump_Justifica = model.Justifica,
                            Cump_Nojustifica = model.Nojustifica,
                            Cump_Observ = model.Observaciones,
                            Cump_Registro = DateTime.Now,
                            Empr_Nit = model.Nit,
                            Iest_Id = model.ItemEstandarId,
                            Auev_Id = autoevaluacion.Auev_Id,
                        };
                    db.Tb_Cumplimiento.Add(cumplimiento);
                }
                else
                {
                    cumplimiento = db.Tb_Cumplimiento.Find(model.Id);
                    cumplimiento.Cump_NoAplica = model.NoAplica;
                    cumplimiento.Cump_Id = model.Id;
                    cumplimiento.Cump_Cumple = model.Cumple;
                    cumplimiento.Cump_Nocumple = model.Nocumple;
                    cumplimiento.Cump_Justifica = model.Justifica;
                    cumplimiento.Cump_Nojustifica = model.Nojustifica;
                    cumplimiento.Cump_Observ = model.Observaciones;
                    cumplimiento.Cump_Registro = DateTime.Now;
                    cumplimiento.Empr_Nit = model.Nit;
                    cumplimiento.Iest_Id = model.ItemEstandarId;
                    cumplimiento.Auev_Id = autoevaluacion.Auev_Id;
                    db.Entry(cumplimiento).State = EntityState.Modified;
                }
                db.SaveChanges();
                model.Id = cumplimiento.Cump_Id;
                ViewBag.TextExitoso = "Se guardaron los datos exitosamente";
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
                ItemEstandar item = db.Tb_ItemEstandar.Find(model.ItemEstandarId);
                model.ItemEstandar =
                    new ElementoViewModel
                    {
                        Id = item.Iest_Id,
                        Descripcion = item.Iest_Desc,
                        Observaciones = item.Iest_Observa,
                        Porcentaje = item.Iest_Porcentaje,
                        Recurso = item.Iest_Recurso,
                        Registro = item.Iest_Registro,
                        Reursob = item.Iest_Rescursob,
                        Verificar = item.Iest_Verificar,
                        Video = item.Iest_Video,
                        Periodo = item.Iest_Peri,
                        MasInformacion = item.Iest_MasInfo
                    };
                return View(model);
            }

            return RedirectToAction("AutoevaluacionApb");
        }
        /// <summary>
        /// Guarda y termina la autoevaluación si ya estan diligenciados todos los elementos.
        /// </summary>
        /// <returns>
        /// Retorna la vista de inicio del sistema o el mensaje de error en caso de que se presente.
        /// </returns>
        [Authorize]
        public ActionResult GuardarTerminar()
        {
            List<CriteriosViewModel> list = new List<CriteriosViewModel>();
            try
            {
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                TipoEmpresa tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
                AutoEvaluacion autoevaluacion = db.Tb_AutoEvaluacion.FirstOrDefault(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada);
                if (autoevaluacion != null)
                {
                    int q = db.Tb_Cumplimiento.Count(c => c.Auev_Id == autoevaluacion.Auev_Id);
                    int q2 = db.Tb_ItemEstandar.Count(ie => tipoEmpresa.Categoria == 0 || ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria);
                    if (q2 > q)
                    {
                        return RedirectToAction("AutoevaluacionSST", new { textError = "Esta evaluación aún no ha sido finalizada" });
                    }
                    autoevaluacion.Auev_Fin = DateTime.Now;
                    autoevaluacion.Finalizada = true;
                    db.Entry(autoevaluacion).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
                return RedirectToAction("AutoevaluacionSST");
            }

            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Carga una evidencia asociada al cumplimiento seleccionado.
        /// </summary>
        /// <param name="idItem">
        /// Identificador del elemento seleccionado.
        /// </param>
        /// <returns>
        /// Retorna el cumplimiento asociado cargado en la vista.
        /// </returns>
        public ActionResult CargaEvidenciaApb(int idItem)
        {
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            ViewBag.users = new SelectList(db.Users.Where(c => c.Empr_Nit == usuario.Empr_Nit), "Acum_Id", "Acum_Desc");

            EvidenciaCumplimientoViewModel evidenciaCumplimientoViewModel = new EvidenciaCumplimientoViewModel
            {
                IdCumplimiento = idItem

            };
            return View(evidenciaCumplimientoViewModel);
        }
        /// <summary>
        /// Carga una evidencia asociada al cumplimiento seleccionado.
        /// </summary>
        /// <param name="idItem">
        /// Identificador del elemento seleccionado.
        /// </param>
        /// <returns>
        /// Retorna el cumplimiento asociado cargado en la vista.
        /// </returns>
        [HttpPost]
        public ActionResult CargaEvidenciaApb([Bind(Include = "Evidencia,Archivo,NombreDocumento,TipoDocumento,Fecha,Responsable,IdCumplimiento")]EvidenciaCumplimientoViewModel model)
        {
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.FirstOrDefault(a => a.Cump_Id == model.IdCumplimiento);
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ViewBag.users = new SelectList(db.Users.Where(b => b.Empr_Nit == usuario.Empr_Nit), "Id", "Pers_Nom1");
            string nombreArchivo = model.NombreDocumento;
            List<Evidencia> evidencias = db.Tb_Evidencia.Where(f => f.Evid_Nombre == nombreArchivo).ToList();
            if (evidencias.Count == 0)
            {
                if (model.Archivo == null)
                {
                    ViewBag.falla = "Seleccion un archivo";
                    return View(model);
                }
                string extensionArchivo = model.Archivo.FileName.Split('.').Last();

                Evidencia evidencia = new Evidencia
                {
                    Evid_Nombre = nombreArchivo,
                    Cump_Id = model.IdCumplimiento,
                    Evid_Registro = model.Fecha,
                    Tdca_id = Convert.ToInt32(model.TipoDocumento),
                    Evid_Archivo = nombreArchivo + "." + extensionArchivo

                };
                evidencia.Responsable = AccountData.UsuarioId;
                db.Tb_Evidencia.Add(evidencia);
                db.SaveChanges();

                if (model.Archivo.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), nombreArchivo + "." + extensionArchivo);
                    model.Archivo.SaveAs(path);
                }
                ViewBag.exitoso = "Guardado con exito en la ruta";
            }
            else
            {
                ViewBag.falla = "Ya existe un documento con ese nombre";
                return View(model);
            }
            return View(new EvidenciaCumplimientoViewModel());
        }
        /// <summary>
        /// Carga la vista de número de empleados en el caso de que se quiera cambiar la cantidad de empleados
        /// </summary>
        /// <returns>
        /// Retorna la vista de número de empleados.
        /// </returns>
        public ActionResult NumeroEmpleadosApb()
        {
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

            //Validacion. Existe alguna autoevaluacion en proceso
            if (db.Tb_AutoEvaluacion.Any(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada))
            {
                return RedirectToAction("AutoevaluacionApb");
            }
            if (empresa == null)
            {
                return RedirectToAction("Account", "Login");
            }
            return View(new EmpresaViewModel { IdEmpresa = empresa.Empr_Nit, NombreEmpresa = empresa.Empr_Nom, NumeroEmpleados = empresa.Empr_Ttrabaja });
        }
        /// <summary>
        /// Carga la vista de número de empleados en el caso de que se quiera cambiar la cantidad de empleados
        /// </summary>
        /// <returns>
        /// Retorna la vista de número de empleados.
        /// </returns>
        [HttpPost]
        public ActionResult NumeroEmpleadosApb([Bind(Include = "NumeroEmpleadosApb")]EmpresaViewModel model)
        {
            return RedirectToAction("AutoevaluacionApb");
        }
        /// <summary>
        /// Carga la vista de número de empleados en el caso de que se quiera cambiar la cantidad de empleados
        /// </summary>
        /// <returns>
        /// Retorna la vista de número de empleados.
        /// </returns>        
        public ActionResult ModificarNumeroEmpleados(int numeroEmpleados)
        {
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            EmpresaViewModel model = new EmpresaViewModel
            {
                IdEmpresa = empresa.Empr_Nit,
                NombreEmpresa = empresa.Empr_Nom,
                NumeroEmpleados = numeroEmpleados
            };
            return View(model);
        }
        /// <summary>
        /// Actualiza el número de empleados.
        /// </summary>
        /// <returns>
        /// Retorna la vista de número de empleados.
        /// </returns>
        [HttpPost]
        public ActionResult ModificarNumeroEmpleados([Bind(Include = "NumeroEmpleados")]EmpresaViewModel model)
        {
            Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
            if (!ModelState.IsValid)
            {
                model.NombreEmpresa = empresa.Empr_Nom;
                return View(model);
            }
            empresa.Empr_Ttrabaja = model.NumeroEmpleados;
            db.Entry(empresa).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("AutoevaluacionSST");
        }
        /// <summary>
        /// Carga la vista de historico de autoevaluaciones paginado.
        /// </summary>
        /// <param name="pagina">
        ///  El número de la página actual para el listado de autoevaluaciones.
        /// </param>
        /// <returns>
        /// La vista de hitorico de autoevaluaciones paginada.
        /// </returns>
        public ActionResult VerHistoricoApb(int pagina = 1)
        {
            int _TotalRegistros = 0;
            string user = User.Identity.Name;
            int? EmpNit = db.Users.Where(c => c.Email == user).FirstOrDefault().Empr_Nit;
            int identificadorIncremental = 1;
            List<AutoEvaluacion> autoEvaluacions = db.Tb_AutoEvaluacion.Where(c => c.Empr_Nit == EmpNit && c.Finalizada).OrderBy(c => c.Auev_Fin).ToList();
            _TotalRegistros = autoEvaluacions.Count();
            List<AutoEvaluacionViewModel> autoEvaluacionViewModel = new List<AutoEvaluacionViewModel>();
            foreach (AutoEvaluacion a in autoEvaluacions)
            {
                AutoEvaluacionViewModel autoEvaluacionView = new AutoEvaluacionViewModel
                {
                    Id = a.Auev_Id,
                    IdentificadorIncremental = identificadorIncremental,
                    Auev_Fin = a.Auev_Fin,
                    AutoEvaluacion = a,
                    Auev_Inicio = a.Auev_Inicio,
                    NameAutoEvaluacion = a.Auev_Nom
                };
                autoEvaluacionViewModel.Add(autoEvaluacionView);
                identificadorIncremental++;
            }
            autoEvaluacionViewModel = autoEvaluacionViewModel.Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _PaginadorCustomers = new PaginadorGenerico<AutoEvaluacionViewModel>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = autoEvaluacionViewModel
            };
            return View(_PaginadorCustomers);
        }

    }
}