using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLogConfigTool.Models
{
    public class MqttServerSettings
    {
        public string server_name { get; set; }
        public string server_port { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string pub_topic { get; set; }
        public string sub_topic { get; set; }
        public int status { get; set; }
    }
}
