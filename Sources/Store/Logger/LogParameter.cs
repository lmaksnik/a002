using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logger {
	public class LogParameter {
		public LogParameter(string key, string value) {
			Key = key;
			Value = value;
		}
		public LogParameter(string key, object value) : this(key, value?.ToString()) { }

		public string Key { get; }

		public string Value { get; }

	}
}
