using System;

namespace DomoRest
{
	public class Module
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ModuleType Type { get; set; }

		public ModuleStatus Status { get; set; }
	}
}

