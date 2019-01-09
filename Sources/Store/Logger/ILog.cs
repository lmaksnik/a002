namespace Store.Logger {
	public interface ILog {

		ELogLevel Level { get; }

		string Message { get; }

		string MemberName { get; }

		string ClassName { get; }

		LogParameter[] Parameters { get; }
	}
}
