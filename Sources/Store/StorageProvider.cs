using System;
using System.IO;
using Store.Cache;
using Store.Configuration.Owner;

namespace Store {
	public class StorageProvider : IStorageProvider {
		public StorageProvider (ICache cache) {
		}

		public bool Approve (Guid id, IOwner owner) {
			throw new NotImplementedException ();
		}

		public IStorageObject Download (Guid id, IOwner owner) {
			throw new NotImplementedException ();
		}

		public void Init (StorageBase storage) {
			throw new NotImplementedException ();
		}

		public bool Remove (Guid id, IOwner owner) {
			throw new NotImplementedException ();
		}

		public IStorageObject Upload (Stream stream, string group, string name, string contentType, IOwner owner = null, bool? approve = null) {
			throw new NotImplementedException ();
		}
	}
}
