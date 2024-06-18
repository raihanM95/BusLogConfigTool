using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLogConfigTool.Models
{
    public class DeviceInformation
    {
        public string device_name { get; set; }
        public string firmware_version { get; set; }
        public string device_id { get; set; }
        public string reading_interval { get; set; }
        public string upload_interval { get; set; }
        public int data_storage { get; set; }
    }
}
