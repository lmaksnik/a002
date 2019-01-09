using System;
using System.Runtime.CompilerServices;

namespace Store.Logger {
	public interface ILogger : IInitializer, IDisposable {

		void Log(params ILog[] logObject);
	}
}
