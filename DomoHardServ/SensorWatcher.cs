using DomoHard;
using DomoHard.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace DomoHardServ
{
    /// <summary>
    /// Sensor Watcher, Registers itself on reader event of sensor and process the data
    /// to the MonogoDb
    /// </summary>
    internal class SensorWatcher : IDisposable
    {
        private Thread sensorThread;
        private AutoResetEvent waiter = new AutoResetEvent(false);
        private bool running = true;

        private readonly string mongoConnectionString;
        private readonly string mongoDbName;

        private TeleInfoReader teleInfoReader;
        private SaveData<TeleInfoData> teleInfoSave;

        public SensorWatcher()
        {
            this.mongoConnectionString = ConfigurationManager.AppSettings["mongoConnectionString"];
            this.mongoDbName = ConfigurationManager.AppSettings["mongoDbName"];

            ThreadStart start = new ThreadStart(this.WatchSensor);
            this.sensorThread = new Thread(start);
            this.sensorThread.Start();
        }

        /// <summary>
        /// Stop the thread
        /// </summary>
        public void Dispose()
        {
            this.sensorThread.Abort();
            this.sensorThread.Join();
        }

        /// <summary>
        /// Thread action, just Init the Sensor, then wait for event.
        /// And finally wxait for the stopping of the thread.
        /// </summary>
        private void WatchSensor()
        {
            try
            {
                this.InitSensorWatch();
                while (running)
                {
                    waiter.WaitOne();
                }
            }
            catch (ThreadAbortException)
            {
                this.DisposeSensors();
                running = false;
                Thread.ResetAbort();
                Console.WriteLine("Stopping SensorWatch");
            }
        }

        /// <summary>
        /// Init all sensor and register Reader event on them to save the data in MongoDb
        /// </summary>
        private void InitSensorWatch()
        {
            this.teleInfoReader = new TeleInfoReader(ConfigurationManager.AppSettings["TeleInfoPort"]);
            this.teleInfoReader.TeleInfoDataRead += TeleInfoDataRead;
            this.teleInfoSave = new SaveData<TeleInfoData>(this.mongoConnectionString, this.mongoDbName);
        }

        /// <summary>
        /// Dispose the sensors when the thread is stopped
        /// </summary>
        private void DisposeSensors()
        {
            this.teleInfoReader.Dispose();
            this.teleInfoSave.Dispose();
        }

        /// <summary>
        /// TeleInfo reader event
        /// </summary>
        /// <param name="data">TeleInfo data received</param>
        private void TeleInfoDataRead(TeleInfoData data)
        {
            waiter.Set();
            this.teleInfoSave.AddData(data);
            Console.WriteLine("Date : {2}, HP : {0}, HC : {1}", data.PeekHourCpt, data.LowHourCpt, data.Date.ToShortTimeString());
        }
    }
}
