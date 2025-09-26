using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using log4net;
using sapb1_connection_basic.Models;

namespace sapb1_connection_basic
{
    public class Program
    {       
        private static readonly ILog log = Logs.GetLogger();
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// Configura la cultura, carga las conexiones desde el archivo XML
        /// e intenta conectarse a cada base de datos SAP definida.
        /// </summary>
        static void Main() 
        {
            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "ServiceLayer.config");

            try
            {
                // Configuración regional para Perú (decimal "." y separador de miles ",")
                CultureInfo culturaPersonalizada = new CultureInfo("es-PE");
                culturaPersonalizada.NumberFormat.NumberDecimalSeparator = ".";
                culturaPersonalizada.NumberFormat.NumberGroupSeparator = ",";
                Thread.CurrentThread.CurrentCulture = culturaPersonalizada;

                // Leer archivo XML y obtener lista de conexiones SBO
                List<SBO> sboList = ObtenerListaDeSBOs(filePath);

                // Conectar a cada base de datos SAP y establecer sesión
                foreach (var sbo in sboList)
                {
                    try
                    {
                        TransactionService cls_slTransactions = new ServiceLayerEndpoint();
                        cls_slTransactions.gl_Scheme = sbo.SL_SCHEME;
                        cls_slTransactions.gl_Host = sbo.SL_HOST;
                        cls_slTransactions.gl_Port = sbo.SL_PORT;
                        cls_slTransactions.gl_Path = sbo.SL_BASEPATH;
                        cls_slTransactions.gl_companyDb = sbo.SAP_BASE;
                        cls_slTransactions.gl_username = sbo.SAP_USUARIO;
                        cls_slTransactions.gl_password = sbo.SAP_PASSWORD;

                        // Guardar el token de sesión global
                        Globals.Token_SL = cls_slTransactions.SessionId;
                        log.Info($"Conexión establecida con {sbo.SAP_BASE} ✅");
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error al conectar con {sbo.SAP_BASE}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error al iniciar la aplicación", ex);
            }
        }
        /// <summary>
        /// Lee un archivo XML de configuración y devuelve una lista de objetos SBO
        /// con los datos de conexión a SAP y Service Layer.
        /// </summary>
        /// <param name="filePath">Ruta del archivo XML de configuración.</param>
        /// <returns>Lista de conexiones SBO.</returns>
        private static List<SBO> ObtenerListaDeSBOs(string filePath)
        {
            List<SBO> sboList = new List<SBO>();

            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);

                foreach (var sboElement in xmlDoc.Descendants("SBO"))
                {
                    SBO sbo = new SBO();
                    foreach (var addElement in sboElement.Elements("add"))
                    {
                        string key = (string)addElement.Attribute("key");
                        string value = (string)addElement.Attribute("value");

                        // Mapear cada clave del XML a la propiedad del objeto SBO
                        switch (key)
                        {
                            case "SAP_SERVIDOR":
                                sbo.SAP_SERVIDOR = value;
                                break;
                            case "SAP_BASE":
                                sbo.SAP_BASE = value;
                                break;
                            case "SAP_TIPO_BASE":
                                sbo.SAP_TIPO_BASE = value;
                                break;
                            case "SAP_DBUSUARIO":
                                sbo.SAP_DBUSUARIO = value;
                                break;
                            case "SAP_DBPASSWORD":
                                sbo.SAP_DBPASSWORD = value;
                                break;
                            case "SAP_USUARIO":
                                sbo.SAP_USUARIO = value;
                                break;
                            case "SAP_PASSWORD":
                                sbo.SAP_PASSWORD = value;
                                break;
                            case "SL_SCHEME":
                                sbo.SL_SCHEME = value;
                                break;
                            case "SL_PORT":
                                sbo.SL_PORT = value;
                                break;
                            case "SL_HOST":
                                sbo.SL_HOST = value;
                                break;
                            case "SL_BASEPATH":
                                sbo.SL_BASEPATH = value;
                                break;
                        }
                    }
                    sboList.Add(sbo);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al leer el archivo de configuración XML", ex);
                throw;
            }

            return sboList;
        }
    }
}
