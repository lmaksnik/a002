using System;
using System.Collections.Generic;
using System.Text;
using Store.Log;

namespace Store.Configuration {
	public interface IStorageConfiguration {

		bool AllowUploadWithoutOwner { get; }

		bool AllowReadWithoutOwner { get; }

		bool AllowRead { get; }

		bool AllowUpload { get; }

		bool AllowRemove { get; }

		long DefaultStreamMaxSize { get; }
	}
}
