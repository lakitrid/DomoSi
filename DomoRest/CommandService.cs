using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoRest
{
    public class CommandService : ICommandService
    {
		public DateTime Ping()
        {
			return DateTime.UtcNow;
        }

		public string Status()
		{
			return ModuleManager.Instance.Status;
		}

		public List<Module> GetModules ()
		{
			return  ModuleManager.Instance.Modules;
		}
    }
}
