using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace Plenamente.App_Tool
{
    /// <summary>
    /// Clase que implementa las propiedades necesarias para mantener la identidad de los usuarios.
    /// </summary>
    public static class AccountData
    {
        /// <summary>
        /// Instancia del objeto session.
        /// </summary>
        private static SessionManager Session = new SessionManager();
        /// <summary>
        /// Obtiene el valor que indica si [la session esta iniciada].
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> Si [Session esta llena]; otra forma, <c>Falso</c>.
        /// </value>
        public static bool SessionOut => HttpContext.Current == null;
        /// <summary>
        /// Obtiene el identificador del usuario.
        /// </summary>
        /// <value>
        /// El indicador del usuario.
        /// </value>
        public static string UsuarioId
        {
            get
            {
                string id = Session.GetValue<string>("Session.UserId");
                if (string.IsNullOrEmpty(id))
                {
                    id = HttpContext.Current.User.Identity.GetUserId();
                    Session.SetValue("Session.UserId", id);
                }
                return id;
            }
        }
        /// <summary>
        /// Obtiene o llena el nit de la empresa.
        /// </summary>
        /// <value>
        /// El nit de la empresa.
        /// </value>
        public static int NitEmpresa
        {
            get { return Session.GetValue<int>("Session.NitEmpresa"); }
            set { Session.SetValue("Session.NitEmpresa", value);  }
        }
        /// <summary>
        /// Cierra la sesión.
        /// </summary>
        public static void CloseSession()
        {
            Session.Clear();
        }
    }
    /// <summary>
    /// Clase que implementa los métodos necesarios para administrar la sesión.
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// Agrega un valor a los parametros.
        /// </summary>
        /// <param name="key">El nombre o llave del parametro.</param>
        /// <param name="value">El valor del parametro.</param>
        public void SetValue(string key, object value)
        {
            if (HttpContext.Current != null)
            {
                System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
                session[key] = value;
            }
        }
        /// <summary>
        /// Obtiene el valor de un recurso especifico.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo del recurso.
        /// </typeparam>
        /// <param name="key">Nombre o llave del recurso.</param>
        /// <returns>Retorna el valor del parametro si no esta nulode lo contrario el valor por defecto.</returns>
        public T GetValue<T>(string key)
        {
            if (HttpContext.Current != null)
            {
                System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
                return session[key] == null ? default(T) : (T)Convert.ChangeType(session[key], typeof(T));
            }

            return default(T);
        }
        /// <summary>
        /// Limpia la instancia.
        /// </summary>
        public void Clear()
        {
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
            session.Clear();
            session.Abandon();
        }
    }
}