using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Statistics {
	public interface IStatistics : IInitializer {

		void Increment(string eventName, DateTime datetime, Dictionary<string, object> parameters, long count = 1);

	}
}
