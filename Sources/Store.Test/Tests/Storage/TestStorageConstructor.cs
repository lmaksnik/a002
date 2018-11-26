using System;
using System.IO;
using Store.Configuration;
using Store.Configuration.Owner;
using Store.Exceptions;
using Store.Log;
using Store.Logger;
using Store.Test.Test;

namespace Store.Test.Tests.Storage {
	public class TestStorageConstructor : ITest {

		#region System

		protected readonly Action [] Actions;

		public TestStorageConstructor() {
			Actions = new Action [] {
				Action1,
				Action2,
				Action3,
				Action4,
				Action5
			};
		}

		public void Run () {
			Console.WriteLine("Start testing Constructor ...");
			int idx = 1;
			foreach (var action in Actions) {
				action ();
				Console.WriteLine($"    Test {idx} is successed.");
				idx++;
			}
			Console.WriteLine("Testing Constructor is completed!");
			
		}

		#endregion

		#region Actions for upload

		protected void Action1 () {
			try {
				var storage = new Store.Storage(null, null, null);
				throw new Exception("Storage не должен был создаться.");
			} catch (ArgumentNullException ex) {
				if (ex.ParamName != "configuration") throw;
			}
		}

		protected void Action2 () {
			try {
				var storage = new Store.Storage(new StorageConfiguration {
					AllowRead = true,
					AllowReadWithoutOwner = true,
					AllowRemove = true,
					AllowUpload = true,
					AllowUploadWithoutOwner = true,
					DefaultStreamMaxSize = 0
				}, null, null);
				throw new Exception("Storage не должен был создаться.");
			} catch (ArgumentNullException ex) {
				if (ex.ParamName != "storageProvider") throw;
			}
		}

		protected void Action3 () {
			try {
				var storage = new Store.Storage(new StorageConfiguration {
					AllowRead = true,
					AllowReadWithoutOwner = true,
					AllowRemove = true,
					AllowUpload = true,
					AllowUploadWithoutOwner = true,
					DefaultStreamMaxSize = 0
				}, new StoragePrvider(), null);
				throw new Exception("Storage не должен был создаться.");
			} catch (ArgumentNullException ex) {
				if (ex.ParamName != "logger") throw;
			}
		}

		protected void Action4 () {
			try {
				var storage = new Store.Storage(new StorageConfiguration {
					AllowRead = true,
					AllowReadWithoutOwner = true,
					AllowRemove = true,
					AllowUpload = true,
					AllowUploadWithoutOwner = true,
					DefaultStreamMaxSize = 0
				}, new StoragePrvider(), new StorageLogger());
			} catch (Exception ex) {
				throw;
			}
		}

		protected void Action5 () {
			var config = new StorageConfiguration {
				AllowRead = true,
				AllowReadWithoutOwner = true,
				AllowRemove = true,
				AllowUpload = true,
				AllowUploadWithoutOwner = true,
				DefaultStreamMaxSize = -1
			};
			try {
				var storage = new Store.Storage(config, new StoragePrvider(), new StorageLogger());
				throw new Exception("Storage не должен был создаться.");
			} catch (StorageConfigurationException ex) {
				if (ex.ParameterName != nameof(IStorageConfiguration.DefaultStreamMaxSize)) throw;
			}
		}

		#endregion

		#region Model

		private class StoragePrvider : IStorageProvider {
			public void Init(StorageBase storage) {
				throw new NotImplementedException();
			}

			public IStorageObject Upload(Stream stream, string @group, string name, string contentType, IOwner owner = null, bool? approve = null) {
				throw new NotImplementedException();
			}

			public bool Approve(Guid id, IOwner owner) {
				throw new NotImplementedException();
			}

			public IStorageObject Download(Guid id, IOwner owner) {
				throw new NotImplementedException();
			}

			public bool Remove(Guid id, IOwner owner) {
				throw new NotImplementedException();
			}
		}

		private class StorageLogger: ILogger {
			public void Init(StorageBase storage) {
				throw new NotImplementedException();
			}

			public void Dispose() {
				throw new NotImplementedException();
			}

			public void Write(params ILog[] logObject) {
				throw new NotImplementedException();
			}

			public void Critical(Exception ex, string memberName = null, string classFilePath = null) {
				throw new NotImplementedException();
			}

			public void Error(Exception ex, string addInfo = null) {
				throw new NotImplementedException();
			}

			public void Audit(string message, string addInfo = null) {
				throw new NotImplementedException();
			}

			public void Info(string message, string addInfo = null) {
				throw new NotImplementedException();
			}

			public void Trace(string message, string memberName = null, string classFilePath = null) {
				throw new NotImplementedException();
			}
		}

		#endregion


		#region Actions for constructor

		protected Store.Storage CreateStorage (IStorageConfiguration configuration) {
			var storage = new Store.Storage (configuration, null, null);
			storage.Initialize();
			return storage;
		}


		#endregion
	}
}
