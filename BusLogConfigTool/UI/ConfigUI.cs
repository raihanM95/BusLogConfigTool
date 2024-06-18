using BusLogConfigTool.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace BusLogConfigTool.UI
{
    public partial class ConfigUI : Form
    {
        private const string PlaceholderFText = "From";
        private const string PlaceholderTText = "To";
        private static SerialPort _sp;

        public ConfigUI()
        {
            InitializeComponent();

            txtDAR1F.Text = PlaceholderFText; // Set placeholder text initially
            txtDAR1F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR1F.Leave += txtDAR1F_Leave; // Attach Leave event handler
            txtDAR1F.Enter += txtDAR1F_Enter; // Attach Enter event handler

            txtDAR1T.Text = PlaceholderTText; // Set placeholder text initially
            txtDAR1T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR1T.Leave += txtDAR1T_Leave; // Attach Leave event handler
            txtDAR1T.Enter += txtDAR1T_Enter; // Attach Enter event handler


            txtDAR2F.Text = PlaceholderFText; // Set placeholder text initially
            txtDAR2F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR2F.Leave += txtDAR2F_Leave; // Attach Leave event handler
            txtDAR2F.Enter += txtDAR2F_Enter; // Attach Enter event handler

            txtDAR2T.Text = PlaceholderTText; // Set placeholder text initially
            txtDAR2T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR2T.Leave += txtDAR2T_Leave; // Attach Leave event handler
            txtDAR2T.Enter += txtDAR2T_Enter; // Attach Enter event handler


            txtDAR3F.Text = PlaceholderFText; // Set placeholder text initially
            txtDAR3F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR3F.Leave += txtDAR3F_Leave; // Attach Leave event handler
            txtDAR3F.Enter += txtDAR3F_Enter; // Attach Enter event handler

            txtDAR3T.Text = PlaceholderTText; // Set placeholder text initially
            txtDAR3T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR3T.Leave += txtDAR3T_Leave; // Attach Leave event handler
            txtDAR3T.Enter += txtDAR3T_Enter; // Attach Enter event handler


            txtDAR4F.Text = PlaceholderFText; // Set placeholder text initially
            txtDAR4F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR4F.Leave += txtDAR4F_Leave; // Attach Leave event handler
            txtDAR4F.Enter += txtDAR4F_Enter; // Attach Enter event handler

            txtDAR4T.Text = PlaceholderTText; // Set placeholder text initially
            txtDAR4T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            txtDAR4T.Leave += txtDAR4T_Leave; // Attach Leave event handler
            txtDAR4T.Enter += txtDAR4T_Enter; // Attach Enter event handler


            checkBoxTCP.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxMQTT.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void ConfigUI_Load(object sender, EventArgs e)
        {
            // Get the version string
            string version = GetVersion();

            // Append the version to the form's title
            this.Text = $"{this.Text} v{version}";
            timer.Start();

            DisableAllTextBoxesIfNeeded();
        }

        private string GetVersion()
        {
            // Get the current assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the version attribute
            Version version = assembly.GetName().Version;

            // Return the version as a string
            return version.ToString();
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            InitializeSerialPort();

            // Read data from the serial port
            string receivedData = _sp.ReadExisting();
            if (!string.IsNullOrEmpty(receivedData))
            {
                MessageBox.Show($"Data received: {receivedData}");
            }
            else
            {
                MessageBox.Show("No data received from the serial port.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            InitializeSerialPort();

            // Create the object that matches the JSON structure
            var data = new Root
            {
                date_time = DateTime.Now, //"2023-05-28T17:10:45",
                device_information = new DeviceInformation
                {
                    device_name = "BusLog4G_V1.2",
                    firmware_version = "V3.2",
                    device_id = "861190059739950",
                    reading_interval = "2",
                    upload_interval = "10",
                    data_storage = 1
                },
                serial_settings = new SerialSettings
                {
                    baud_rate = "9600",
                    data_bits = "8",
                    parity = "none",
                    stop_bit = "1",
                    flow_control = "none"
                },
                modbus_settings = new ModbusSettings
                {
                    modbus_device_id = "2",
                    modbus_data_register = "3",
                    data_address_range = new DataAddressRange
                    {
                        range1 = new Range { from = "20001", to = "20009" },
                        range2 = new Range { from = "20001", to = "20009" },
                        range3 = new Range { from = "20001", to = "20009" },
                        range4 = new Range { from = "20001", to = "20009" }
                    }
                },
                wifi_settings = new WifiSettings
                {
                    wifi_1_name = "siletch_wifi",
                    wifi_1_password = "@@##12345678",
                    wifi_2_name = "siltech_wifi2",
                    wifi_2_password = "@@##12345678@#"
                },
                tcp_server_settings = new TcpServerSettings
                {
                    server_name = "mydatahub.in",
                    server_port = "5003",
                    userid = "user",
                    password = "12345678",
                    status = 1
                },
                mqtt_server_settings = new MqttServerSettings
                {
                    server_name = "mydatahub.in",
                    server_port = "5004",
                    userid = "user",
                    password = "12345678",
                    pub_topic = "pub" + "BusLog4G_V1.2" + "861190059739950",
                    sub_topic = "sub" + "BusLog4G_V1.2" + "861190059739950",
                    status = 0
                },
                other_settings = new OtherSettings
                {
                    command = "network_sts",
                    message = "connected(31)"
                }
            };

            // Serialize the object to a JSON string
            string messageToSend = JsonConvert.SerializeObject(data);

            // Send the JSON string to the ESP32
            SendStringToSerialPort(messageToSend);
            //MessageBox.Show($"Sent: {messageToSend}");
            MessageBox.Show($"Data Send");

            // Wait for 2 second before reading from the serial port
            System.Threading.Thread.Sleep(2000);

            // Read data from the serial port
            string receivedData = _sp.ReadExisting();
            if (!string.IsNullOrEmpty(receivedData))
            {
                MessageBox.Show($"Data received: {receivedData}");
            }
            else
            {
                MessageBox.Show("No data received from the serial port.");
            }

            // Close the serial port
            _sp.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {

        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {

        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string version = GetVersion();
            this.Text = $"BusLog Config Tool v{version} \t\t\t {DateTime.Now:yyyy/MM/dd HH:mm:ss}";
        }

        private void txtDAR1F_Enter(object sender, EventArgs e)
        {
            if (txtDAR1F.Text == PlaceholderFText)
            {
                txtDAR1F.Text = "";
                txtDAR1F.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR1F_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR1F.Text))
            {
                txtDAR1F.Text = PlaceholderFText;
                txtDAR1F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR1T_Enter(object sender, EventArgs e)
        {
            if (txtDAR1T.Text == PlaceholderTText)
            {
                txtDAR1T.Text = "";
                txtDAR1T.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR1T_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR1T.Text))
            {
                txtDAR1T.Text = PlaceholderTText;
                txtDAR1T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR2F_Enter(object sender, EventArgs e)
        {
            if (txtDAR2F.Text == PlaceholderFText)
            {
                txtDAR2F.Text = "";
                txtDAR2F.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR2F_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR2F.Text))
            {
                txtDAR2F.Text = PlaceholderFText;
                txtDAR2F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR2T_Enter(object sender, EventArgs e)
        {
            if (txtDAR2T.Text == PlaceholderTText)
            {
                txtDAR2T.Text = "";
                txtDAR2T.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR2T_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR2T.Text))
            {
                txtDAR2T.Text = PlaceholderTText;
                txtDAR2T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR3F_Enter(object sender, EventArgs e)
        {
            if (txtDAR3F.Text == PlaceholderFText)
            {
                txtDAR3F.Text = "";
                txtDAR3F.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR3F_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR3F.Text))
            {
                txtDAR3F.Text = PlaceholderFText;
                txtDAR3F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR3T_Enter(object sender, EventArgs e)
        {
            if (txtDAR3T.Text == PlaceholderTText)
            {
                txtDAR3T.Text = "";
                txtDAR3T.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR3T_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR3T.Text))
            {
                txtDAR3T.Text = PlaceholderTText;
                txtDAR3T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR4F_Enter(object sender, EventArgs e)
        {
            if (txtDAR4F.Text == PlaceholderFText)
            {
                txtDAR4F.Text = "";
                txtDAR4F.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR4F_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR4F.Text))
            {
                txtDAR4F.Text = PlaceholderFText;
                txtDAR4F.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void txtDAR4T_Enter(object sender, EventArgs e)
        {
            if (txtDAR4T.Text == PlaceholderTText)
            {
                txtDAR4T.Text = "";
                txtDAR4T.ForeColor = System.Drawing.SystemColors.ControlText; // Set default text color
            }
        }

        private void txtDAR4T_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDAR4T.Text))
            {
                txtDAR4T.Text = PlaceholderTText;
                txtDAR4T.ForeColor = System.Drawing.SystemColors.GrayText; // Set placeholder text color
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox currentCheckbox = (CheckBox)sender;

            // If the current checkbox is checked, uncheck the other checkbox
            if (currentCheckbox.Checked)
            {
                if (currentCheckbox == checkBoxTCP)
                {
                    if (checkBoxTCP.Checked)
                    {
                        txtTCPServerIPDomain.Enabled = true;
                        txtTCPServerPort.Enabled = true;
                        txtTCPUserId.Enabled = true;
                        txtTCPPassword.Enabled = true;

                        checkBoxMQTT.Checked = false;
                        txtMQTTServerIPDomain.Enabled = false;
                        txtMQTTServerPort.Enabled = false;
                        txtMQTTUserId.Enabled = false;
                        txtMQTTPassword.Enabled = false;
                        txtMQTTPubTopic.Enabled = false;
                        txtMQTTSubTopic.Enabled = false;
                    }
                }
                else if (currentCheckbox == checkBoxMQTT)
                {
                    if (checkBoxMQTT.Checked)
                    {
                        txtMQTTServerIPDomain.Enabled = true;
                        txtMQTTServerPort.Enabled = true;
                        txtMQTTUserId.Enabled = true;
                        txtMQTTPassword.Enabled = true;
                        txtMQTTPubTopic.Enabled = true;
                        txtMQTTSubTopic.Enabled = true;

                        checkBoxTCP.Checked = false;
                        txtTCPServerIPDomain.Enabled = false;
                        txtTCPServerPort.Enabled = false;
                        txtTCPUserId.Enabled = false;
                        txtTCPPassword.Enabled = false;
                    }
                }
            }
            else
            {
                DisableAllTextBoxesIfNeeded();
            }
        }

        private void DisableAllTextBoxesIfNeeded()
        {
            // If both checkboxes are unchecked, disable all text boxes
            if (!checkBoxTCP.Checked && !checkBoxMQTT.Checked)
            {
                txtTCPServerIPDomain.Enabled = false;
                txtTCPServerPort.Enabled = false;
                txtTCPUserId.Enabled = false;
                txtTCPPassword.Enabled = false;

                txtMQTTServerIPDomain.Enabled = false;
                txtMQTTServerPort.Enabled = false;
                txtMQTTUserId.Enabled = false;
                txtMQTTPassword.Enabled = false;
                txtMQTTPubTopic.Enabled = false;
                txtMQTTSubTopic.Enabled = false;
            }
        }

        private static void InitializeSerialPort()
        {
            try
            {
                // Create and configure the SerialPort object
                _sp = new SerialPort
                {
                    PortName = "COM4",  // Set the port name here
                    BaudRate = 115200,  // Baud rate
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,

                    // Set read/write timeouts
                    ReadTimeout = 500,
                    WriteTimeout = 500
                };

                _sp.Open();  // Open the serial port

                Console.WriteLine("Serial port opened.");
            }
            catch (Exception ex)
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();  // Close the serial port if an error occurs
                }
                MessageBox.Show($"Error: {ex}");  // Display error message
            }
        }

        private static void SendStringToSerialPort(string message)
        {
            try
            {
                if (_sp.IsOpen)
                {
                    _sp.Write(message);  // Write the message to the serial port
                }
                else
                {
                    MessageBox.Show("Serial port is not open.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex}");  // Display error message if writing fails
            }
        }
    }
}
