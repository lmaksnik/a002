using Store.Domain.DataStore;

namespace Store.Implementation.DataStore.FilesDirectory.MetaData {
	public interface IFileDataStoreObject : IDataStoreObject {

		string FileName { get; }

		string SourcePath { get; }

		string DestinationPath { get; }

		void SetIsApproved(bool value);

	}
}
