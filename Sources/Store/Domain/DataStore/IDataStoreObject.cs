using System;

namespace Store.Domain.DataStore {
	public interface IDataStoreObject {

		IDataStore Store { get; }

		Guid Id { get; }

		string Group { get; }

		string Name { get; }

		string ContentType { get; }

		bool IsApproved { get; }

		#region Methods



		#endregion
	}
}
