using System;
using System.Collections.Generic;
using Store.Test.Test;
using Store.Test.Tests.Storage;

namespace Store.Test {
	class Program {
		static void Main (string [] args) {
			var program = new Program ();
			program.StartTest();
		}


		protected void StartTest(){
			var tests = new List<ITest> {
				new TestStorageConstructor()
			};

			foreach(var test in tests){
				test.Run();
			}
		}
	}
}
