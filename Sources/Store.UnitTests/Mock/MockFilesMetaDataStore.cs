using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Configuration.Owner;
using Store.Implementation.DataStore.FilesDirectory;
using Store.Implementation.DataStore.FilesDirectory.MetaData;

namespace Store.UnitTests.Mock {
	public class MockFilesMetaDataStore : IFilesMetaDataStore {

		public List<IFileDataStoreObject> Store;
		public FilesDirectoryDataStore DataStore;

		public void Init(FilesDirectoryDataStore dataStore) {
			DataStore = dataStore;
			Store = new List<IFileDataStoreObject>();
		}

		public IFileDataStoreObject Get(Guid id) {
			if (DataStore == null) throw new ArgumentNullException(nameof(DataStore));
			if (Store == null) throw new ArgumentNullException(nameof(Store));
			return Store.FirstOrDefault(a => a.Id == id);
		}

		public IFileDataStoreObject Create(string @group, string name, string contentType, IOwner owner, bool approve,
			string fileName, string sourcePath, string destinationPath) {
			if (DataStore == null) throw new ArgumentNullException(nameof(DataStore));
			if (Store == null) throw new ArgumentNullException(nameof(Store));

			var obj = new FileDataStoreObject(DataStore, Guid.NewGuid(), group, name, contentType, approve, fileName,
				sourcePath, destinationPath);
			Store.Add(obj);
			return obj;
		}

		public void Update(Guid id, bool isApprove, IOwner owner = null) {
			if (DataStore == null) throw new ArgumentNullException(nameof(DataStore));
			if (Store == null) throw new ArgumentNullException(nameof(Store));

			var obj = Get(id) as FileDataStoreObject;
			if (obj == null) throw new NullReferenceException();

			obj.IsApproved = isApprove;
		}

		public void Delete(Guid id) {
			if (DataStore == null) throw new ArgumentNullException(nameof(DataStore));
			if (Store == null) throw new ArgumentNullException(nameof(Store));

			var obj = Get(id) as FileDataStoreObject;
			if (obj == null) throw new NullReferenceException();

			Store.Remove(obj);
		}

		public void Dispose() {
			if (DataStore == null) throw new ArgumentNullException(nameof(DataStore));
			if (Store == null) throw new ArgumentNullException(nameof(Store));
			DataStore = null;
			Store = null;
		}
	}
}
