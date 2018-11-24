using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Store {
	public interface IStorageObject {

		Guid Id { get; set; }

		string Group { get; set; }

		string Name { get; set; }

		string ContentType { get; set; }

		Stream Stream { get; set; }
	}
}
