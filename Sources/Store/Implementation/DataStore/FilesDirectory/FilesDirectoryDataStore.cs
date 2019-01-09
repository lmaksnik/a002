using System;
using System.IO;
using System.Linq;
using Store.Configuration.Owner;
using Store.DataStore;

namespace Store.Implementation.DataStore.FilesDirectory {
	/// <summary>
	/// Провайдер для работы с файловой директорией
	/// </summary>
	public class FilesDirectoryDataStore : IDataStore {
		public FilesDirectoryDataStore (IFilesMetaDataStore metaDataStore, string[] defaultSourceFileDirectories, bool overrideFileExtensions, EFileDirectoriesScheme directoryScheme = EFileDirectoriesScheme.Default) {
			MetaData = metaDataStore ?? throw new ArgumentNullException(nameof(metaDataStore));

			if (defaultSourceFileDirectories == null) throw new ArgumentNullException(nameof(defaultSourceFileDirectories));
			defaultSourceFileDirectories = defaultSourceFileDirectories.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();
			if (defaultSourceFileDirectories.Length < 1) throw new ArgumentNullException(nameof(defaultSourceFileDirectories));

			SourcesFileDirectories = defaultSourceFileDirectories;
			OverrideFileExtensions = overrideFileExtensions;
			DirectoriesScheme = directoryScheme;
		}

		#region Properties

		private readonly object _initLockObject = new object();

		public bool IsInitialized { get; private set; }

		public readonly bool OverrideFileExtensions;

		public readonly IFilesMetaDataStore MetaData;

		public readonly string[] SourcesFileDirectories;

		public readonly EFileDirectoriesScheme DirectoriesScheme;

		#endregion

		public void Init() {
			lock (_initLockObject) {
				if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" has already been initialized.");
				MetaData.Init(this);
				IsInitialized = true;
			}
		}

		public IDataStoreObject Get(Guid id) {
			if (id == Guid.Empty) return null;
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			return MetaData.Get(id);
		}

		public IDataStoreObject Upload(Stream stream, string @group, string name, string contentType, IOwner owner, bool approve) {
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			if (stream == null || stream == Stream.Null) throw new ArgumentNullException(nameof(stream));

			if (string.IsNullOrWhiteSpace(name))
				name = Path.GetTempFileName();

			var fileName = name;
			if (OverrideFileExtensions)
				fileName = $"{name}.file";

			var sourcePath = GetSourceDirectory();
			var destinationPath = GetDestinationDirectory();

			OnUpload(stream, Path.Combine(sourcePath, destinationPath, fileName));
			return MetaData.Create(group, name, contentType, owner, approve, fileName, sourcePath, destinationPath);
		}

		public void Approve(Guid id, IOwner owner = null) {
			if (id == Guid.Empty) return;
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");

			MetaData.Update(id, true, owner);
		}

		public void Delete(Guid id) {
			if (id == Guid.Empty) return;
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");

			var obj = MetaData.Get(id);
			if (obj == null) throw new Exception($"File with id \"{id}\" not found.");
			
			OnDelete(Path.Combine(obj.SourcePath, obj.DestinationPath, obj.FileName));
			MetaData.Delete(obj.Id);
		}


		#region Protected methods

		private readonly object _lockObjectGetSourceDirectory = new object();

		private int _lastDirectoryIndex;

		protected string GetSourceDirectory() {
			lock (_lockObjectGetSourceDirectory) {
				if (_lastDirectoryIndex < 0 || _lastDirectoryIndex >= SourcesFileDirectories.Length)
					_lastDirectoryIndex = 0;
				else _lastDirectoryIndex++;
				return SourcesFileDirectories[_lastDirectoryIndex];
			}
		}

		protected string GetDestinationDirectory() {
			var today = DateTime.Now;
			switch (DirectoriesScheme) {
				case EFileDirectoriesScheme.Default:
				case EFileDirectoriesScheme.FoldersYearMonthDay: {
					return Path.Combine(today.Year.ToString(), today.Month.ToString(), today.Day.ToString());
				}
				case EFileDirectoriesScheme.FoldersYearMontDayHour: {
					return Path.Combine(today.Year.ToString(), today.Month.ToString(), today.Day.ToString(), today.Hour.ToString());
				}
				default: throw new Exception($"Unknown enum item \"{DirectoriesScheme}\"");
			}
		}

		#endregion

		#region Handlers
		
		protected void OnUpload(Stream stream, string fullPath) {
			if (File.Exists(fullPath)) throw new Exception($"File \"{fullPath}\" already exists.");
			using (var fileStream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.ReadWrite)) {
				stream.CopyTo(fileStream);
				fileStream.Flush();
			}
		}

		protected internal Stream OnLoad(string fullPath) {
			if (File.Exists(fullPath))
				return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
			return Stream.Null;
		}

		protected void OnDelete(string fullPath) {
			if (File.Exists(fullPath))
				File.Delete(fullPath);
		}

		#endregion

		public void Dispose() {
			lock (_initLockObject) {
				if (!IsInitialized) return;
				MetaData.Dispose();
				IsInitialized = false;
			}
		}
	}

	public enum EFileDirectoriesScheme {
		Default = 0,
		FoldersYearMonthDay = 1,
		FoldersYearMontDayHour = 2
	}
}
