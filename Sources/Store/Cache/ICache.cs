using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Store.Domain.DataStore;
using Store.StreamProvider;

namespace Store.Cache {
	public interface ICache : IInitializer, IDisposable {

		void AddToCache(IDataStoreObject dataStoreObject);

		void RemoveFromCache(Guid id);

		void RemoveFromCache(IDataStoreObject dataStoreObject);

		Stream GetFromCache(Guid id);

		Stream GetFromCache(IDataStoreObject dataStoreObject);

		void ClearCache();
	}
}
