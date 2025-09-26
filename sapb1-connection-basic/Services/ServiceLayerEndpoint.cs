using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapb1_connection_basic
{
    /// <summary>
    /// Implementación concreta de un servicio en el Service Layer.
    /// Provee métodos GET y POST para interactuar con recursos SAP B1.
    /// </summary>
    public class ServiceLayerEndpoint : TransactionService
    {
        public ServiceLayerEndpoint()
        {
        }
        public override void Get()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtiene un recurso del Service Layer y lo deserializa en un objeto genérico.
        /// </summary>
        public dynamic Get<T>(string resource)
        {
            var rslt = default(RestSharp.IRestResponse);
            rslt = sLInteract.httpGET(getBasePath() + resource, Globals.Token_SL);

            var objeto = System.Text.Json.JsonSerializer.Deserialize<T>(rslt.Content);

            return objeto;
        }

        public override U POST<U>(string strJSON)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Envía datos en formato JSON a un recurso del Service Layer.
        /// </summary>
        public IRestResponse POST(string ps_Object, string strJSON)
        {

            var rslt = default(RestSharp.IRestResponse);
            rslt = sLInteract.httpPOST(ps_Object, Globals.Token_SL, strJSON);

            return rslt;

        }
    }
}
