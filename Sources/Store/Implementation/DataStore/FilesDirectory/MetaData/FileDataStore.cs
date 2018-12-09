using System;
using Store.Domain.DataStore;

namespace Store.Implementation.DataStore.FilesDirectory.MetaData {
	public class FileDataStore : IFileDataStoreObject {
		internal FileDataStore(IDataStore store, Guid id, string @group, string name, string contentType, bool isApproved, string fileName, string sourcePath, string destinationPath) {
			Store = store;
			Id = id;
			Group = @group;
			Name = name;
			ContentType = contentType;
			IsApproved = isApproved;
			FileName = fileName;
			SourcePath = sourcePath;
			DestinationPath = destinationPath;
		}

		public IDataStore Store { get; }
		public Guid Id { get; }
		public string Group { get; }
		public string Name { get; }
		public string ContentType { get; }
		public bool IsApproved { get; private set; }
		public string FileName { get; }
		public string SourcePath { get; }
		public string DestinationPath { get; }

		public void SetIsApproved(bool value) {
			IsApproved = value;
		}
	}
}
