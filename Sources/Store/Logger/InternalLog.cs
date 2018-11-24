using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Store.Log {
	internal class InternalLog : ILog {

		public InternalLog(ELogLevel level, string message, [CallerMemberName] string callMemberName = null, [CallerLineNumber]int lineNumber = 0, [CallerFilePath]string callClassName = null, IDictionary<string, object> additionalInfo = null) {
			Level = level;
			Message = message;
			MemberName = $"{callMemberName} ({lineNumber})";
			ClassName = callClassName;
			AdditionalInfo = additionalInfo;
		}

		public InternalLog(Exception ex, ELogLevel level = ELogLevel.Error, [CallerMemberName] string callMemberName = null, [CallerLineNumber]int lineNumber = 0, [CallerFilePath]string callClassName = null, IDictionary<string, object> additionalInfo = null) {
			Level = level;
			Message = ex.Message;
			MemberName = $"{callMemberName} ({lineNumber})";
			ClassName = callClassName;
			AdditionalInfo = additionalInfo;
		}

		public ELogLevel Level { get; set; }
		public string Message { get; set; }
		public string MemberName { get; set; }
		public string ClassName { get; set; }
		public IDictionary<string, object> AdditionalInfo { get; set; }
	}
}
