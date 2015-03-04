using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using DomoHard.Data;
using System.IO;

namespace DomoHard
{
    /// <summary>
    /// Communication system Over serial for getting data from oregon sensor using an arduino as receiver.
    /// The default hardware connection is over USB port, but we can use direct Serial wiring.
    /// 
    /// Be aware of the difference of voltage between the arduino (5V) and the linux Core (target a beaglebone black => 3.3V)
    /// </summary>
    public class OregonOnSerial : IDisposable
    {
        private string serialPortName;
        private int baudrate;
        private SerialPort serialPort;

        public delegate void OregonDataReadHandler(OregonData data);
        public event OregonDataReadHandler OregonDataRead;

        public OregonOnSerial(string serialPortName, int baudrate)
        {
            this.serialPortName = serialPortName;
            this.baudrate = baudrate;
        }

        public void Dispose()
        {
            this.serialPort.Close();
        }

        /// <summary>
        /// Initialize serial port reading and subscribe to reading event
        /// </summary>
        /// <returns>Whether the serial port is Open</returns>
        public bool InitSerial()
        {
            try
            {
                this.serialPort = new SerialPort(this.serialPortName, this.baudrate);
                this.serialPort.Open();
                this.serialPort.DataReceived += this.DataReceived;
                return this.serialPort.IsOpen;
            }
            catch (IOException exc)
            {
                return false;
            }
        }

        /// <summary>
        /// Event handler for receiving serial data
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event parameters</param>
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read while there is data to read
            while (this.serialPort.BytesToRead > 0)
            {
                try
                {
                    // We are reading line by line
                    OregonData data = this.DecodeData(this.serialPort.ReadLine());

                    if (OregonDataRead != null && data != null)
                    {
                        OregonDataRead.Invoke(data);
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Decode data from the arduino
        /// The data are as follow :
        /// I:XX;C:XX;T:xx.xx;B:xxx
        /// </summary>
        /// <param name="raw">raw string data</param>
        /// <returns>object data</returns>
        private OregonData DecodeData(string raw)
        {
            OregonData data = new OregonData();
            data.Date = DateTime.Now;

            bool isOk = true;

            string[] rawSplit = raw.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string set in rawSplit)
            {
                string[] setData = set.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (setData.Length == 2)
                {
                    switch (setData[0])
                    {
                        case "I":
                            data.Id = setData[1];
                            break;
                        case "C":
                            int channel = 0;
                            if (int.TryParse(setData[1], out channel))
                            {
                                data.Channel = channel;
                            }
                            else
                            {
                                isOk = false;
                            }
                            break;
                        case "T":
                            decimal temp = 0;
                            if (decimal.TryParse(setData[1], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out temp))
                            {
                                data.Temperature = temp;
                            }
                            else
                            {
                                isOk = false;
                            }
                            break;
                        case "B":
                            decimal battery = 0;
                            if (decimal.TryParse(setData[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out battery))
                            {
                                data.BatteryLevel = battery;
                            }
                            break;
                    }
                }
            }

            if (!isOk || !data.IsValid())
            {
                return null;
            }

            return data;
        }
    }
}
