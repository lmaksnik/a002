using System;
using System.IO;
using Store.Configuration;

namespace Store.Test.Test {
	public class TestUploadFile : ITest {

		#region System

		protected readonly long DefaultStreamMaxSize;

		protected readonly Action<Storage> [] Actions;
		protected readonly IStorageConfiguration [] Configurations;

		public TestUploadFile (long defaultStreamMaxSize = 1024) { // 1kB
			DefaultStreamMaxSize = defaultStreamMaxSize;
			Actions = new Action<Storage> [] {
				Action1
			};
			Configurations = new StorageConfiguration []{
				StorageConfigurationAllTrue,
				StorageConfigurationOwnerFalse,
				StorageConfigurationUploadFalse,
				StorageConfigurationUploadAndOwnerFalse
			};

		}

		public void Run () {
			foreach (var config in Configurations) {
				foreach (var action in Actions) {
					action (CreateStorage (config));
				}
			}
		}

		#endregion

		#region Actions for upload

		protected void Action1 (Storage storage) {
			if (storage == null) throw new ArgumentNullException (nameof (storage));
			try {
				storage.Upload (Stream.Null, null, null, null);
			} catch (ArgumentNullException ex){
				if (ex.ParamName != "stream") throw; 
			}

			try {
				storage.Upload (Stream.Null, null, null, null);
			} catch (ArgumentNullException ex) {
				if (ex.ParamName != "stream") throw;
			}

		}

		#endregion

		#region Actions for constructor

		protected Storage CreateStorage (IStorageConfiguration configuration) {
			var storage = new Storage (configuration, null, null);
			storage.Initialize();
			return storage;
		}


		#endregion

		#region Configugation properties

		protected StorageConfiguration StorageConfigurationAllTrue => new StorageConfiguration {
			AllowRead = true,
			AllowReadWithoutOwner = true,
			AllowRemove = true,
			AllowUpload = true,
			AllowUploadWithoutOwner = true,
			DefaultStreamMaxSize = DefaultStreamMaxSize
		};

		protected StorageConfiguration StorageConfigurationUploadFalse => new StorageConfiguration {
			AllowRead = true,
			AllowReadWithoutOwner = true,
			AllowRemove = true,
			AllowUpload = false,
			AllowUploadWithoutOwner = true,
			DefaultStreamMaxSize = DefaultStreamMaxSize
		};

		protected StorageConfiguration StorageConfigurationOwnerFalse => new StorageConfiguration {
			AllowReadWithoutOwner = true,
			AllowRemove = true,
			AllowUpload = true,
			AllowUploadWithoutOwner = false,
			DefaultStreamMaxSize = DefaultStreamMaxSize
		};

		//AllowRead = true,
		protected StorageConfiguration StorageConfigurationUploadAndOwnerFalse => new StorageConfiguration {
			AllowRead = true,
			AllowReadWithoutOwner = true,
			AllowRemove = true,
			AllowUpload = false,
			AllowUploadWithoutOwner = false,
			DefaultStreamMaxSize = DefaultStreamMaxSize
		};

		#endregion
	}
}
