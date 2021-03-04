using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using Plenamente.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Plenamente.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult RevisarTerminos()
        {
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var username = UserCurrent.UserName;
            var Terminos = UserCurrent.Pers_terminos;

            if (Terminos == false)
            {
                return RedirectToAction("Terminos", "admin", new { id = userId });
            }

            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Index()
        {
            List<EventViewModel> lst = new List<EventViewModel>();
            try
            {
                lst =
                    db.Tb_ProgamacionTareas
                        .Where(a => a.ActiCumplimiento.Empr_Nit == AccountData.NitEmpresa
                                && a.Estado
                                && a.ActiCumplimiento.Usersplandetrabajo.Count > 0)
                        .Select(a =>
                            new EventViewModel
                            {
                                Id = a.Id,
                                Description = "Tarea programada",
                                Title = a.Descripcion,
                                Start = a.FechaHora,
                                BackgroundColor = "#7DDAFF",
                                BorderColor = "#9FBDC9",
                                EventRoute = "/ActividadCumplimiento/Details/" + a.ActiCumplimiento.Acum_Id
                            }).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View(lst);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            List<Schedule> schedules = new List<Schedule>();
            ViewBag.Message = "Your contact page.";


            EveryXMonthsSchedule monthly = new EveryXMonthsSchedule
            {
                Name = "Prueba",
                MonthsBetween = 2, //Cada dos meses
                TimeOfDay = new TimeSpan(19, 30, 0),
                DayOfMonth = 20,
                SchedulingRange = new Period(DateTime.Today, new DateTime(2020,07,20)),
            };
            schedules.Add(monthly);

            CalendarGenerator generator = new CalendarGenerator();
            Period period = new Period(DateTime.Today, new DateTime(2020, 07, 20));
            IEnumerable<Appointment> appointments = generator.GenerateCalendar(period, schedules);

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Guía()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}