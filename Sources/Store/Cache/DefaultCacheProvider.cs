using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

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

		public void AddToCache(IStorageObject storageObject, Stream stream) {
			if (storageObject != null && !Cache.ContainsKey(storageObject.Id)) {
				Cache.Add(storageObject.Id, new CacheObject(storageObject, ToMemoryStream(stream)));
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

		public void RemoveFromCache(IStorageObject storageObject) {
			if (storageObject != null)
				RemoveFromCache(storageObject.Id);
		}

		public Stream GetFromCache(Guid id) {
			
		}

		public Stream GetFromCache(IStorageObject storageObject) {
			if (storageObject == null) return Stream.Null;
			return GetFromCache(storageObject.Id);
		}

		public void ClearCache() {
			Cache.Clear();
			CurrentSize = 0;
		}

		public void Dispose() {
			Cache.Clear();
		}
	}
}
