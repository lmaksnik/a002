using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Store.Cache {
	public class CacheObject {
		public CacheObject(IStorageObject storageObject, Stream stream) {
			if (storageObject == null) throw new ArgumentNullException(nameof(storageObject));
			if (stream == Stream.Null) throw new ArgumentNullException(nameof(stream));
			Id = storageObject.Id;
			StorageObject = storageObject;
			Stream = stream;
		}

		public readonly Guid Id;

		public readonly IStorageObject StorageObject;

		protected readonly Stream Stream;

		public DateTime LastTimeRead { get; private set; }

		public int CountRequests { get; private set; }

		public Stream GetStream() {
			CountRequests++;
			LastTimeRead = DateTime.Now;
			return Stream;
		}

	}
}
