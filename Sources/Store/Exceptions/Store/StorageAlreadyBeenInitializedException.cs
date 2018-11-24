using System;

namespace Store.Exceptions.Store {
	public class StorageAlreadyBeenInitializedException : Exception {

		private const string _msg = "This storage instance has already been initialized.";

		public StorageAlreadyBeenInitializedException(Storage storage) : base(_msg) {
			Storage = storage;
		}

		public StorageAlreadyBeenInitializedException(Storage storage, string message) : base($"{_msg} {message}") {
			Storage = storage;
		}

		public StorageAlreadyBeenInitializedException(Storage storage, string message, Exception innerException) : base($"{_msg} {message}", innerException) {
			Storage = storage;
		}

		public readonly Storage Storage;
	}
}
