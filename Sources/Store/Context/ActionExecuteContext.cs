using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Store.Context {
	public class ActionExecuteContext {
		internal ActionExecuteContext(string memberName, string className, params ActionParameter[] parameters) {
			var stackTrace = new StackTrace(); //todo(maksim): Нужно проверить в релизе на виндовс и линукс
			MemberName = memberName;
			ClassOrFileName = className;
			Parameters = new List<ActionParameter>(parameters);
			ExecuteId = Guid.NewGuid();
		}

		public readonly Guid ExecuteId;

		public readonly ICollection<ActionParameter> Parameters;

		public readonly string MemberName;

		public readonly string ClassOrFileName;

		public TimeSpan Runtime => _stopwatch.Elapsed;

		private readonly Stopwatch _stopwatch = new Stopwatch();

		internal void BeginExecute() {
			_stopwatch.Reset();
			_stopwatch.Start();
		}

		internal void EndExecute() {
			Runtime = _stopwatch.Elapsed;
			_stopwatch.Stop();
		}
	}

	public class ActionParameter {
		public ActionParameter(string name, object value) {
			Name = name;
			Value = value;
		}

		public ActionParameter(string name) {
			Name = name;
			Value = default(object);
		}

		public string Name { get; set; }

		public object Value { get; set; }
	}
}
