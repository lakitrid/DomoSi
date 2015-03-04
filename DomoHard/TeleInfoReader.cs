using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using DomoHard.Data;
using System.IO;

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
                this.serialPort = new SerialPort(this.serialPortName, this.baudrate, Parity.Even, 7, StopBits.One);
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

        /// <summary>
        /// Decode TeleInfo Data Sample frame :
        /// Start with STX (0x02)
        /// Serial number :  ADCO SerialNumber C
        /// Type of billing : OPTARIF HC.. <
        /// Level of power : ISOUSC 30 9
        /// Low Hour (in WH) : HCHC 034172198 )
        /// Peek Hour (IN WH) : HCHP 036245714 3
        /// current period : PTEC HP..  
        /// Instant intensity (in A) : IINST 003 Z
        /// Max intensoty (in A) : IMAX 029 J
        /// Apparent Power (in VA) : PAPP 00820 +
        /// Warning over intensity ( in A) : ADPS
        /// HHPHC A ,
        /// MOTDETAT 000000 B
        /// End with ETX (0x03)
        /// 
        /// for each variable, start with LF, end with CR
        /// </summary>
        /// <param name="raw">raw Data</param>
        /// <returns></returns>
        private TeleInfoData DecodeData(string raw)
        {
            TeleInfoData data = new TeleInfoData();



            return data;
        }
    }
}
