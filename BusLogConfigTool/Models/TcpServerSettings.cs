using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLogConfigTool.Models
{
    public class TcpServerSettings
    {
        public string server_name { get; set; }
        public string server_port { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public int status { get; set; }
    }
}
