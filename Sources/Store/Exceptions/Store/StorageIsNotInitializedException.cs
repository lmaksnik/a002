using System;

namespace Store.Exceptions.Store {
	public class StorageIsNotInitializedException : Exception {

		private const string _msg = "This storage instance is not initialized.";

		public StorageIsNotInitializedException(StorageBase storage) : base(_msg) {
			Storage = storage;
		}

		public StorageIsNotInitializedException(StorageBase storage, string message) : base($"{_msg} {message}") {
			Storage = storage;
		}

		public StorageIsNotInitializedException(StorageBase storage, string message, Exception innerException) : base($"{_msg} {message}", innerException) {
			Storage = storage;
		}

		public readonly Storage Storage;
	}
}
