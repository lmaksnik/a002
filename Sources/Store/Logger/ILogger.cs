using System;
using System.Runtime.CompilerServices;

namespace Store.Logger {
	public interface ILogger : IInitializer, IDisposable {

		ELogLevel LogLevel { get; }

		void Log(params ILog[] logObject);
	}
}
