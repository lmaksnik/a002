using System;
using System.IO;
using Store.DataStore;

namespace Store.Implementation.DataStore.FilesDirectory.MetaData {
	public class FileDataStoreObject : IFileDataStoreObject {
		internal FileDataStoreObject(FilesDirectoryDataStore store, Guid id, string @group, string name, string contentType, bool isApproved, string fileName, string sourcePath, string destinationPath) {
			Store = store ?? throw new ArgumentNullException(nameof(store));
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

		private Stream _stream;

		public Stream Stream => _stream ?? (_stream = ((FilesDirectoryDataStore) Store).OnLoad(Path.Combine(SourcePath, DestinationPath)));

		#region File attributes

		public string FileName { get; }

		public string SourcePath { get; }

		public string DestinationPath { get; }

		#endregion
	}
}
