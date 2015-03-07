using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using DomoHard.Data;
using System.IO;
using System.ComponentModel;

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

        private Timer timer;

        public TeleInfoReader(string serialPortName, int baudrate = 1200)
        {
            Console.WriteLine("Start TeleInfoReader");
            this.serialPortName = serialPortName;
            this.baudrate = baudrate;
            this.timer = new Timer(this.SerialReadCallback, null, TimeSpan.FromSeconds(5), TimeSpan.Zero);
        }

        /// <summary>
        /// Dispose TeleInfo Reader
        /// </summary>
        public void Dispose()
        {
            this.timer.Dispose();
            Console.WriteLine("Stop TeleInfoReader");
        }

        /// <summary>
        /// Callback of the Timer : Start a read cycle on the serial
        /// </summary>
        /// <param name="state">the timer state</param>
        private void SerialReadCallback(object state)
        {
            Console.WriteLine("Start TeleInfoReader SerialReadCallback");

            if (this.InitSerial())
            {
                try
                {
                    this.ReadData();
                }
                catch
                {
                    Console.WriteLine("TeleInfoReader Problem While Reading on Serial port");
                }

                this.CloseSerial();
            }

            this.timer.Change(TimeSpan.FromMinutes(1), TimeSpan.Zero);

            Console.WriteLine("Stop TeleInfoReader SerialReadCallback");
        }

        /// <summary>
        /// Read the TeleInfo frame
        /// 
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
        /// </summary>
        private void ReadData()
        {
            Console.WriteLine("Start TeleInfoReader ReadData");
            string line = this.ReadLine();

            // Waiting for the first line of a frame
            while (!line.StartsWith("ADCO"))
            {
                line = this.ReadLine();
            }

            // Here we are at the start of a frame, we read each lines until the next frame
            TeleInfoData infoData = new TeleInfoData() { Date = DateTime.Now };

            do
            {
                string[] datas = line.Split(new char[] { ' ' });

                switch (datas[0])
                {
                    case "ADCO":
                        infoData.MeterId = datas[1];
                        break;
                    case "ISOUSC":
                        infoData.AccountIntensity = int.Parse(datas[1]);
                        break;
                    case "HCHC":
                        infoData.LowHourCpt = decimal.Parse(datas[1]);
                        break;
                    case "HCHP":
                        infoData.PeekHourCpt = decimal.Parse(datas[1]);
                        break;
                    case "IINST":
                        infoData.ActualIntensity = int.Parse(datas[1]);
                        break;
                    case "IMAX":
                        infoData.MaxIntensity = int.Parse(datas[1]);
                        break;
                    case "PAPP":
                        infoData.ApparentPower = int.Parse(datas[1]);
                        break;
                    case "ADPS":
                        infoData.HasExceed = true;
                        break;
                }

                line = this.ReadLine();
            }
            while (!line.StartsWith("ADCO"));

            // Launch the reading Event
            if (this.TeleInfoDataRead != null)
            {
                this.TeleInfoDataRead.BeginInvoke(infoData, null, null);
            }

            Console.WriteLine("Stop TeleInfoReader ReadData");
        }

        /// <summary>
        /// Read a line on the serial port and clean the output from special characters
        /// </summary>
        /// <returns>line readed</returns>
        private string ReadLine()
        {
            string line = this.serialPort.ReadLine();
            return line.Split(new char[] { (char)0x02, (char)0x03, '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        /// <summary>
        /// Initialize serial port reading and subscribe to reading event
        /// </summary>
        /// <returns>Whether the serial port is Open</returns>
        private bool InitSerial()
        {
            try
            {
                Console.WriteLine("Start TeleInfoReader InitSerial");
                this.serialPort = new SerialPort(this.serialPortName, this.baudrate, Parity.Even, 7, StopBits.One);
                this.serialPort.Open();
                return this.serialPort.IsOpen;
            }
            catch
            {
                Console.WriteLine("TeleInfoReader can't open Serial port {0}", this.serialPortName);
                return false;
            }
        }

        /// <summary>
        /// Close Serial Port
        /// </summary>
        private void CloseSerial()
        {
            try
            {
                this.serialPort.Close();
            }
            catch
            {
                Console.WriteLine("TeleInfoReader can't close Serial port {0}", this.serialPortName);
            }
        }
    }
}
