using System;
using Store.Implementation.DataStore.FilesDirectory.MetaData;

namespace Store.Implementation.DataStore.FilesDirectory {
	public interface IFilesMetaDataStore {

		void Init(FilesDirectoryDataStore dataStore);

		IFileDataStoreObject Get(Guid id);

		IFileDataStoreObject[] GetNotApproved();

		bool Has(Guid id);

		IFileDataStoreObject Create(string @group, string name, string contentType, bool isApproved, string fileName, string sourcePath, string destinationPath);

		void Insert(IFileDataStoreObject obj);

		void Update(IFileDataStoreObject obj);

		void Delete(IFileDataStoreObject obj);

	}
}
