using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Configuration.Owner;
using Store.DataStore;
using Store.Exceptions;
using Store.Logger;
using Store.StreamProvider;

namespace Store.UnitTests.Tests.Storage {
	[TestClass]
	public class TestStorageConstructor {

		[TestMethod]
		public void TestStorageConfigurations() {
			var configurations = StoreConfigurationHelper.GetAllConfigurationVariations();
			foreach (var configuration in configurations) {
				try {
					var storage = new Store.Storage(configuration, new StorageProvider(), new StorageLogger());
					Assert.IsTrue(configuration != null && configuration.DefaultStreamMaxSize >= 0);
				} catch (ArgumentNullException ex) { } catch (StorageConfigurationException ex) {

				} catch (Exception ex) {
					Assert.Fail(ex.Message);
				}
			}
		}

		[TestMethod]
		public void TestStorageForNull() {
			var configuration = StoreConfigurationHelper.GetNormalConfiguration();
			try {
				var storage = new Store.Storage(configuration, null, null);
				Assert.Fail("Storage incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				var storage = new Store.Storage(configuration, null, new StorageLogger());
				Assert.Fail("Storage incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				var storage = new Store.Storage(configuration, new StorageProvider(), null);
				Assert.Fail("Storage incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				var storage = new Store.Storage(configuration, new StorageProvider(), new StorageLogger());
			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}
		}

		#region Classes

		private class StorageLogger : ILogger {
			public void Init(StorageBase storage) {
				throw new NotImplementedException();
			}

			public void Dispose() {
				throw new NotImplementedException();
			}

			public void Log(params ILog[] logObject) {
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

		private class StorageProvider : IDataStore{
			public void Init(StorageBase storage) {
				throw new NotImplementedException();
			}

			public IDataStoreObject Upload(Stream stream, string @group, string name, string contentType, IOwner owner = null, bool? approve = null) {
				throw new NotImplementedException();
			}

			public bool Approve(Guid id, IOwner owner) {
				throw new NotImplementedException();
			}

			public IDataStoreObject Download(Guid id, IOwner owner) {
				throw new NotImplementedException();
			}

			public bool Remove(Guid id, IOwner owner) {
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
