using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Store.Configuration.Owner;
using Store.Implementation.DataStore.FilesDirectory.MetaData;

namespace Store.Implementation.DataStore.FilesDirectory {
	public interface IFilesMetaDataStore : IDisposable {

		void Init(FilesDirectoryDataStore dataStore);

		IFileDataStoreObject Get(Guid id);

		IFileDataStoreObject Create(string group, string name, string contentType, IOwner owner, bool approve, string fileName, string sourcePath, string destinationPath);

		void Update(Guid id, bool isApprove, IOwner owner = null);

		void Delete(Guid id);

	}
}
