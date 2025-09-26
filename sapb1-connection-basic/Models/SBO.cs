using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapb1_connection_basic.Models
{
    public class SBO
    {
        public string SAP_SERVIDOR { get; set; }
        public string SAP_BASE { get; set; }
        public string SAP_TIPO_BASE { get; set; }
        public string SAP_DBUSUARIO { get; set; }
        public string SAP_DBPASSWORD { get; set; }
        public string SAP_USUARIO { get; set; }
        public string SAP_PASSWORD { get; set; }
        public string SL_SCHEME { get; set; }
        public string SL_PORT { get; set; }
        public string SL_HOST { get; set; }
        public string SL_BASEPATH { get; set; }
    }
}
