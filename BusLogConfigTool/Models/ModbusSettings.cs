using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLogConfigTool.Models
{
    public class ModbusSettings
    {
        public string modbus_device_id { get; set; }
        public string modbus_data_register { get; set; }
        public DataAddressRange data_address_range { get; set; }
    }
}
