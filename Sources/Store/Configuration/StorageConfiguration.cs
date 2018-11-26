using System;
namespace Store.Configuration {
	public class StorageConfiguration : IStorageConfiguration {

		public StorageConfiguration (){}


		public bool AllowUploadWithoutOwner { get; set; }

		public bool AllowReadWithoutOwner { get; set; }

		public bool AllowRead { get; set; }

		public bool AllowUpload { get; set; }

		public bool AllowRemove { get; set; }

		public long DefaultStreamMaxSize { get; set; }
	}
}