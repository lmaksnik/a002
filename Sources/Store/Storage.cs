using Store.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Store.Configuration.Owner;
using Store.DataStore;
using Store.Exceptions;
using Store.Logger;

namespace Store {
	public class Storage : IInitializer, IDisposable {

		#region Properties

		public readonly IStorageConfiguration Configuration;

		public readonly IDataStore DataStore;

		public readonly ILogger Logger;

		private bool _isInitialized;

		private readonly object _initLockObject = new object();

		#endregion

		public Storage(IStorageConfiguration configuration, IDataStore dataStore, ILogger logger) {
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));
			if (configuration.DefaultStreamMaxSize < 0) throw new StorageConfigurationException(nameof(configuration.DefaultStreamMaxSize));
			Configuration = configuration;
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			DataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
		}

		
		#region Public

		public void Init() {
			lock (_initLockObject) {
				if (_isInitialized) return;
				try {
					Logger.Init();
					DataStore.Init();
				} catch (Exception ex) {
					Critical(ex, nameof(Storage), nameof(Init));
					throw;
				}

				_isInitialized = true;
			}
		}

		public IDataStoreObject Upload(Stream stream, string group, string name, string contentType, IOwner owner = null, bool? approve = null) {
			var logParams = new List<LogParameter> {
				new LogParameter("streamLength", stream.Length),
				new LogParameter(nameof(group), group), 
				new LogParameter(nameof(name), name), 
				new LogParameter(nameof(contentType), contentType), 
				new LogParameter(nameof(owner), owner?.Gid ?? Guid.Empty), 
				new LogParameter(nameof(approve), approve.HasValue && approve.Value)
			};
			var sw = new Stopwatch();
			sw.Start();
			try {
				if (!_isInitialized) throw new StorageAlreadyBeenInitializedException(this);
				if (stream == Stream.Null) throw new ArgumentNullException(nameof(stream), "Stream is null!");
				if (string.IsNullOrWhiteSpace(group)) throw new ArgumentNullException(nameof(group));
				if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
				if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentNullException(nameof(contentType));

				if (HasUpload(owner)) throw new StorageReadonlyException();
				if (HasStreamSize(stream, owner)) throw new StreamExceedsAllowableSizeException();
				if (HasContentType(contentType, owner)) throw new ContentTypeNotAllowedException(contentType);

				var obj = DataStore.Upload(stream, group, name, contentType, owner, approve.HasValue && approve.Value);

				Info("File uploaded", nameof(Storage), nameof(Upload), logParams.ToArray());

				return obj;
			}
			catch (Exception ex) {
				Error(ex, nameof(Storage), nameof(Upload), logParams.ToArray());
				throw;
			}
			finally {
				logParams.Add(new LogParameter("runtime", sw.Elapsed));
				Trace(nameof(Storage), nameof(Upload), logParams.ToArray());
			}
		}

		public void Approve(Guid id, IOwner owner = null) {
			var logParams = new List<LogParameter> {
				new LogParameter(nameof(id), id),
				new LogParameter(nameof(owner), owner?.Gid ?? Guid.Empty)
			};
			var sw = new Stopwatch();
			sw.Start();
			try {
				if (!_isInitialized) throw new StorageAlreadyBeenInitializedException(this);
				if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

				if (HasUpload(owner)) throw new StorageReadonlyException();

				DataStore.Approve(id, owner);

				Info("File approved!", nameof(Storage), nameof(Approve), logParams.ToArray());
			} catch (Exception ex) {
				Error(ex, nameof(Storage), nameof(Approve), logParams.ToArray());
				throw;
			} finally {
				logParams.Add(new LogParameter("runtime", sw.Elapsed));
				Trace(nameof(Storage), nameof(Approve), logParams.ToArray());
			}
		}

		public IDataStoreObject Get(Guid id) {
			var logParams = new List<LogParameter> {
				new LogParameter(nameof(id), id)
			};
			var sw = new Stopwatch();
			sw.Start();
			try {
				if (!_isInitialized) throw new StorageAlreadyBeenInitializedException(this);
				if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

				var obj = DataStore.Get(id);

				Info("File approved!", nameof(Storage), nameof(Get), logParams.ToArray());

				return obj;
			} catch (Exception ex) {
				Error(ex, nameof(Storage), nameof(Get), logParams.ToArray());
				throw;
			} finally {
				logParams.Add(new LogParameter("runtime", sw.Elapsed));
				Trace(nameof(Storage), nameof(Get), logParams.ToArray());
			}
		}

		public void Remove(Guid id, IOwner owner = null) {
			var logParams = new List<LogParameter> {
				new LogParameter(nameof(id), id),
				new LogParameter(nameof(owner), owner?.Gid ?? Guid.Empty)
			};
			var sw = new Stopwatch();
			sw.Start();
			try {
				if (!_isInitialized) throw new StorageAlreadyBeenInitializedException(this);
				if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
				if (!HasRemove(owner)) throw new StorageReadonlyException();

				DataStore.Delete(id);

				Info("File removed!", nameof(Storage), nameof(Remove), logParams.ToArray());
			} catch (Exception ex) {
				Error(ex, nameof(Storage), nameof(Remove), logParams.ToArray());
				throw;
			} finally {
				logParams.Add(new LogParameter("runtime", sw.Elapsed));
				Trace(nameof(Storage), nameof(Remove), logParams.ToArray());
			}
		}

		#endregion

		#region Logger

		protected void Critical(Exception ex, string className, string memberName, params LogParameter[] parameters) {
			var logParams = new  List<LogParameter>(parameters) {
				new LogParameter(nameof(ex.Message), ex.Message),
				new LogParameter(nameof(ex.InnerException), ex.InnerException),
				new LogParameter(nameof(ex.StackTrace), ex.StackTrace),
			};
			Logger.Log(new InternalLog(ELogLevel.Critical, ex.Message, memberName, className, logParams.ToArray()));
		}

		protected void Error(Exception ex, string className, string memberName, params LogParameter[] parameters) {
			var logParams = new List<LogParameter>(parameters) {
				new LogParameter(nameof(ex.Message), ex.Message),
				new LogParameter(nameof(ex.InnerException), ex.InnerException),
				new LogParameter(nameof(ex.StackTrace), ex.StackTrace),
			};
			Logger.Log(new InternalLog(ELogLevel.Error, ex.Message, memberName, className, logParams.ToArray()));
		}

		protected void Info(string message, string className, string memberName = null, params LogParameter[] parameters) {
			Logger.Log(new InternalLog(ELogLevel.Info, message, memberName, className, parameters));
		}

		protected void Trace(string className, string memberName = null, params LogParameter[] parameters) {
			Logger.Log(new InternalLog(ELogLevel.Trace, "Trace", memberName, className, parameters));
		}

		#endregion

		#region Has 

		protected virtual bool HasUpload(IOwner owner) {
			return (owner != null && owner.AllowUpload) || Configuration.AllowUpload ||
			       Configuration.AllowUploadWithoutOwner;
		}

		protected virtual bool HasRemove(IOwner owner) {
			return (owner != null && owner.AllowRemove) || Configuration.AllowRemove;
		}

		protected bool HasStreamSize(Stream stream, IOwner owner) {
			var maxSize = owner?.StreamMaxSize ?? Configuration.DefaultStreamMaxSize;
			return maxSize <= 0 || stream.Length <= maxSize;
		}

		protected bool HasContentType(string contentType, IOwner owner) {
			//todo(Maksim): Сделать проверку на тип контента
			return true;
		}

		#endregion

		public void Dispose() {
			lock (_initLockObject) {
				if (!_isInitialized) return;
				Logger?.Dispose();
				DataStore?.Dispose();
				_isInitialized = false;
			}
		}
	}
}