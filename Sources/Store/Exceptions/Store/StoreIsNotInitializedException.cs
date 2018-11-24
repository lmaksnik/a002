using System;

namespace Store.Exceptions.Store {
	public class StoreIsNotInitializedException : Exception {

		private const string _msg = "This storage instance is not initialized.";

		public StoreIsNotInitializedException(Storage storage) : base(_msg) {
			Storage = storage;
		}

		public StoreIsNotInitializedException(Storage storage, string message) : base($"{_msg} {message}") {
			Storage = storage;
		}

		public StoreIsNotInitializedException(Storage storage, string message, Exception innerException) : base($"{_msg} {message}", innerException) {
			Storage = storage;
		}

		public readonly Storage Storage;
	}
}
