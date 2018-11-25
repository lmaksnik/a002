using System;
using System.Collections.Generic;
using Store.Test.Test;

namespace Store.Test {
	class Program {
		static void Main (string [] args) {
			var program = new Program ();
			program.StartTest();
		}


		protected void StartTest(){
			var tests = new List<ITest> {
				new TestUploadFile()
			};

			foreach(var test in tests){
				test.Run();
			}
		}
	}
}
