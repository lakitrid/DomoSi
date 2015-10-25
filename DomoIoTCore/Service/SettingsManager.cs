using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoIoTCore.Service
{
    public sealed class SettingsManager
    {
        private static readonly SettingsManager instance = new SettingsManager();

        private Dictionary<string, string> parameters;

        private SettingsManager()
        {
            this.parameters = new Dictionary<string, string>();
            this.LoadConfiguration();
        }

        private void LoadConfiguration()
        {
        }

        public static SettingsManager Instance { get { return instance; } }
    }
}
