using System;

namespace Store.Exceptions {
	public class StreamExceedsAllowableSizeException : Exception {
		public StreamExceedsAllowableSizeException() : base("Stream exceeds allowable size.") {
		}

		public StreamExceedsAllowableSizeException(string message) : base($"Stream exceeds allowable size. {message}") {
		}

		public StreamExceedsAllowableSizeException(string message, Exception innerException) : base($"Stream exceeds allowable size. {message}", innerException) {
		}
	}
}
