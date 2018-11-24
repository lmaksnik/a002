namespace Store.Configuration.Owner {

	public interface IOwner : IStorageObject {

		string Name { get; set; }

		bool AllowRead { get; set; }

		bool AllowUpload { get; set; }

		bool AllowRemove{ get; set; }

		long? StreamMaxSize { get; set; }

	}
}
