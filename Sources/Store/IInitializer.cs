using System;
using System.Collections.Generic;
using System.Text;

namespace Store {
	public interface IInitializer {

		void Init(StorageBase storage);

	}
}
