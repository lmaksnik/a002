namespace Store.Configuration.Owner {

	public interface IOwner : IStorageObject {

		string Name { get; }

		bool AllowUpload { get; }

		bool AllowRemove{ get; }

		long? StreamMaxSize { get; }

	}
}
