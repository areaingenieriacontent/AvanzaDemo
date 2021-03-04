using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.App_Tool;
using Plenamente.Scheduler;
using Plenamente.Models;
using Plenamente.Models.ViewModel;


namespace Plenamente.Controllers
{

    /// <summary>
    /// Controlador encargado del crud de actividades 
    /// </summary>
    public class ActividadCumplimientoController : Controller
    {
        /// <summary>
        /// Variables estandar para la paginacion
        /// </summary>
        private readonly int _RegistrosPorPagina = 10;
        private PaginadorGenerico<ActiCumplimiento> _PaginadorCustomers;
        private PaginadorGenerico<ProgamacionTareas> _PaginadorCustomers1;
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ActividadCumplimiento
        /// <summary>
        /// Lista y pagina las actividades perteneciente a un plan de trabajo de una empresa 
        /// </summary>
        /// <param name="pagina">la pagina actual en la que esta ubicado el usuario</param>
        /// <returns>retorna un objeto de tipo paginadorcustomer que contiene la paginacion y el objeto a paginar</returns>
        public ActionResult Index(int pagina = 1)
        {
            int _TotalRegistros = 0;
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            PlandeTrabajo PT = db.Tb_PlandeTrabajo.Where(e => e.Emp_Id == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var list = db.Tb_ProgamacionTareas.Where(a => a.ActiCumplimiento.Empr_Nit == AccountData.NitEmpresa
                               && a.Estado
                               && a.ActiCumplimiento.Usersplandetrabajo.Count > 0).OrderBy(x=>x.FechaHora).ToList();
            _TotalRegistros = list.Count();
            list = list.Skip((pagina - 1) * _RegistrosPorPagina)
                                               .Take(_RegistrosPorPagina)
                                               .ToList();
            int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _PaginadorCustomers1 = new PaginadorGenerico<ProgamacionTareas>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = list
            };
            //ActiCumplimiento actiEmpresas =  db.Tb_ActiCumplimiento.Find(AccountData.NitEmpresa);
            ViewBag.ReturnUrl = Request.UrlReferrer;
            //ViewBag.idptrab = PT.Plat_Id;
            return View(_PaginadorCustomers1);
        }

        // GET: ActividadCumplimiento/Details/5
        /// <summary>
        /// Lista la descripcion de una actividad perteneciente a un plan de trabajo asignados a una empresa
        /// </summary>
        /// <param name="id">Recibe el id de la actividad a buscar </param>			
        /// <returns>Retorna el viewmodel ViewModelActividadCumplimiento </returns>
        public ActionResult Details(int id,int? idpt)
        {

            ActiCumplimiento list = db.Tb_ActiCumplimiento.Find(id);
            ObjEmpresa objetivo = db.Tb_ObjEmpresa.Where(obj => obj.Oemp_Id == list.Oemp_Id).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(list.Id);
            Frecuencia frec = db.Tb_Frecuencia.Find(list.Frec_Id);
            ProgamacionTareas statetask = db.Tb_ProgamacionTareas.Where(est => est.Id == idpt).FirstOrDefault();
            ViewData["obj_name"] = objetivo.Oemp_Nombre;
            ViewData["username"] = usuario.Pers_Nom1 + "" + usuario.Pers_Nom2 + "" + usuario.Pers_Apel1 + "" + usuario.Pers_Apel2;
            ViewData["frec_name"] = frec.Frec_Descripcion;
            ViewData["idpt"] = idpt;
            string fechaeje = Convert.ToString(statetask.Fechaeje);
            ViewData["fprog"] = statetask.FechaHora;
            if (fechaeje == "01/01/1900 0:00:00")
            {
                ViewData["fejec"] = "No se ha ejecutado";
            }
            else
            {

                ViewData["fejec"] = statetask.Fechaeje;
            }
            var estado = statetask.Finalizada;
            if (estado == true)
            {
                ViewData["statetask"] = 1;
            }
            else
            {
                ViewData["statetask"] = null;
            }
            return View(list);

        }
        /// <summary>
        /// Lista y selecciona la empresa a la que se esta logeada
        /// </summary>
        /// <param name="idPlanDeTrabajo">Recibe el id del plan de trabajo a la cual va a pertenecer la actividad </param>	
        /// <returns>retorna a la vista para crear la actividad</returns>
        // GET: ActividadCumplimiento/Create
        public ActionResult Create(int idPlanDeTrabajo)
        {
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var listusers = db.Users.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Id, Value = o.Pers_Nom1 }).ToList();
            ViewBag.users = new SelectList(listusers, "Id", "Value");
            PlandeTrabajo planT = db.Tb_PlandeTrabajo.Find(idPlanDeTrabajo);
            ViewBag.datestart = planT.FechaInicio;
            ViewBag.dateend = planT.FechaFin;
            ViewModelActividadCumplimiento model = new ViewModelActividadCumplimiento();
            ViewBag.ReturnUrl = Request.UrlReferrer;
            ViewBag.idptrab = idPlanDeTrabajo;
            return View(model);

        }
        /// <summary>
        /// Crea la actividad recibiendo un objeto de tipo actividad validando que pertenezca a un plan de trabajo
        /// </summary>
        /// <param name="ViewModelActividadCumplimiento">recibe un objeto de tipo actividad para crearlo en la bd </param>
        /// <returns>retorna el objeto si no se crea adecuadamente o direcciona a la pagina inmediatamente anterior desde donde se origino la accion de creación si logra crearlo</returns>
        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia,idObjetivo,Frecuencia_desc,period,weekly_0,weekly_1,weekly_2,weekly_3,weekly_4,weekly_5,weekly_6,retornar,asigrecursos,IdUser,idPlanDeTrabajo")] ViewModelActividadCumplimiento model)
        {


            
            // TODO: Add insert logic here
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            // resolvemos el dia de la semana segun el checkbox seleccionado
            string dias = "";
            if (model.weekly_0 != null)
            {
                dias += "lunes,";
            }
            if (model.weekly_1 != null)
            {
                dias += "martes,";
            }
            if (model.weekly_2 != null)
            {
                dias += "miercoles,";
            }
            if (model.weekly_3 != null)
            {
                dias += "jueves,";
            }
            if (model.weekly_4 != null)
            {
                dias += "viernes,";
            }
            if (model.weekly_5 != null)
            {
                dias += "sabado,";
            }
            if (model.weekly_6 != null)
            {
                dias += "domingo,";
            }

            // TODO: Add insert logic here
            ActiCumplimiento actcumplimiento = new ActiCumplimiento
            {
                Acum_Desc = model.NombreActividad,
                Acum_Porcentest = model.Meta,
                Acum_IniAct = model.FechaInicial,
                Acum_FinAct = model.FechaFinal,
                Oemp_Id = model.idObjetivo,
                Acum_Registro = DateTime.Now,
                Id = model.IdUser,
                Frec_Id = Convert.ToInt32(model.Frecuencia),
                Peri_Id = 6,
                Empr_Nit = empresa.Empr_Nit,
                Repeticiones = model.period,
                DiasSemana = dias,
                HoraAct = model.hora,
                Finalizada = false,
                asigrecursos = model.asigrecursos



            };

            db.Tb_ActiCumplimiento.Add(actcumplimiento);

            db.SaveChanges();
            /// adicionamos el usuario asignado responsable para la actividad
            UsuariosPlandetrabajo user = new UsuariosPlandetrabajo
            {
                Acum_Id = actcumplimiento.Acum_Id,
                Plat_Id = model.idPlanDeTrabajo,
                Emp_Id = AccountData.NitEmpresa,
                Id = model.IdUser
            };
            db.Tb_UsersPlandeTrabajo.Add(user);
            db.SaveChanges();
            //Generamos la programacion de tareas en el tiempo.
            generateAppoiment(model, actcumplimiento.Acum_Id);

            var link = model.retornar;
            return Redirect(link);
           
        }

        private void generateAppoiment(ViewModelActividadCumplimiento model, int idActcumplimiento)
        {
            //// se asigna fecha inicial a la fecha final para tener solo una fecha de ejecucion
            //model.FechaFinal = model.FechaInicial;
            List<Schedule> schedules = new List<Schedule>();

            if (model.Frecuencia_desc == "norepeat")
            {
                SingleSchedule single1 = new SingleSchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora, //new TimeSpan(19, 30, 0),
                    Date = model.FechaInicial.Date
                };
                schedules.Add(single1);
            }
            else if (model.Frecuencia_desc == "daily")
            {
                SimpleRepeatingSchedule simple = new SimpleRepeatingSchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora,//new TimeSpan(10, 0, 0),
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                    //DaysBetween = model.period
                };
                schedules.Add(simple);
            }
            else if (model.Frecuencia_desc == "weekly")
            {
                WeeklySchedule weekly = new WeeklySchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora,//TimeSpan(8, 0, 0),
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                };

                //Seteamos loas dias de la semana seleccionados.
                int i = 0;
                List<DayOfWeek> dayOfWeeks = new List<DayOfWeek>();
                if (model.weekly_0 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Monday);
                    i++;
                }

                if (model.weekly_1 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Tuesday);
                    i++;
                }

                if (model.weekly_2 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Wednesday);
                    i++;
                }

                if (model.weekly_3 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Thursday);
                    i++;
                }

                if (model.weekly_4 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Friday);
                    i++;
                }

                if (model.weekly_5 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Saturday);
                    i++;
                }

                if (model.weekly_6 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Sunday);
                    i++;
                }

                weekly.SetDays(dayOfWeeks);

                schedules.Add(weekly);
            }
            else if (model.Frecuencia_desc == "monthly")
            {
                MonthlySchedule monthly = new MonthlySchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora,//TimeSpan(8, 0, 0),
                    DayOfMonth = model.period,
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                };
                schedules.Add(monthly);
            }
            else if (model.Frecuencia_desc == "bimestral")
            {
                EveryXMonthsSchedule everyxMonths = new EveryXMonthsSchedule
                {
                    Name = model.NombreActividad,
                    MonthsBetween = 2, //Cada dos meses
                    TimeOfDay = model.hora,
                    DayOfMonth = model.period,
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                };
                schedules.Add(everyxMonths);
            }
            else if (model.Frecuencia_desc == "trimestral")
            {
                EveryXMonthsSchedule everyxMonths = new EveryXMonthsSchedule
                {
                    Name = model.NombreActividad,
                    MonthsBetween = 3, //Cada tres meses
                    TimeOfDay = model.hora,
                    DayOfMonth = model.period,
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                };
                schedules.Add(everyxMonths);
            }
            else if (model.Frecuencia_desc == "semestral")
            {
                EveryXMonthsSchedule everyxMonths = new EveryXMonthsSchedule
                {
                    Name = model.NombreActividad,
                    MonthsBetween = 6, //Cada Seis meses
                    TimeOfDay = model.hora,
                    DayOfMonth = model.period,
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                };
                schedules.Add(everyxMonths);
            }

            CalendarGenerator generator = new CalendarGenerator();
            Period period = new Period(model.FechaInicial.Date, model.FechaFinal.Date);
            IEnumerable<Appointment> appointments = generator.GenerateCalendar(period, schedules);
            foreach (var app in appointments)
            {


                db.Tb_ProgamacionTareas.Add(
                new ProgamacionTareas
                {
                    ActiCumplimiento_Id = idActcumplimiento,
                    Descripcion = app.Name,
                    //FechaHora = new DateTime(model.FechaInicial.Year, model.FechaInicial.Month, model.FechaInicial.Day, model.hora.Hours, model.hora.Minutes, model.hora.Seconds),
                    FechaHora = app.Time,
                    Estado = true,
                    Finalizada=false,
                }
                );
            }
            db.SaveChanges();
        }

        // GET: ActividadCumplimiento/Edit/5
        /// <summary>
        /// Lista la actividad si es encontrado mediante el id que recibe si no es nullable
        /// </summary>
        /// <param name="id">id actividad si no es nullable</param>
        /// <returns>retorna la actividad a editar</returns>
        public ActionResult Edit(int id)
        {
            var listfrec = db.Tb_Frecuencia.Select(o => new { Id = o.Frec_Id, Value = o.Frec_Descripcion }).ToList();
            ViewBag.frecuenciaEmpresa = new SelectList(listfrec, "Id", "Value");
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            UsuariosPlandetrabajo upt = db.Tb_UsersPlandeTrabajo.Where(e => e.Acum_Id == id).FirstOrDefault();
            PlandeTrabajo planT = db.Tb_PlandeTrabajo.Find(upt.Plat_Id);
            ViewBag.datestart = planT.FechaInicio;
            ViewBag.dateend = planT.FechaFin;
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var listusers = db.Users.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Id, Value = o.Pers_Nom1 }).ToList();
            ViewBag.users = new SelectList(listusers, "Id", "Value");
            var model2 = db.Tb_ActiCumplimiento.Find(id);
            ViewModelActividadCumplimiento model = new ViewModelActividadCumplimiento
            {
                IdActiCumplimiento = model2.Acum_Id,
                IdEmpresa = model2.Empr_Nit,
                NombreActividad = model2.Acum_Desc,
                Meta = model2.Acum_Porcentest,
                idObjetivo = model2.Oemp_Id,
                FechaInicial = model2.Acum_IniAct,
                FechaFinal = model2.Acum_FinAct,
                hora = model2.HoraAct,
                Frecuencia = Convert.ToString(model2.Frec_Id),
                period = model2.Repeticiones,
                Finalizada = model2.Finalizada,
                asigrecursos = model2.asigrecursos,
                IdUser = model2.Id


            };
            if (model2.DiasSemana != null)
            {
                var lunes = model2.DiasSemana.Contains("lunes");
                var martes = model2.DiasSemana.Contains("martes");
                var miercoles = model2.DiasSemana.Contains("miercoles");
                var jueves = model2.DiasSemana.Contains("jueves");
                var viernes = model2.DiasSemana.Contains("viernes");
                var sabado = model2.DiasSemana.Contains("sabado");
                var domingo = model2.DiasSemana.Contains("domingo");
                if (lunes)
                {
                    ViewData["lunes"] = "checked";
                }

                if (martes)
                {
                    ViewData["martes"] = "checked";
                }

                if (miercoles)
                {
                    ViewData["miercoles"] = "checked";
                }

                if (jueves)
                {
                    ViewData["jueves"] = "checked";
                }

                if (viernes)
                {
                    ViewData["viernes"] = "checked";
                }

                if (sabado)
                {
                    ViewData["sabado"] = "checked";
                }

                if (domingo)
                {
                    ViewData["domingo"] = "checked";
                }
            }
            ViewData["userid"] = model2.Id;
            return View(model);
        }

        // POST: ActividadCumplimiento/Edit/5
        /// <summary>
        /// Mediante el objeto actividadcumplimiento que recibe se hacen las modificaciones por el usuario si el objeto es valido
        /// </summary>
        /// <param name="ViewModelActividadCumplimiento">recibe el objeto actividad a modificar</param>
        /// <returns>retorna la actividad si no fue modificado correctamente , si fue modificado redirecciona a la pagina inmediatamente anterior desde donde se realizo la accion</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpresa,IdActiCumplimiento,NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia,idObjetivo,Frecuencia_desc,period,weekly_0,weekly_1,weekly_2,weekly_3,weekly_4,weekly_5,weekly_6,retornar,asigrecursos,Finalizada,IdUser")] ViewModelActividadCumplimiento model)
        {
            
            UsuariosPlandetrabajo uplant = db.Tb_UsersPlandeTrabajo.Where(e => e.Acum_Id == model.IdActiCumplimiento).FirstOrDefault();
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            string dias = "";
            string periodo = model.Frecuencia;
            string frecuenciadesc = "";
            if (model.weekly_0 != null)
            {
                dias += "lunes,";
            }
            if (model.weekly_1 != null)
            {
                dias += "martes,";
            }
            if (model.weekly_2 != null)
            {
                dias += "miercoles,";
            }
            if (model.weekly_3 != null)
            {
                dias += "jueves,";
            }
            if (model.weekly_4 != null)
            {
                dias += "viernes,";
            }
            if (model.weekly_5 != null)
            {
                dias += "sabado,";
            }
            if (model.weekly_6 != null)
            {
                dias += "domingo,";
            }

            if (periodo == "1")
            {
                frecuenciadesc = "norepeat";
            }
            else if (periodo == "2")
            {
                frecuenciadesc = "daily";
            }
            else if (periodo == "3")
            {
                frecuenciadesc = "weekly";
            }
            else if (periodo == "4")
            {
                frecuenciadesc = "monthly";
            }
            else if (periodo == "8")
            {
                frecuenciadesc = "bimestral";
            }
            else if (periodo == "9")
            {
                frecuenciadesc = "trimestral";
            }
            else if (periodo == "10")
            {
                frecuenciadesc = "semestral";
            }
            model.Frecuencia_desc = frecuenciadesc;

            // TODO: Add insert logic here
            ActiCumplimiento actcumplimiento = new ActiCumplimiento
            {
                Acum_Id = model.IdActiCumplimiento,
                Acum_Ejec = null,
                Acum_Desc = model.NombreActividad,
                Acum_Porcentest = model.Meta,
                Acum_IniAct = model.FechaInicial,
                Acum_FinAct = model.FechaFinal,
                Oemp_Id = model.idObjetivo,
                Acum_Registro = DateTime.Now,
                Id = model.IdUser,
                Frec_Id = Convert.ToInt32(model.Frecuencia),
                Peri_Id = 6,
                Empr_Nit = empresa.Empr_Nit,
                Repeticiones = model.period,
                DiasSemana = dias,
                HoraAct = model.hora,
                asigrecursos = model.asigrecursos,
                Finalizada = model.Finalizada



            };

            
            db.Entry(actcumplimiento).State = EntityState.Modified;
            db.SaveChanges();
            
            var prog = db.Tb_ProgamacionTareas.Where(e => e.ActiCumplimiento_Id == actcumplimiento.Acum_Id).ToList();

            foreach (var program in prog)
            {
                program.Estado = false;
            }

            db.SaveChanges();
            generateAppoiment(model, actcumplimiento.Acum_Id);
           


            //Generamos la programacion de tareas en el tiempo.

            return RedirectToAction("Index");
        }
        public ActionResult Updatetask(int id)
        {
            ProgamacionTareas progt= db.Tb_ProgamacionTareas.Where(e => e.Id == id).FirstOrDefault();
            progt.Finalizada = true;
            progt.Fechaeje = DateTime.Now;
            db.Entry(progt).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("../Calendario/Index");
        }

        // GET: ActividadCumplimiento/Delete/5
        /// <summary>
        /// lista las actividades pertenecientes a un plan de trabajo que tiene a eliminar 
        /// </summary>
        /// <param name="id">recibe el id del objeto Actividad a eliminar</param>
        /// <returns>retorna el viewmodel ViewModelActividadCumplimiento a eliminar </returns>
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActividadCumplimiento/Create
        public ActionResult Crear(int idPlanDeTrabajo)
        {
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var listusers = db.Users.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Id, Value = o.Pers_Nom1 }).ToList();
            ViewBag.users = new SelectList(listusers, "Id", "Value");
            PlandeTrabajo planT = db.Tb_PlandeTrabajo.Find(idPlanDeTrabajo);
            ViewBag.datestart = planT.FechaInicio;
            ViewBag.dateend = planT.FechaFin;
            ViewModelActividadCumplimiento model = new ViewModelActividadCumplimiento();
            ViewBag.ReturnUrl = Request.UrlReferrer;
            ViewBag.idptrab = idPlanDeTrabajo;
            return View(model);

        }
        /// <summary>
        /// Crea la actividad recibiendo un objeto de tipo actividad validando que pertenezca a un plan de trabajo
        /// </summary>
        /// <param name="ViewModelActividadCumplimiento">recibe un objeto de tipo actividad para crearlo en la bd </param>
        /// <returns>retorna el objeto si no se crea adecuadamente o direcciona a la pagina inmediatamente anterior desde donde se origino la accion de creación si logra crearlo</returns>
        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Crear([Bind(Include = "NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia,idObjetivo,Frecuencia_desc,period,weekly_0,weekly_1,weekly_2,weekly_3,weekly_4,weekly_5,weekly_6,retornar,asigrecursos,IdUser,idPlanDeTrabajo")] ViewModelActividadCumplimiento model)
        {



            // TODO: Add insert logic here
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            // resolvemos el dia de la semana segun el checkbox seleccionado
            string dias = "";
            if (model.weekly_0 != null)
            {
                dias += "lunes,";
            }
            if (model.weekly_1 != null)
            {
                dias += "martes,";
            }
            if (model.weekly_2 != null)
            {
                dias += "miercoles,";
            }
            if (model.weekly_3 != null)
            {
                dias += "jueves,";
            }
            if (model.weekly_4 != null)
            {
                dias += "viernes,";
            }
            if (model.weekly_5 != null)
            {
                dias += "sabado,";
            }
            if (model.weekly_6 != null)
            {
                dias += "domingo,";
            }

            // TODO: Add insert logic here
            ActiCumplimiento actcumplimiento = new ActiCumplimiento
            {
                Acum_Desc = model.NombreActividad,
                Acum_Porcentest = model.Meta,
                Acum_IniAct = model.FechaInicial,
                Acum_FinAct = model.FechaFinal,
                Oemp_Id = model.idObjetivo,
                Acum_Registro = DateTime.Now,
                Id = model.IdUser,
                Frec_Id = Convert.ToInt32(model.Frecuencia),
                Peri_Id = 6,
                Empr_Nit = empresa.Empr_Nit,
                Repeticiones = model.period,
                DiasSemana = dias,
                HoraAct = model.hora,
                Finalizada = false,
                asigrecursos = model.asigrecursos



            };

            db.Tb_ActiCumplimiento.Add(actcumplimiento);

            db.SaveChanges();
            /// adicionamos el usuario asignado responsable para la actividad
            UsuariosPlandetrabajo user = new UsuariosPlandetrabajo
            {
                Acum_Id = actcumplimiento.Acum_Id,
                Plat_Id = model.idPlanDeTrabajo,
                Emp_Id = AccountData.NitEmpresa,
                Id = model.IdUser
            };
            db.Tb_UsersPlandeTrabajo.Add(user);
            db.SaveChanges();
            //Generamos la programacion de tareas en el tiempo.
            generateAppoiment(model, actcumplimiento.Acum_Id);

            var link = model.retornar;
            return Redirect(link);

        }
    }
}
