using System;
using System.IO;
using Store.Configuration.Owner;
using Store.Domain.DataStore;
using Store.Implementation.DataStore.FilesDirectory.MetaData;

namespace Store.Implementation.DataStore.FilesDirectory {
	/// <summary>
	/// Провайдер для работы с файловой директорией
	/// </summary>
	public class FilesDirectoryDataStore : IDataStore {
		public FilesDirectoryDataStore (IFilesMetaDataStore metaDataStore, string[] defaultSourceFileDirectories, bool overrideFileExtensions, EFileDirectoriesScheme directoryScheme = EFileDirectoriesScheme.Default) {
			MetaData = metaDataStore ?? throw new ArgumentNullException(nameof(metaDataStore));
			if (defaultSourceFileDirectories == null || defaultSourceFileDirectories.Length < 1) throw new ArgumentNullException(nameof(defaultSourceFileDirectories));
			SourcesFileDirectories = defaultSourceFileDirectories;
			OverrideFileExtensions = overrideFileExtensions;
			DirectoriesScheme = directoryScheme;
		}

		#region Properties

		private readonly object _initLockObject = new object();

		protected bool IsInitialized { get; private set; }

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

		public IDataStoreObject[] GetNotApproved() {
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			return MetaData.GetNotApproved();
		}

		public bool Has(Guid id) {
			if (id == Guid.Empty) return false;
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			return MetaData.Has(id);
		}

		public IDataStoreObject Upload(Stream stream, string @group, string name, string contentType, IOwner owner, bool approve) {
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			if (stream == null || stream == Stream.Null) throw new ArgumentNullException(nameof(stream));

			if (string.IsNullOrWhiteSpace(name))
				name = Path.GetTempFileName();

			var fileName = name;
			if (OverrideFileExtensions)
				fileName = $"{name}.file";
			
			var obj = MetaData.Create(group, name, contentType, approve, fileName, GetSourceDirectory(), GetDestinationDirectory());
			OnSave(stream, Path.Combine(obj.SourcePath, obj.DestinationPath, obj.FileName));
			MetaData.Insert(obj);

			return obj;
		}

		public void Approve(IDataStoreObject storeObject, IOwner owner) {
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			if (storeObject == null) throw new ArgumentNullException(nameof(storeObject));

			var obj = (IFileDataStoreObject) storeObject;
			obj.SetIsApproved(true);
			MetaData.Update(obj);
		}

		public Stream Download(IDataStoreObject storeObject, IOwner owner) {
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			if (storeObject == null) throw new ArgumentNullException(nameof(storeObject));

			var obj = (IFileDataStoreObject) storeObject;
			return OnLoad(Path.Combine(obj.SourcePath, obj.DestinationPath, obj.FileName));
		}

		public void Delete(IDataStoreObject storeObject, IOwner owner) {
			if (IsInitialized) throw new Exception($"This instance \"{nameof(FilesDirectoryDataStore)}\" is not initialized.");
			if (storeObject == null) throw new ArgumentNullException(nameof(storeObject));

			var obj = (IFileDataStoreObject)storeObject;
			OnDelete(Path.Combine(obj.SourcePath, obj.DestinationPath, obj.FileName));
			MetaData.Delete(obj);
		}

		#region Protected methods

		private object _lockObjectGetSourceDirectory = new object();

		private int lastDirectoryIndex = 0;

		protected string GetSourceDirectory() {
			lock (_lockObjectGetSourceDirectory) {
				if (lastDirectoryIndex < 0 || lastDirectoryIndex >= SourcesFileDirectories.Length)
					lastDirectoryIndex = 0;
				else lastDirectoryIndex++;
				return SourcesFileDirectories[lastDirectoryIndex];
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

		protected void OnSave(Stream stream, string fullName) {
			if (File.Exists(fullName)) throw new Exception($"File \"{fullName}\" already exists.");
			using (var fileStream = new FileStream(fullName, FileMode.CreateNew, FileAccess.ReadWrite)) {
				stream.CopyTo(fileStream);
				fileStream.Flush();
			}
		}

		protected Stream OnLoad(string fullName) {
			if (File.Exists(fullName)) return new FileStream(fullName, FileMode.Open, FileAccess.Read);
			return Stream.Null;
		}

		protected void OnDelete(string fullName) {
			if (File.Exists(fullName))
				File.Delete(fullName);
		}

		#endregion

	}

	public enum EFileDirectoriesScheme {
		Default = 0,
		FoldersYearMonthDay = 1,
		FoldersYearMontDayHour = 2
	}
}
