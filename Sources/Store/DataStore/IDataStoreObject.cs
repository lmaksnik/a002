using System;
using System.IO;

namespace Store.DataStore {
	public interface IDataStoreObject {

		IDataStore Store { get; }

		Guid Id { get; }

		string Group { get; }

		string Name { get; }

		string ContentType { get; }

		bool IsApproved { get; }

		Stream Stream { get; }

	}
}
