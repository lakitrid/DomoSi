using System;
using System.IO;
using System.Collections.Generic;

namespace DomoRest
{
	public class ModuleManager
	{
		private static readonly ModuleManager SingleInstance = new ModuleManager();

		private readonly string statusModulesLoaded = "Modules loaded";
		private readonly string statusModulesNotLoaded = "Modules not loaded";

		private ModuleManager ()
		{
			this.Modules = new List<Module> ();
		}

		public static ModuleManager Instance 
		{
			get {
				return SingleInstance;
			}
		}

		public string Status { get; set; }

		public List<Module> Modules { get; private set; }

		public bool Load()
		{
			string modules = string.Empty;
			this.Status = this.statusModulesNotLoaded;

			// Load the module list and check there availability
			try
			{
				StreamReader reader = new StreamReader("modules.list");

				modules = reader.ReadToEnd();

				reader.Close();
			}
			catch(Exception exc) {
				Console.WriteLine ("Error while loading the modules : {0}", exc.Message);
				return false;
			}

			if (string.IsNullOrWhiteSpace (modules)) {
				Console.WriteLine ("No modules to load");
				return false;
			}

			StringReader dataReader = new StringReader(modules);
			string line = null;

			while((line = dataReader.ReadLine()) != null)
			{
				string[] data = line.Split(new char[] { ';' });

				Module module = new Module ();
				module.Id = int.Parse (data [0]);
				module.Name = data [1];
				module.Type = (ModuleType)Enum.Parse (typeof(ModuleType), data [2]);
				module.Status = ModuleStatus.Imported;

				this.Modules.Add (module);
			}

			this.Status = this.statusModulesLoaded;
			return true;
		}
	}
}

