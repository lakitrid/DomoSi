using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoRest
{
    internal class Log
    {
        private static readonly Log InstanceObj = new Log();

        private Log()
        {

        }

        public static Log Instance
        {
            get
            {
                return InstanceObj;
            }
        }

        public void Error(string message)
        {
            this.LogMessage("Error", message);
        }

        public void Error(string message, params object[] args)
        {
            this.Error(string.Format(message, args));
        }

        public void Info(string message)
        {
            this.LogMessage("Info", message);
        }

        public void Info(string message, params object[] args)
        {
            this.Info(string.Format(message, args));
        }

        private void LogMessage(string p, string message)
        {

        }
    }
}
