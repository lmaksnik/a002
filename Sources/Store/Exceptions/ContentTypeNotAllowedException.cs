using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Exceptions {
	public class ContentTypeNotAllowedException : Exception {
		public ContentTypeNotAllowedException(string contentType) : base($"Content type \"{contentType}\" not allowed.") {
		}
	}
}
