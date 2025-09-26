using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapb1_connection_basic
{
    /// <summary>
    /// Clase base abstracta para manejar transacciones en el Service Layer.
    /// Define conexión, sesión y métodos generales.
    /// </summary>
    public abstract class TransactionService
    {
        private SessionHandler loginDAO = null;
        public string gl_Scheme = string.Empty;
        public string gl_Host = string.Empty;
        public string gl_Port = string.Empty;
        public string gl_Path = string.Empty;
        public string gl_companyDb = string.Empty;
        public string gl_username = string.Empty;
        public string gl_password = string.Empty;
        protected string _sessionId = null;
        protected ServiceLayerConnector sLInteract = null;

        public TransactionService()
        {
            loginDAO = new SessionHandler();

        }
        public abstract void Get();
        public abstract U POST<U>(string strJSON);
        /// <summary>
        /// Construye la URL base del Service Layer a partir de los parámetros globales.
        /// </summary>
        protected string getBasePath()
        {
            return new UriBuilder()
            {
                Scheme = gl_Scheme,
                Host = gl_Host,
                Port = Convert.ToInt32(gl_Port),
                Path = gl_Path
            }.ToString();
        }
        /// <summary>
        /// Devuelve el SessionId actual o genera uno nuevo si no existe.
        /// </summary>
        public string SessionId
        {
            get
            {
                return _sessionId = _sessionId ?? getSessionID();
            }
        }
        /// <summary>
        /// Obtiene un nuevo SessionId realizando login en el Service Layer.
        /// </summary>
        private string getSessionID()
        {
            sLInteract = new ServiceLayerConnector(getBasePath());
            //var loginResponse = JsonConvert.DeserializeObject<B1SLLoginResponse>
            //    (sLInteract.httpPOST("Login", string.Empty, (B1SLLogin)loginDAO.read("1")).Content);
            var loginResponse = sLInteract.httpPOST("Login", string.Empty, (B1SLLogin)loginDAO.read(gl_companyDb, gl_username, gl_password)).Content;

            B1SLLoginResponse reponse = System.Text.Json.JsonSerializer.Deserialize<B1SLLoginResponse>(loginResponse);
            return reponse.SessionId;
        }
        /// <summary>
        /// Cierra la sesión en el Service Layer (Logout).
        /// </summary>
        public void Logout()
        {
            var response = sLInteract.httpPOST("Logout", Globals.Token_SL, null);
            if (!response.IsSuccessful) throw new InvalidOperationException(response.StatusDescription);
            _sessionId = null;
        }
    }
}
