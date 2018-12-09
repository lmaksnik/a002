using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using Store.Domain.DataStore;
using Store.StreamProvider;

namespace Store.Cache {
	public class DefaultCacheProvider : ICache {

		protected readonly IDictionary<Guid, CacheObject> Cache;

		protected long CurrentSize { get; private set; }

		public DefaultCacheProvider() {
			Cache = new Dictionary<Guid, CacheObject>();
		}

		public void Init(StorageBase storage) {
			Cache.Clear();
			CurrentSize = 0;
		}

		public void AddToCache(IDataStoreObject dataStoreObject, Stream stream) {
			if (dataStoreObject != null && !Cache.ContainsKey(dataStoreObject.Id)) {
				Cache.Add(dataStoreObject.Id, new CacheObject(dataStoreObject, ToMemoryStream(stream)));
				CurrentSize += stream.Length;
			}
		}

		protected virtual MemoryStream ToMemoryStream(Stream stream) {
			if (stream is MemoryStream)
				return (MemoryStream)stream;
			var result = new MemoryStream();
			stream.CopyTo(result);
			return result;
		}

		public void RemoveFromCache(Guid id) {

		}

		public void RemoveFromCache(IDataStoreObject dataStoreObject) {
			if (dataStoreObject != null)
				RemoveFromCache(dataStoreObject.Id);
		}

		public Stream GetFromCache(Guid id) {
			return Stream.Null;
		}

		public Stream GetFromCache(IDataStoreObject dataStoreObject) {
			if (dataStoreObject == null) return Stream.Null;
			return GetFromCache(dataStoreObject.Id);
		}

		public void ClearCache() {
			Cache.Clear();
			CurrentSize = 0;
		}

		public void Dispose() {
			Cache.Clear();
		}

		public void AddToCache (IDataStoreObject dataStoreObject) {
			throw new NotImplementedException ();
		}
	}
}
