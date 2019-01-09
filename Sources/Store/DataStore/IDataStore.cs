using System;
using System.IO;
using Store.Configuration.Owner;

namespace Store.DataStore {

	public interface IDataStore : IInitializer, IDisposable {

		IDataStoreObject Get(Guid id);

		IDataStoreObject Upload(Stream stream, string group, string name, string contentType, IOwner owner, bool approve = false);

		void Approve(Guid id, IOwner owner = null);

		void Delete(Guid id);

	}
}
