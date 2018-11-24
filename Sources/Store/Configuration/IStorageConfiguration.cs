using System;
using System.Collections.Generic;
using System.Text;
using Store.Log;

namespace Store.Configuration {
	public interface IStorageConfiguration {

		bool AllowUploadWithoutOwner { get; set; }

		bool AllowReadWithoutOwner { get; set; }

		bool AllowRead { get; set; }

		bool AllowUpload { get; set; }

		bool AllowRemove { get; set; }

		long DefaultStreamMaxSize { get; set; }
	}
}
