using System;

namespace Store.Logger {
	[Flags]
	public enum ELogLevel {
		Critical = 0,
		Error = 1,
		Info = 2,
		Trace = 4
	}
}
