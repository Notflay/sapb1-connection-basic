using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace sapb1_connection_basic
{
    /// <summary>
    /// Maneja las llamadas HTTP al Service Layer de SAP B1.
    /// Encapsula métodos GET y POST con RestSharp.
    /// </summary>
    public class ServiceLayerConnector
    {
        private static readonly ILog log = Logs.GetLogger();
        private string baseURL;
        /// <summary>
        /// Inicializa el conector con la URL base del Service Layer.
        /// </summary>
        /// <param name="baseURL">Dirección base del Service Layer (ej: https://servidor:50000/b1s/v1/)</param>
        public ServiceLayerConnector(string baseURL)
        {
            this.baseURL = baseURL;
        }
        /// <summary>
        /// Envía una petición POST con un objeto genérico como cuerpo (ej. Login).
        /// </summary>
        public IRestResponse httpPOST<T>(string uri, string sessionId, T t)  
           where T : class
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new RestClient(baseURL);
                var request = new RestRequest(uri, Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddCookie("B1SESSION", sessionId);
                request.AddCookie("ROUTEID", ".node0");
                request.AddJsonBody(t);

                log.Info($"[POST] {baseURL}{uri} | Body: {System.Text.Json.JsonSerializer.Serialize(t)}");
                var response = client.Execute(request);
                log.Info($"[RESPONSE] Status: {response.StatusCode} | Content: {response.Content}");

                return client.Execute(request);
            }
            catch { throw; }
        }
        /// <summary>
        /// Envía una petición POST con un JSON en formato string.
        /// </summary>
        public IRestResponse httpPOST(string uri, string sessionId, string json)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient(baseURL);
            var request = new RestRequest(uri, Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddCookie("B1SESSION", sessionId);
            request.AddCookie("ROUTEID", ".node1");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            log.Info($"[POST] {baseURL}{uri} | Body: {json}");
            var response = client.Execute(request);
            log.Info($"[RESPONSE] Status: {response.StatusCode} | Content: {response.Content}");

            return response;
        }
        /// <summary>
        /// Ejecuta una petición GET al Service Layer.
        /// </summary>
        public IRestResponse httpGET(string uri, string sessionId)          // Traer ITEM
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new RestClient(baseURL);
                var request = new RestRequest(uri, Method.GET);
                request.AddCookie("B1SESSION", sessionId);
                request.AddCookie("ROUTEID", ".node0");

                log.Info($"[GET] {baseURL}{uri}");
                var response = client.Execute(request);
                log.Info($"[RESPONSE] Status: {response.StatusCode} | Content: {response.Content}");

                return response;
            }
            catch { throw; }
        }
    }
}
