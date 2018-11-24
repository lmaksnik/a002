using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Store.Log;

namespace Store.Logger {
	public interface ILogger : IInitializer, IDisposable {

		void Write(params ILog[] logObject);

		void Critical(Exception ex, [CallerMemberName] string memberName = null, [CallerFilePath] string classFilePath = null);

		void Error(Exception ex, string addInfo = null);

		void Audit(string message, string addInfo = null);

		void Info(string message, string addInfo = null);

		void Trace(string message, [CallerMemberName] string memberName = null, [CallerFilePath] string classFilePath = null);
	}
}
