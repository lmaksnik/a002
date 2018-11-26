using System;

namespace Store.Exceptions {
	public class StorageConfigurationException : Exception {
		public StorageConfigurationException(string parameterName) : base($"Parameter incorrent \"{parameterName}\"") {
			ParameterName = parameterName;
		}

		public StorageConfigurationException(string parameterName, string message) : base($"Parameter incorrent \"{parameterName}\" - {message}") {
			ParameterName = parameterName;
		}

		public StorageConfigurationException(string parameterName, string message, Exception innerException) : base($"Parameter incorrent \"{parameterName}\" - {message}", innerException) {
			ParameterName = parameterName;
		}

		public string ParameterName { get; }
	}
}
