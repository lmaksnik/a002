using Store.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using Store.Configuration.Owner;
using Store.Context;
using Store.Exceptions;
using Store.Exceptions.Store;
using Store.Logger;
using Store.Statistics;

namespace Store {
	public class Storage : StorageBase{

		#region Properties

		public readonly StorageOwnersCollection Owners = new StorageOwnersCollection();

		protected readonly IStorageConfiguration Configuration;

		#endregion

		#region Providers

		protected readonly IStorageProvider StorageProvider;

		#endregion

		public Storage(IStorageConfiguration configuration, IStorageProvider storageProvider, ILogger logger, IStatistics statistics = null) : base(logger, statistics, storageProvider) {
			Configuration = configuration;
			StorageProvider = storageProvider;
		}

		#region Public}

		protected virtual IStorageObject OnUpload(Stream stream, string group, string name, string contentType,
			IOwner owner = null, bool? approve = null) {
			if (HasUpload(owner)) throw new StorageReadonlyException();
			if (owner == null && !Configuration.AllowUploadWithoutOwner) throw new ArgumentNullException(nameof(owner));

			if (HasStreamSize(stream, owner)) throw new StreamExceedsAllowableSizeException();
			if (HasContentType(contentType, owner)) throw new ContentTypeNotAllowedException(contentType);

			return StorageProvider.Upload(stream, group, name, contentType, owner, approve);
		}

		public IStorageObject Upload(Stream stream, string group, string name, string contentType, IOwner owner = null, bool? approve = null) {
			if (!Initialized) throw new StorageAlreadyBeenInitializedException(this);
			if (stream == Stream.Null) throw new ArgumentNullException(nameof(stream), "Stream is null!");
			if (string.IsNullOrWhiteSpace(group)) throw new ArgumentNullException(nameof(group));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
			if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentNullException(nameof(contentType));

			var parameters = new List<ActionParameter> {
				new ActionParameter(nameof(stream), stream),
				new ActionParameter(nameof(group), group),
				new ActionParameter(nameof(name), name),
				new ActionParameter(nameof(contentType), contentType),
				new ActionParameter(nameof(owner), owner),
				new ActionParameter(nameof(approve), approve),
			};
			return Call(() => OnUpload(stream, group, name, contentType, owner, approve), parameters.ToArray());
		}

		protected virtual bool OnApprove(Guid id, IOwner owner = null) {
			if (HasRead(owner)) throw new StorageReadonlyException();
			return StorageProvider.Approve(id, owner);
		}

		public bool Approve(Guid id, IOwner owner = null) {
			if (!Initialized) throw new StorageAlreadyBeenInitializedException(this);
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			return Call(() => OnApprove(id, owner), new[] {
				new ActionParameter(nameof(id), id),
				new ActionParameter(nameof(owner), owner)
			});
		}

		protected virtual IStorageObject OnDownload(Guid id, IOwner owner = null) {
			if (HasRead(owner)) throw new StorageReadonlyException();
			return StorageProvider.Download(id, owner);
		}

		public IStorageObject Download(Guid id, IOwner owner = null) {
			if (!Initialized) throw new StorageAlreadyBeenInitializedException(this);
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			return Call(() => OnDownload(id, owner), new[] {
				new ActionParameter(nameof(id), id),
				new ActionParameter(nameof(owner), owner)
			});
		}

		protected virtual bool OnRemove(Guid id, IOwner owner = null) {
			if (HasRemove(owner)) throw new StorageReadonlyException();
			return StorageProvider.Remove(id, owner);
		}

		public bool Remove(Guid id, IOwner owner = null) {
			if (!Initialized) throw new StorageAlreadyBeenInitializedException(this);
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			return Call(() => OnRemove(id, owner), new[] {
				new ActionParameter(nameof(id), id),
				new ActionParameter(nameof(owner), owner)
			});
		}

		#endregion

		protected virtual bool HasUpload(IOwner owner) {
			return (owner != null && owner.AllowUpload) || Configuration.AllowUpload ||
			       Configuration.AllowUploadWithoutOwner;
		}

		protected virtual bool HasRead(IOwner owner) {
			return (owner != null && owner.AllowRead) || Configuration.AllowRead ||
			       Configuration.AllowReadWithoutOwner;
		}

		protected virtual bool HasRemove(IOwner owner) {
			return (owner != null && owner.AllowRemove) || Configuration.AllowRemove;
		}

		protected bool HasStreamSize(Stream stream, IOwner owner) {
			var maxSize = owner?.StreamMaxSize ?? Configuration.DefaultStreamMaxSize;
			return maxSize <= 0 || stream.Length <= maxSize;
		}

		protected bool HasContentType(string contentType, IOwner owner) {
			//todo(Maksim): Сделать проверку на тип контента
			return true;
		}
	}
}