using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Store.Domain.DataStore;
using Store.StreamProvider;

namespace Store.Cache {
	public class CacheObject {
		public CacheObject(IDataStoreObject dataStoreObject, Stream stream) {
			if (dataStoreObject == null) throw new ArgumentNullException(nameof(dataStoreObject));
			if (stream == Stream.Null) throw new ArgumentNullException(nameof(stream));
			Id = dataStoreObject.Id;
			DataStoreObject = dataStoreObject;
			Stream = stream;
		}

		public readonly Guid Id;

		public readonly IDataStoreObject DataStoreObject;

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
