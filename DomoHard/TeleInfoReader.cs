using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace DomoHard
{
    public class TeleInfoReader
    {
        public void Read()
        {
            string filename = string.Empty;
            int baudrate = 1200;

            filename = "out.log";

            SerialPort serial = null;
            try
            {
                serial = new SerialPort(filename, baudrate, Parity.Even, 7, StopBits.One);
                serial.Open();
            }
            catch (Exception exc)
            {
                Console.WriteLine(string.Format("Open Failed ! {0}", exc.Message));
                return;
            }


            Console.WriteLine(string.Empty);

            try
            {
                while (true)
                {
                    if (serial.BytesToRead > 0)
                    {
                        int byteCount = serial.BytesToRead;
                        byte[] buffer = new byte[byteCount];

                        serial.Read(buffer, 0, byteCount);

                        //if (byteCount >= 16)
                        //{
                        //Console.WriteLine(string.Format("Read {0} bytes : ", byteCount));

                        //foreach (byte data in buffer)
                        //{
                        //    Console.Write(data.ToString("X2"));
                        //}

                        //Console.WriteLine(string.Empty);
                        foreach (byte data in buffer)
                        {
                            Console.Write((char)data);
                        }
                        //}
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(string.Format("Failure while reading ! {0}", exc.Message));
                return;
            }
        }
    }
}
