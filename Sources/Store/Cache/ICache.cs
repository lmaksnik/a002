using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Store.Cache {
	public interface ICache : IInitializer, IDisposable {

		void AddToCache(IStorageObject storageObject);

		void RemoveFromCache(Guid id);

		void RemoveFromCache(IStorageObject storageObject);

		Stream GetFromCache(Guid id);

		Stream GetFromCache(IStorageObject storageObject);

		void ClearCache();
	}
}
