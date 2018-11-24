using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Log {
	public interface ILog {

		ELogLevel Level { get; set; }

		string Message { get; set; }

		string MemberName { get; set; }

		string ClassName { get; set; }

		IDictionary<string, object> AdditionalInfo { get; set; }

	}
}
