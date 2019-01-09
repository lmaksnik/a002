using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Configuration {
	public interface IStorageConfiguration {

		bool AllowUploadWithoutOwner { get; }

		bool AllowUpload { get; }

		bool AllowRemove { get; }

		long DefaultStreamMaxSize { get; }
	}
}
