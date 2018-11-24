using System;

namespace Store.Exceptions.Store {
	public class StoreHasAlreadyBeenInitializedException : Exception {

		private const string _msg = "This storage instance has already been initialized.";

		public StoreHasAlreadyBeenInitializedException(Storage storage) : base(_msg) {
			Storage = storage;
		}

		public StoreHasAlreadyBeenInitializedException(Storage storage, string message) : base($"{_msg} {message}") {
			Storage = storage;
		}

		public StoreHasAlreadyBeenInitializedException(Storage storage, string message, Exception innerException) : base($"{_msg} {message}", innerException) {
			Storage = storage;
		}

		public readonly Storage Storage;
	}
}
