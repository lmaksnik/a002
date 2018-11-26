using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Configuration;
using Store.Configuration.Owner;
using Store.Exceptions.Store;
using Store.Log;
using Store.Logger;

namespace Store.UnitTests.Tests.Storage {
	[TestClass]
	public class TestStorageUpload {

		protected Stream UploadededStream;

		[TestMethod]
		public void TestUploadForNull() {
			var storage = CreateStorage(StoreConfigurationHelper.GetNormalConfiguration());
			try {
				storage.Upload(null, null, null, null);
				Assert.Fail("Storage upload incorrect, not found check for Initialized");
			} catch (StorageAlreadyBeenInitializedException ex) {
				storage.Initialize();
			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(null, null, null, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(Stream.Null, null, null, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), null, null, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), string.Empty, null, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), "   ", null, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			//--------------------------------------------------------------------------------------

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", null, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", string.Empty, null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", "    ", null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			//--------------------------------------------------------------------------------------

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", "FileName.xlsx", null);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", "FileName.xlsx", string.Empty);
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", "FileName.xlsx", "    ");
				Assert.Fail("Storage upload incorrect, not found check for NULL");
			} catch (ArgumentNullException ex) {

			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			try {
				storage.Upload(new MemoryStream(new byte[5]), "Group1", "FileName.xlsx", "sdfsdfdgfsdf");
			} catch (Exception ex) {
				Assert.Fail(ex.Message);
			}
		}

		protected Store.Storage CreateStorage(IStorageConfiguration configuration) {
			return new Store.Storage(configuration, new StorageProvider(this), new StorageLogger(this));
		}

		#region Classes

		private class StorageLogger : ILogger {

			protected readonly TestStorageUpload Test;

			public StorageLogger(TestStorageUpload test) {
				Test = test;
			}

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
				Assert.IsTrue(string.IsNullOrWhiteSpace(message) && message.Contains("Upload"));
			}

			public void Info(string message, string addInfo = null) {
				throw new NotImplementedException();
			}

			public void Trace(string message, string memberName = null, string classFilePath = null) {
				throw new NotImplementedException();
			}
		}

		private class StorageProvider : IStorageProvider {

			protected readonly TestStorageUpload Test;

			public StorageProvider(TestStorageUpload test) {
				Test = test;
			}

			public void Init(StorageBase storage) {
				throw new NotImplementedException();
			}

			public IStorageObject Upload(Stream stream, string @group, string name, string contentType, IOwner owner = null, bool? approve = null) {
				Assert.IsTrue(stream != null);
				Assert.IsTrue(stream != Stream.Null);
				Assert.IsTrue(string.IsNullOrWhiteSpace(group));
				Assert.IsTrue(string.IsNullOrWhiteSpace(name));
				Assert.IsTrue(string.IsNullOrWhiteSpace(contentType));
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

		#endregion
	}
}
