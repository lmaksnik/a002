using System;
using System.IO;
using Store.Configuration.Owner;

namespace Store.Domain.DataStore {
	public interface IDataStore {

		void Init();

		IDataStoreObject Get(Guid id);

		IDataStoreObject[] GetNotApproved();

		bool Has(Guid id);

		IDataStoreObject Upload(Stream stream, string group, string name, string contentType, IOwner owner, bool approve);

		void Approve(IDataStoreObject storeObject, IOwner owner);

		Stream Download(IDataStoreObject storeObject, IOwner owner);

		void Delete(IDataStoreObject storeObject, IOwner owner);
	}
}
