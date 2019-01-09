using System;
using System.Collections.Generic;
using System.Text;
using Store.Logger;

namespace Store.UnitTests.Mock {
	public class MockLogger : ILogger {

		public ELogLevel LogLevel { get; set; }

		public List<ILog> Logs;

		public void Init() {
			Logs = new List<ILog>();
		}

		public void Dispose() {
			if (Logs == null) throw new NullReferenceException();
			Logs = null;
		}


		public void Log(params ILog[] logObject) {
			if (Logs == null) throw new NullReferenceException();
			Logs.AddRange(logObject);
		}
	}
}
