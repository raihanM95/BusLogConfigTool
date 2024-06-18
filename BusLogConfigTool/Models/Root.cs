using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLogConfigTool.Models
{
    public class Root
    {
        public DateTime date_time { get; set; }
        public DeviceInformation device_information { get; set; }
        public SerialSettings serial_settings { get; set; }
        public ModbusSettings modbus_settings { get; set; }
        public WifiSettings wifi_settings { get; set; }
        public TcpServerSettings tcp_server_settings { get; set; }
        public MqttServerSettings mqtt_server_settings { get; set; }
        public OtherSettings other_settings { get; set; }
    }
}
