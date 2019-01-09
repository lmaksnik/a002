using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logger {
	internal class InternalLog : ILog {
		public InternalLog(ELogLevel level, string message, string memberName, string className, LogParameter[] parameters) {
			Level = level;
			Message = message;
			MemberName = memberName;
			ClassName = className;
			Parameters = parameters;
		}

		public ELogLevel Level { get; }
		public string Message { get; }
		public string MemberName { get; }
		public string ClassName { get; }
		public LogParameter[] Parameters { get; }
	}
}
