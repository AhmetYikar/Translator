using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Domain.Entities
{
	//This model is created to show log records on admin panel.
	public class Log
    {
		public int Id { get; set; }
		public string MachineName { get; set; }
		public DateTime Logged { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public string Logger { get; set; }
		public string Callsite { get; set; }
		public string Exception { get; set; }
	}
}
