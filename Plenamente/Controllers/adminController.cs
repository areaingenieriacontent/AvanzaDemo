using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using Plenamente.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        //Metodo que envia a la vista al usuario que no ha aceptado los terminos y condiciones.
        public ActionResult Terminos(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Terminos()
        {
            var userId = User.Identity.GetUserId();

            // Query the database for the row to be updated.
            var query =
                from use in db.Users
                where use.Id == userId
                select use;

            // Execute the query, and change the column values
            // you want to change.
            foreach (ApplicationUser use in query)
            {
                use.Pers_terminos = true;
                // Insert any additional changes to column values.
            }
            // Submit the changes to the database.
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            return RedirectToAction("Index", "Home");
        }

        //Vista inicial de los usuarios en donde se muestran graficas, notificaciones, etc.
        // GET: Administrador/admin
        [Authorize(Roles = "Administrator,Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Users(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr = UserCurrent.Empr_Nit;
            var usuarios = db.Users.Include(u => u.SedeCiudad).Include(u => u.CargoEmpresa).Include(u => u.AreaEmpresa).Include(u => u.Jefe).Include(u => u.EstadoPersona).Include(u => u.Arl).Include(u => u.Empresa).Include(u => u.Eps).Where(e => e.Empr_Nit == Empr);
            // from s in db.Users
            //select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.Pers_Nom1.Contains(searchString)
                                       || s.Pers_Apel1.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Pers_Nom1);
                    break;
                default:  // Name ascending 
                    usuarios = usuarios.OrderBy(s => s.Pers_Nom1);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Admin/Edit/TestUser
        [Authorize(Roles = "Admin")]
        #region public ActionResult EditUser(string UserName)
        public ActionResult EditarUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tdoc_Id = new SelectList(db.Tb_TipoDocumento, "Tdoc_Id", "Tdoc_Nom", user.Tdoc_Id);
            ViewBag.Gene_Id = new SelectList(db.Tb_Genero, "Gene_Id", "Gene_Nom", user.Gene_Id);
            ViewBag.Espe_Id = new SelectList(db.Tb_EstadoPersona, "Espe_Id", "Espe_Nom", user.Espe_Id);
            ViewBag.Cate_Id = new SelectList(db.Tb_CateLicencia, "Cate_Id", "Cate_Nom", user.Cate_Id);
            ViewBag.Afp_Id = new SelectList(db.Tb_Afp, "Afp_Id", "Afp_Nom", user.Afp_Id);
            ViewBag.Eps_Id = new SelectList(db.Tb_Eps, "Eps_Id", "Eps_Nom", user.Eps_Id);
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", user.Arl_Id);
            ViewBag.Tvin_Id = new SelectList(db.Tb_TipoVinculacion, "Tvin_Id", "Tvin_Nom", user.Tvin_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", user.Empr_Nit);
            return View(user);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditarUser([Bind(Include = "Id,Pers_Nom1,Pers_Apel1,Tdoc_Id,Pers_Doc,Gene_Id,Espe_Id,Pers_Licencia,Pers_LicVence,Cate_Id,Pers_Dir,Pers_Cemeg,Pers_Temeg,Afp_Id,Eps_Id,Arl_Id,Tvin_Id,Empr_Nit,UserName,Email,Pers_Cargo")] ApplicationUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            ViewBag.Tdoc_Id = new SelectList(db.Tb_TipoDocumento, "Tdoc_Id", "Tdoc_Nom", user.Tdoc_Id);
            ViewBag.Gene_Id = new SelectList(db.Tb_Genero, "Gene_Id", "Gene_Nom", user.Gene_Id);
            ViewBag.Espe_Id = new SelectList(db.Tb_EstadoPersona, "Espe_Id", "Espe_Nom", user.Espe_Id);
            ViewBag.Cate_Id = new SelectList(db.Tb_CateLicencia, "Cate_Id", "Cate_Nom", user.Cate_Id);
            ViewBag.Afp_Id = new SelectList(db.Tb_Afp, "Afp_Id", "Afp_Nom", user.Afp_Id);
            ViewBag.Eps_Id = new SelectList(db.Tb_Eps, "Eps_Id", "Eps_Nom", user.Eps_Id);
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", user.Arl_Id);
            ViewBag.Tvin_Id = new SelectList(db.Tb_TipoVinculacion, "Tvin_Id", "Tvin_Nom", user.Tvin_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", user.Empr_Nit);
            return View(user);
        }

        // GET: /Admin/Edit/Create 
        [Authorize(Roles = "Admin")]
        #region public ActionResult Create()
        //Función que permite obtener los campos de creación del usuario generado en el modelo UserRolesDTO en la clase ExpandedUserDTO
        public ActionResult CrearPersona(int? Empr_Nit)
        {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                objExpandedUserDTO.tipoDocumento = db.Tb_TipoDocumento.ToList<TipoDocumento>();
                objExpandedUserDTO.afp = db.Tb_Afp.ToList<Afp>();
                objExpandedUserDTO.eps = db.Tb_Eps.ToList<Eps>();
                objExpandedUserDTO.arl = db.Tb_Arl.ToList<Arl>();
                objExpandedUserDTO.sedeCiudad = db.Tb_SedeCiudad.ToList<SedeCiudad>();
                objExpandedUserDTO.ciudad = db.Tb_Ciudad.ToList<Ciudad>();
                objExpandedUserDTO.cargoEmpresa = db.Tb_CargoEmpresa.ToList<CargoEmpresa>();
                objExpandedUserDTO.areaEmpresa = db.Tb_AreaEmpresa.ToList<AreaEmpresa>();
                objExpandedUserDTO.cateLicencia = db.Tb_CateLicencia.ToList<CateLicencia>();
                objExpandedUserDTO.genero = db.Tb_Genero.ToList<Genero>();
                objExpandedUserDTO.jornadaEmpresa = db.Tb_JornadaEmpresa.ToList<JornadaEmpresa>();
                objExpandedUserDTO.tipoVinculacion = db.Tb_TipoVinculacion.ToList<TipoVinculacion>();
                objExpandedUserDTO.estadoPersona = db.Tb_EstadoPersona.ToList<EstadoPersona>();
                objExpandedUserDTO.Empresa = db.Tb_Empresa.ToList<Empresa>();
            }

            ViewBag.Roles = GetAllRolesAsSelectList();

            return View(objExpandedUserDTO);
        }
        #endregion
    

        //Método POST para enviar los campos llenados del formulario 
        // PUT: /Admin/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Create(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult CrearPersona(ExpandedUserDTO paramExpandedUserDTO, ExpandedUserDTO objExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userId = User.Identity.GetUserId();
                var UserCurrent = db.Users.Find(userId);
                var Empr_Nit = UserCurrent.Empr_Nit.ToString();
                int Empr_NitI = int.Parse(Empr_Nit);
 

                var Email = paramExpandedUserDTO.Email.Trim();
                var UserName = paramExpandedUserDTO.Email.Trim();
                var Password = "Detu8123113!";
                var Nombre = paramExpandedUserDTO.Nombres.Trim();
                var Apellido = paramExpandedUserDTO.Apellidos.Trim();
                var Documento = paramExpandedUserDTO.Documento;
                var Licencia = paramExpandedUserDTO.Pers_Licencia;
                var VigLicencia = paramExpandedUserDTO.Pers_LicVence;
                var Direccion = paramExpandedUserDTO.Pers_Direccion;
                var ContactoEme = paramExpandedUserDTO.Pers_ContactoEmeg;
                var TelefonoEme = paramExpandedUserDTO.Pers_TelefonoEmeg;
                var TipoDocumento = paramExpandedUserDTO.Tdoc_Id;
                var Afp = paramExpandedUserDTO.Afp_Id;
                var Eps = paramExpandedUserDTO.Eps_Id;
                var Arl = paramExpandedUserDTO.Arl_Id;
                var SedeCiudad = paramExpandedUserDTO.Sciu_Id;
                var Ciudad = paramExpandedUserDTO.Ciud_Id;
                var Cargo = paramExpandedUserDTO.Pers_Cargo;
                var AreaEmpresa = paramExpandedUserDTO.Aemp_Id;
                var Categoria = paramExpandedUserDTO.Cate_Id;
                var Genero = paramExpandedUserDTO.Gene_Id;
                var Jornada = paramExpandedUserDTO.Jemp_Id;
                var TipoVinculacion = paramExpandedUserDTO.Tvin_Id;
                int Empresa = Empr_NitI;
                var EstadoPersona = paramExpandedUserDTO.Espe_Id;


                if (Email == "")
                {
                    throw new Exception("No Email");
                }

                if (Password == "")
                {
                    throw new Exception("No Password");
                }

                // convierte en minusculas el email ingresado
                UserName = Email.ToLower();

                // Proceso de creación del usuario

                var objNewAdminUser = new ApplicationUser
                {
                    UserName = UserName,
                    Email = Email,
                    Pers_Nom1 = Nombre,
                    Pers_Apel1 = Apellido,
                    Pers_Doc = Documento,
                    Pers_Licencia = Licencia,
                    Pers_LicVence = VigLicencia,
                    Pers_Dir = Direccion,
                    Pers_Cemeg = ContactoEme,
                    Pers_Temeg = TelefonoEme,
                    Tdoc_Id = TipoDocumento,
                    Afp_Id = Afp,
                    Eps_Id = Eps,
                    Arl_Id = Arl,
                    Sciu_Id = SedeCiudad,
                    Pers_Cargo = Cargo,
                    Aemp_Id = AreaEmpresa,
                    Cate_Id = Categoria,
                    Gene_Id = Genero,
                    Jemp_Id = Jornada,
                    Tvin_Id = TipoVinculacion,
                    Empr_Nit = Empresa,
                    Espe_Id = EstadoPersona
                };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);

                if (AdminUserCreateResult.Succeeded == true)
                {
                    string strNewRole = "Usuario";

                    if (strNewRole != "0")
                    {
                        // Le asigna un rol al usuario
                        UserManager.AddToRole(objNewAdminUser.Id, strNewRole);
                    }

                    return RedirectToAction("Users");
                }
                else
                {
                    ViewBag.Roles = GetAllRolesAsSelectList();
                    ModelState.AddModelError(string.Empty,
                        "Error: Failed to create the user. Check password requirements.");
                    return View(paramExpandedUserDTO);
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            //catch (Exception ex)
            //{
            //    ViewBag.Roles = GetAllRolesAsSelectList();
            //    ModelState.AddModelError(string.Empty, "Error: " + ex);
            //    return View("Create");
            //}
        }
        #endregion

        // DELETE: /Admin/ElimarTrabajador
        [Authorize(Roles = "Admin")]
        #region public ActionResult DeleteUser(string UserName)
        public ActionResult EliminarTrabajador(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (UserName.ToLower() == this.User.Identity.Name.ToLower())
                {
                    ModelState.AddModelError(
                        string.Empty, "Error: Cannot delete the current user");

                    return View("EditarUser");
                }

                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                }

                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(UserName));
            }
        }
        #endregion

        // Controllers SysAdmin.
        [Authorize(Roles = "Administrator")]
        public ActionResult UsersSysadmin(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var usuarios = db.Users.Include(u => u.SedeCiudad).Include(u => u.CargoEmpresa).Include(u => u.AreaEmpresa).Include(u => u.Jefe).Include(u => u.EstadoPersona).Include(u => u.Arl).Include(u => u.Empresa);
            // from s in db.Users
            //select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.Pers_Nom1.Contains(searchString)
                                       || s.Pers_Apel1.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Pers_Nom1);
                    break;
                default:  // Name ascending 
                    usuarios = usuarios.OrderBy(s => s.Pers_Nom1);
                    break;
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        //Vista que permite la administración de los usuarios,
        //Lista Usuarios y genera los botones de crear usuario, crear rol, editar y eliminar usuarios,
        //Incluye el paginado de la lista y la busqueda por la cadena de texto ingresada
        // GET: /Admin/
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Index(string searchStringUserNameOrEmail)
        public ActionResult ManageUsers(string searchStringUserNameOrEmail, string currentFilter, int? page)
        {
            try
            {
                int intPage = 1;
                int intPageSize = 10;
                int intTotalPageCount = 0;

                if (searchStringUserNameOrEmail != null)
                {
                    intPage = 1;
                }
                else
                {
                    if (currentFilter != null)
                    {
                        searchStringUserNameOrEmail = currentFilter;
                        intPage = page ?? 1;
                    }
                    else
                    {
                        searchStringUserNameOrEmail = "";
                        intPage = page ?? 1;
                    }
                }

                ViewBag.CurrentFilter = searchStringUserNameOrEmail;

                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();
                int intSkip = (intPage - 1) * intPageSize;

                intTotalPageCount = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .Count();

                var result = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .OrderBy(x => x.UserName)
                    .Skip(intSkip)
                    .Take(intPageSize)
                    .ToList();

                foreach (var item in result)
                {
                    ExpandedUserDTO objUserDTO = new ExpandedUserDTO();

                    objUserDTO.UserName = item.UserName;
                    objUserDTO.Email = item.Email;
                    objUserDTO.LockoutEndDateUtc = item.LockoutEndDateUtc;
                    objUserDTO.Nombres = item.Pers_Nom1;
                    objUserDTO.Apellidos = item.Pers_Apel1;
                    objUserDTO.Documento = item.Pers_Doc;
                    objUserDTO.Pers_Licencia = item.Pers_Licencia;
                    objUserDTO.Pers_LicVence = item.Pers_LicVence;
                    objUserDTO.Pers_Direccion = item.Pers_Dir;
                    objUserDTO.Pers_ContactoEmeg = item.Pers_Cemeg;
                    objUserDTO.Pers_TelefonoEmeg = item.Pers_Temeg;
                    objUserDTO.Tdoc_Id = item.Tdoc_Id;
                    objUserDTO.Sciu_Id = item.Sciu_Id;
                    //objUserDTO.Ciudad = item.Ciudad;
                    objUserDTO.Cemp_Id = item.Cemp_Id;
                    objUserDTO.Aemp_Id = item.Aemp_Id;
                    objUserDTO.Cate_Id = item.Cate_Id;
                    objUserDTO.Gene_Id = item.Gene_Id;
                    objUserDTO.Jemp_Id = item.Jemp_Id;
                    objUserDTO.Tvin_Id = item.Tvin_Id;
                    objUserDTO.Eps_Id = item.Eps_Id;
                    objUserDTO.Afp_Id = item.Afp_Id;
                    objUserDTO.Arl_Id = item.Arl_Id;
                    objUserDTO.Empr_Nit = item.Empr_Nit;
                    objUserDTO.Espe_Id = item.Espe_Id;
                    objUserDTO.Jefe_Id = item.Jefe_Id;

                    col_UserDTO.Add(objUserDTO);
                    ViewBag.Empr_Nit = item.Empr_Nit;
                }

                // Set the number of pages
                var _UserDTOAsIPagedList =
                    new StaticPagedList<ExpandedUserDTO>
                    (
                        col_UserDTO, intPage, intPageSize, intTotalPageCount
                        );

                return View(_UserDTOAsIPagedList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();

                return View(col_UserDTO.ToPagedList(1, 25));
            }
        }
        #endregion

        // Users *****************************



        // GET: /Admin/Edit/Create 
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Create()
        //Función que permite obtener los campos de creación del usuario generado en el modelo UserRolesDTO en la clase ExpandedUserDTO
        public ActionResult Create(int? Empr_Nit)
        {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                objExpandedUserDTO.tipoDocumento = db.Tb_TipoDocumento.ToList<TipoDocumento>();
                objExpandedUserDTO.afp = db.Tb_Afp.ToList<Afp>();
                objExpandedUserDTO.eps = db.Tb_Eps.ToList<Eps>();
                objExpandedUserDTO.arl = db.Tb_Arl.ToList<Arl>();
                objExpandedUserDTO.sedeCiudad = db.Tb_SedeCiudad.ToList<SedeCiudad>();
                objExpandedUserDTO.ciudad = db.Tb_Ciudad.ToList<Ciudad>();
                objExpandedUserDTO.cargoEmpresa = db.Tb_CargoEmpresa.ToList<CargoEmpresa>();
                objExpandedUserDTO.areaEmpresa = db.Tb_AreaEmpresa.ToList<AreaEmpresa>();
                objExpandedUserDTO.cateLicencia = db.Tb_CateLicencia.ToList<CateLicencia>();
                objExpandedUserDTO.genero = db.Tb_Genero.ToList<Genero>();
                objExpandedUserDTO.jornadaEmpresa = db.Tb_JornadaEmpresa.ToList<JornadaEmpresa>();
                objExpandedUserDTO.tipoVinculacion = db.Tb_TipoVinculacion.ToList<TipoVinculacion>();
                objExpandedUserDTO.estadoPersona = db.Tb_EstadoPersona.ToList<EstadoPersona>();
                objExpandedUserDTO.Empresa = db.Tb_Empresa.ToList<Empresa>();
            }

            ViewBag.Roles = GetAllRolesAsSelectList();

            return View(objExpandedUserDTO);
        }
        #endregion



        //Método POST para enviar los campos llenados del formulario 
        // PUT: /Admin/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Create(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult Create(ExpandedUserDTO paramExpandedUserDTO, ExpandedUserDTO objExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var Email = paramExpandedUserDTO.Email.Trim();
                var UserName = paramExpandedUserDTO.Email.Trim();
                var Password = paramExpandedUserDTO.Password.Trim();
                var Nombre = paramExpandedUserDTO.Nombres.Trim();
                var Apellido = paramExpandedUserDTO.Apellidos.Trim();
                var Documento = paramExpandedUserDTO.Documento;
                var Licencia = paramExpandedUserDTO.Pers_Licencia;
                var VigLicencia = paramExpandedUserDTO.Pers_LicVence;
                var Direccion = paramExpandedUserDTO.Pers_Direccion;
                var ContactoEme = paramExpandedUserDTO.Pers_ContactoEmeg;
                var TelefonoEme = paramExpandedUserDTO.Pers_TelefonoEmeg;
                var TipoDocumento = paramExpandedUserDTO.Tdoc_Id;
                var Afp = paramExpandedUserDTO.Afp_Id;
                var Eps = paramExpandedUserDTO.Eps_Id;
                var Arl = paramExpandedUserDTO.Arl_Id;
                var SedeCiudad = paramExpandedUserDTO.Sciu_Id;
                var Ciudad = paramExpandedUserDTO.Ciud_Id;
                var Cargo = paramExpandedUserDTO.Pers_Cargo;
                var AreaEmpresa = paramExpandedUserDTO.Aemp_Id;
                var Categoria = paramExpandedUserDTO.Cate_Id;
                var Genero = paramExpandedUserDTO.Gene_Id;
                var Jornada = paramExpandedUserDTO.Jemp_Id;
                var TipoVinculacion = paramExpandedUserDTO.Tvin_Id;
                var Empresa = paramExpandedUserDTO.Empr_Nit;
                var EstadoPersona = paramExpandedUserDTO.Espe_Id;


                if (Email == "")
                {
                    throw new Exception("No Email");
                }

                if (Password == "")
                {
                    throw new Exception("No Password");
                }

                // convierte en minusculas el email ingresado
                UserName = Email.ToLower();

                // Proceso de creación del usuario

                var objNewAdminUser = new ApplicationUser
                {
                    UserName = UserName,
                    Email = Email,
                    Pers_Nom1 = Nombre,
                    Pers_Apel1 = Apellido,
                    Pers_Doc = Documento,
                    Pers_Licencia = Licencia,
                    Pers_LicVence = VigLicencia,
                    Pers_Dir = Direccion,
                    Pers_Cemeg = ContactoEme,
                    Pers_Temeg = TelefonoEme,
                    Tdoc_Id = TipoDocumento,
                    Afp_Id = Afp,
                    Eps_Id = Eps,
                    Arl_Id = Arl,
                    Sciu_Id = SedeCiudad,
                    Pers_Cargo = Cargo,
                    Aemp_Id = AreaEmpresa,
                    Cate_Id = Categoria,
                    Gene_Id = Genero,
                    Jemp_Id = Jornada,
                    Tvin_Id = TipoVinculacion,
                    Empr_Nit = Empresa,
                    Espe_Id = EstadoPersona
                };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);

                if (AdminUserCreateResult.Succeeded == true)
                {
                    string strNewRole = Convert.ToString(Request.Form["Roles"]);

                    if (strNewRole != "0")
                    {
                        // Le asigna un rol al usuario
                        UserManager.AddToRole(objNewAdminUser.Id, strNewRole);
                    }

                    return RedirectToAction("UsersSysadmin");
                }
                else
                {
                    ViewBag.Roles = GetAllRolesAsSelectList();
                    ModelState.AddModelError(string.Empty,
                        "Error: Failed to create the user. Check password requirements.");
                    return View(paramExpandedUserDTO);
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            //catch (Exception ex)
            //{
            //    ViewBag.Roles = GetAllRolesAsSelectList();
            //    ModelState.AddModelError(string.Empty, "Error: " + ex);
            //    return View("Create");
            //}
        }
        #endregion

        // GET: /Admin/Edit/TestUser 
        [Authorize(Roles = "Administrator")]
        #region public ActionResult EditUser(string UserName)
        public ActionResult EditUser(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // PUT: /Admin/EditUser
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ExpandedUserDTO objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("UsersSysAdmin","admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(paramExpandedUserDTO.UserName));
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUser
        [Authorize(Roles = "Administrator")]
        #region public ActionResult DeleteUser(string UserName)
        public ActionResult DeleteUser(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (UserName.ToLower() == this.User.Identity.Name.ToLower())
                {
                    ModelState.AddModelError(
                        string.Empty, "Error: Cannot delete the current user");

                    return View("EditUser");
                }

                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                }

                return RedirectToAction("Manageusers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(UserName));
            }
        }
        #endregion

        // GET: /Admin/EditRoles/TestUser 
        [Authorize(Roles = "Administrator")]
        #region ActionResult EditRoles(string UserName)
        public ActionResult EditRoles(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserName = UserName.ToLower();

            // Check that we have an actual user
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }

            UserAndRolesDTO objUserAndRolesDTO =
                GetUserAndRoles(UserName);

            return View(objUserAndRolesDTO);
        }
        #endregion

        // PUT: /Admin/EditRoles/TestUser 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        {
            try
            {
                if (paramUserAndRolesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                string UserName = paramUserAndRolesDTO.UserName;
                string strNewRole = Convert.ToString(Request.Form["AddRole"]);

                if (strNewRole != "No Roles Found")
                {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByName(UserName);

                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View(objUserAndRolesDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditRoles");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteRole?UserName="TestUser&RoleName=Administrator
        [Authorize(Roles = "Administrator")]
        #region public ActionResult DeleteRole(string UserName, string RoleName)
        public ActionResult DeleteRole(string UserName, string RoleName)
        {
            try
            {
                if ((UserName == null) || (RoleName == null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                UserName = UserName.ToLower();

                // Check that we have an actual user
                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                if (UserName.ToLower() ==
                    this.User.Identity.Name.ToLower() && RoleName == "Administrator")
                {
                    ModelState.AddModelError(string.Empty,
                        "Error: Cannot delete Administrator Role for the current user");
                }

                // Go get the User
                ApplicationUser user = UserManager.FindByName(UserName);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, RoleName);
                UserManager.Update(user);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                return RedirectToAction("EditRoles", new { UserName = UserName });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View("EditRoles", objUserAndRolesDTO);
            }
        }
        #endregion

        // Roles *****************************

        // GET: /Admin/ViewAllRoles
        [Authorize(Roles = "Administrator")]
        #region public ActionResult ViewAllRoles()
        public ActionResult ViewAllRoles()
        {
            var roleManager =
                new RoleManager<IdentityRole>
                (
                    new RoleStore<IdentityRole>(new ApplicationDbContext())
                    );

            List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                        select new RoleDTO
                                        {
                                            Id = objRole.Id,
                                            RoleName = objRole.Name
                                        }).ToList();

            return View(colRoleDTO);
        }
        #endregion

        // GET: /Admin/AddRole
        //[Authorize(Roles = "Administrator")]
        #region public ActionResult AddRole()
        public ActionResult AddRole()
        {
            RoleDTO objRoleDTO = new RoleDTO();

            return View(objRoleDTO);
        }
        #endregion

        // PUT: /Admin/AddRole
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult AddRole(RoleDTO paramRoleDTO)
        public ActionResult AddRole(RoleDTO paramRoleDTO)
        {
            try
            {
                if (paramRoleDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var RoleName = paramRoleDTO.RoleName.Trim();

                if (RoleName == "")
                {
                    throw new Exception("No RoleName");
                }

                // Create Role
                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext())
                        );

                if (!roleManager.RoleExists(RoleName))
                {
                    roleManager.Create(new IdentityRole(RoleName));
                }

                return Redirect("~/Admin/ViewAllRoles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("AddRole");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUserRole?RoleName=TestRole
        [Authorize(Roles = "Administrator")]
        #region public ActionResult DeleteUserRole(string RoleName)
        public ActionResult DeleteUserRole(string RoleName)
        {
            try
            {
                if (RoleName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (RoleName.ToLower() == "administrator")
                {
                    throw new Exception(String.Format("Cannot delete {0} Role.", RoleName));
                }

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                var UsersInRole = roleManager.FindByName(RoleName).Users.Count();
                if (UsersInRole > 0)
                {
                    throw new Exception(
                        String.Format(
                            "Canot delete {0} Role because it still has users.",
                            RoleName)
                            );
                }

                var objRoleToDelete = (from objRole in roleManager.Roles
                                       where objRole.Name == RoleName
                                       select objRole).FirstOrDefault();
                if (objRoleToDelete != null)
                {
                    roleManager.Delete(objRoleToDelete);
                }
                else
                {
                    throw new Exception(
                        String.Format(
                            "Canot delete {0} Role does not exist.",
                            RoleName)
                            );
                }

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();

                return View("ViewAllRoles", colRoleDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();

                return View("ViewAllRoles", colRoleDTO);
            }
        }
        #endregion


        // Utility

        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region private List<SelectListItem> GetAllRolesAsSelectList()
        private List<SelectListItem> GetAllRolesAsSelectList()
        {
            List<SelectListItem> SelectRoleListItems =
                new List<SelectListItem>();

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

            SelectRoleListItems.Add(
                new SelectListItem
                {
                    Text = "Select",
                    Value = "0"
                });

            foreach (var item in colRoleSelectList)
            {
                SelectRoleListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name.ToString(),
                        Value = item.Name.ToString()
                    });
            }

            return SelectRoleListItems;
        }
        #endregion

        #region private ExpandedUserDTO GetUser(string paramUserName)
        private ExpandedUserDTO GetUser(string paramUserName)
        {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();

            var result = UserManager.FindByName(paramUserName);

            // If we could not find the user, throw an exception
            if (result == null) throw new Exception("Could not find the User");
            
            objExpandedUserDTO.Nombres = result.Pers_Nom1;
            objExpandedUserDTO.Apellidos = result.Pers_Apel1;
            objExpandedUserDTO.Documento = result.Pers_Doc;
            objExpandedUserDTO.UserName = result.UserName;
            objExpandedUserDTO.Email = result.Email;
            objExpandedUserDTO.LockoutEndDateUtc = result.LockoutEndDateUtc;
            objExpandedUserDTO.AccessFailedCount = result.AccessFailedCount;
            objExpandedUserDTO.PhoneNumber = result.PhoneNumber;
            objExpandedUserDTO.Pers_Licencia = result.Pers_Licencia;
            objExpandedUserDTO.Pers_LicVence = result.Pers_LicVence;
            objExpandedUserDTO.Pers_Direccion = result.Pers_Dir;
            objExpandedUserDTO.Pers_ContactoEmeg = result.Pers_Cemeg;
            objExpandedUserDTO.Pers_TelefonoEmeg = result.Pers_Temeg;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                objExpandedUserDTO.tipoDocumento = db.Tb_TipoDocumento.ToList<TipoDocumento>();
                objExpandedUserDTO.afp = db.Tb_Afp.ToList<Afp>();
                objExpandedUserDTO.eps = db.Tb_Eps.ToList<Eps>();
                objExpandedUserDTO.arl = db.Tb_Arl.ToList<Arl>();
                objExpandedUserDTO.sedeCiudad = db.Tb_SedeCiudad.ToList<SedeCiudad>();
                objExpandedUserDTO.ciudad = db.Tb_Ciudad.ToList<Ciudad>();
                objExpandedUserDTO.cargoEmpresa = db.Tb_CargoEmpresa.ToList<CargoEmpresa>();
                objExpandedUserDTO.areaEmpresa = db.Tb_AreaEmpresa.ToList<AreaEmpresa>();
                objExpandedUserDTO.cateLicencia = db.Tb_CateLicencia.ToList<CateLicencia>();
                objExpandedUserDTO.genero = db.Tb_Genero.ToList<Genero>();
                objExpandedUserDTO.jornadaEmpresa = db.Tb_JornadaEmpresa.ToList<JornadaEmpresa>();
                objExpandedUserDTO.tipoVinculacion = db.Tb_TipoVinculacion.ToList<TipoVinculacion>();
                objExpandedUserDTO.estadoPersona = db.Tb_EstadoPersona.ToList<EstadoPersona>();
                objExpandedUserDTO.Jefe = db.Users.ToList<ApplicationUser>();
            }

            return objExpandedUserDTO;
        }
        #endregion

        #region private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO objExpandedUserDTO)
        private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser result =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (result == null)
            {
                throw new Exception("Could not find the User");
            }

            result.Email = paramExpandedUserDTO.Email;

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            UserManager.Update(result);

            // Was a password sent across?
            if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password))
            {
                // Remove current password
                var removePassword = UserManager.RemovePassword(result.Id);
                if (removePassword.Succeeded)
                {
                    // Add new password
                    var AddPassword =
                        UserManager.AddPassword(
                            result.Id,
                            paramExpandedUserDTO.Password
                            );

                    if (AddPassword.Errors.Count() > 0)
                    {
                        throw new Exception(AddPassword.Errors.FirstOrDefault());
                    }
                }
            }

            return paramExpandedUserDTO;
        }
        #endregion

        #region private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser user =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
            UserManager.Update(user);
            UserManager.Delete(user);
        }
        #endregion

        #region private UserAndRolesDTO GetUserAndRoles(string UserName)
        private UserAndRolesDTO GetUserAndRoles(string UserName)
        {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(UserName);

            List<UserRoleDTO> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UserRoleDTO
                 {
                     RoleName = objRole,
                     UserName = UserName
                 }).ToList();

            if (colUserRoleDTO.Count() == 0)
            {
                colUserRoleDTO.Add(new UserRoleDTO { RoleName = "No Roles Found" });
            }

            ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

            // Create UserRolesAndPermissionsDTO
            UserAndRolesDTO objUserAndRolesDTO =
                new UserAndRolesDTO();
            objUserAndRolesDTO.UserName = UserName;
            objUserAndRolesDTO.colUserRoleDTO = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion

        #region private List<string> RolesUserIsNotIn(string UserName)
        private List<string> RolesUserIsNotIn(string UserName)
        {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByName(UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
            var colRolesUserInNotIn = (from objRole in colAllRoles
                                       where !colRolesForUser.Contains(objRole)
                                       select objRole).ToList();

            if (colRolesUserInNotIn.Count() == 0)
            {
                colRolesUserInNotIn.Add("No Roles Found");
            }

            return colRolesUserInNotIn;
        }
        #endregion
    }
}