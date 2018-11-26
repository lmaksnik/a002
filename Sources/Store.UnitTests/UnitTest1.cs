using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.UnitTests {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {
			var configurations = StoreConfigurationHelper.GetAllConfigurationVariations();
		}
	}
}
