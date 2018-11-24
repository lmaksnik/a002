using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Store.Configuration.Owner;

namespace Store {
	public interface IStorageProvider : IInitializer {

		IStorageObject Upload(Stream stream, string group, string name, string contentType, IOwner owner = null, bool? approve = null);

		bool Approve(Guid id, IOwner owner);

		IStorageObject Download(Guid id, IOwner owner);

		bool Remove(Guid id, IOwner owner);
	}
}
