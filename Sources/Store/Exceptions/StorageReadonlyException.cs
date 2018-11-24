using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Exceptions {
	public class StorageReadonlyException : Exception {
		public StorageReadonlyException() : base("Writing is not possible, only reading.") {
		}

		public StorageReadonlyException(string message) : base($"Writing is not possible, only reading. {message}") {
		}

		public StorageReadonlyException(string message, Exception innerException) : base($"Writing is not possible, only reading. {message}", innerException) {
		}
	}
}
