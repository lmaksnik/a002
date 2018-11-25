using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Store.Configuration.Owner {
	public class StorageOwnersCollection : ICollection, IInitializer {
		public void Add(IOwner owner) {
			lock (_innerCollection) {
				
			}
		}

		public void Remove(IOwner owner) {
			lock (_innerCollection) {
				
			}
		}

		public IOwner GetById(Guid id) {
			lock (_innerCollection) {
				return null;
			}
		}

		#region System
	
		private readonly ArrayList _innerCollection = new ArrayList();

		IEnumerator IEnumerable.GetEnumerator() {
			return _innerCollection.GetEnumerator();
		}

		public void CopyTo(Array array, int index) {
			_innerCollection.CopyTo(array, index);
		}

		public int Count => _innerCollection.Count;

		public bool IsSynchronized => _innerCollection.IsSynchronized;

		public object SyncRoot => _innerCollection.SyncRoot;

		#endregion

		public void Init (StorageBase storage) {
			throw new NotImplementedException ();
		}
	}
}
