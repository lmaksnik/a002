using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Store.Context;
using Store.Exceptions.Store;
using Store.Logger;
using Store.Statistics;

namespace Store {
	public abstract class StorageBase : IDisposable {

		public bool Initialized { get; private set; }

		private readonly object _initLockObject = new object();

		// ReSharper disable once IdentifierTypo
		private readonly IInitializer[] _initialize;

		protected readonly ILogger Logger;

		protected readonly IStatistics Statistics;

		protected StorageBase(ILogger logger, IStatistics statistics, params IInitializer[] initialize) {
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Statistics = statistics;
			_initialize = initialize;
		}


		protected virtual void Initialize() {
			lock (_initLockObject) {
				if (Initialized) throw new StorageIsNotInitializedException(this);

				foreach (var initializer in _initialize)
					initializer.Init(this);

				Initialized = true;
			}
		}

		protected virtual void OnActionExecuting(ActionExecuteContext context) {
			Logger.Trace($"OnActionExecuting: {context}");

			context.BeginExecute();
		}

		protected virtual void OnActionExecuted(ActionExecuteContext context, ref object result) {
			context.EndExecute();
			Logger.Trace($"OnActionExecuted: {context}");
			Logger.Audit($"Executed \"{context.MemberName}\"", context.ToString());
			Statistics?.Increment(context.MemberName?.ToLower(), DateTime.Now,
				context.Parameters.ToDictionary(a => a.Name, a => a.Value));
		}

		protected virtual void OnException(ActionExecuteContext context, Exception ex) {
			context.EndExecute();
			Logger.Error(ex, context.ToString());
		}

		protected virtual T Call<T>(Func<T> func, ActionParameter[] parameters,
			[CallerMemberName] string memberName = null, [CallerFilePath] string filePath = null) {
			var context = new ActionExecuteContext(memberName, filePath, parameters);
			try {
				OnActionExecuting(context);

				if (func == null) throw new ArgumentNullException(nameof(func));
				object result = func();
				OnActionExecuted(context, ref result);
				return (T) result;
			}
			catch (Exception ex) {
				OnException(context, ex);
				throw;
			}
		}

		public void Dispose() {
			Logger?.Dispose();
		}
	}
}