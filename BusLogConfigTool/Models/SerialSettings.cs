using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLogConfigTool.Models
{
    public class SerialSettings
    {
        public string baud_rate { get; set; }
        public string data_bits { get; set; }
        public string parity { get; set; }
        public string stop_bit { get; set; }
        public string flow_control { get; set; }
    }
}
