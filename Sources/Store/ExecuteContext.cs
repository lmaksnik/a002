using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Store.Configuration.Owner;

namespace Store {
	public sealed class ExecuteContext<T> {
		public ExecuteContext(IDictionary<string, object> parameters, Exception exception = null, T result = default(T),
			bool actionHandled = false) {
			Parameters = parameters;
			Exception = exception;
			Result = result;
			ActionHandled = actionHandled;
		}

		public IDictionary<string, object> Parameters { get; private set; }

		public Exception Exception { get; internal set; }

		public T Result { get; internal set; }

		public bool ActionHandled { get; set; }

		public Guid TraceId { get; set; }
 

		internal static ExecuteContext<T> CreateForUpload(Stream stream, string group, string name, string contentType,
			IOwner owner = null, bool? approve = null) {
			var parameters = new Dictionary<string, object> {
				{nameof(stream), stream},
				{nameof(name), name},
				{nameof(contentType), contentType},
				{nameof(owner), owner}
			};
			return new ExecuteContext<T>(parameters);

		}

}
}
