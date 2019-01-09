using System;

namespace Store.Exceptions {
	public class ContentTypeNotAllowedException : Exception {
		public ContentTypeNotAllowedException(string contentType) : base($"Content type \"{contentType}\" not allowed.") {
		}
	}
}
