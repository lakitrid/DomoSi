using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using DomoHard.Data;

namespace DomoHard
{
    /// <summary>
    /// TeleInfo Reader on Serial port
    /// 
    /// Used with direct wiring with a component SFH620A-2
    /// </summary>
    public class TeleInfoReader : IDisposable
    {
        private string serialPortName;
        private int baudrate = 1200;
        private SerialPort serialPort;

        public delegate void TeleInfoDataReadHandler(TeleInfoData data);
        public event TeleInfoDataReadHandler TeleInfoDataRead;

        public TeleInfoReader(string serialPortName, int baudrate = 1200)
        {
            this.serialPortName = serialPortName;
            this.baudrate = baudrate;

            this.InitSerial();
        }

        public void Dispose()
        {
            this.serialPort.Close();
        }

        /// <summary>
        /// Initialize serial port reading and subscribe to reading event
        /// </summary>
        private void InitSerial()
        {
            this.serialPort = new SerialPort(this.serialPortName, this.baudrate, Parity.Even, 7, StopBits.One);
            this.serialPort.Open();
            this.serialPort.DataReceived += this.DataReceived;
        }

        /// <summary>
        /// Event handler for receiving serial data
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event parameters</param>
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // We are reading line by line
                TeleInfoData data = this.DecodeData(this.serialPort.ReadExisting());

                if (TeleInfoDataRead != null && data != null)
                {
                    TeleInfoDataRead.Invoke(data);
                }
            }
            catch { }
        }

        private TeleInfoData DecodeData(string raw)
        {
            TeleInfoData data = new TeleInfoData();

            return data;
        }
    }
}
