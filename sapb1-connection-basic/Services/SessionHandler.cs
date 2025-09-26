using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapb1_connection_basic
{
    /// <summary>
    /// Maneja credenciales y sesiones para login en Service Layer.
    /// </summary>
    internal class SessionHandler
    {
        public int add(object obj) { throw new NotImplementedException(); }
        /// <summary>
        /// Crea el objeto de login con las credenciales recibidas.
        /// </summary>
        public object read(string ps_company, string ps_username, string ps_password)
        {
            return new B1SLLogin
            {
                CompanyDB = ps_company,
                UserName = ps_username,
                Password = ps_password,
            };

        }

        public object readAll() { throw new NotImplementedException(); }

        public int update(object obj) { throw new NotImplementedException(); }
    }
}
