using System;
using System.Collections.Generic;
using System.Text;
using Store.Configuration;

namespace Store.UnitTests {
	public class StoreConfigurationHelper {

		/// <summary>
		/// Перечень всех вариций конфигураций
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<IStorageConfiguration> GetAllConfigurationVariations() {
			yield return null;
			foreach (var configuration in BuildConfigurationByAllowUploadWithoutOwner()) {
				yield return configuration;
			}
		}

		public static IStorageConfiguration GetNormalConfiguration() {
			return new TestStorageConfigurations(true, true, true, true, true, 1024);
		}

		protected static IEnumerable<IStorageConfiguration> BuildConfigurationByStreamMaxSize(bool allowUploadWithoutOwner, bool allowReadWithoutOwner, bool allowRead, bool allowUpload, bool allowRemove) {
			yield return new TestStorageConfigurations(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, allowUpload, allowRemove, 0);
			yield return new TestStorageConfigurations(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, allowUpload, allowRemove, -1024);
			yield return new TestStorageConfigurations(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, allowUpload, allowRemove, 1024);
		}

		protected static IEnumerable<IStorageConfiguration> BuildConfigurationByAllowRemove(bool allowUploadWithoutOwner, bool allowReadWithoutOwner, bool allowRead, bool allowUpload) {
			foreach (var configuration in BuildConfigurationByStreamMaxSize(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, allowUpload, true)) {
				yield return configuration;
			}
			foreach (var configuration in BuildConfigurationByStreamMaxSize(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, allowUpload, false)) {
				yield return configuration;
			}
		}

		protected static IEnumerable<IStorageConfiguration> BuildConfigurationByAllowUpload(bool allowUploadWithoutOwner, bool allowReadWithoutOwner, bool allowRead) {
			foreach (var configuration in BuildConfigurationByAllowRemove(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, true)) {
				yield return configuration;
			}
			foreach (var configuration in BuildConfigurationByAllowRemove(allowUploadWithoutOwner, allowReadWithoutOwner, allowRead, false)) {
				yield return configuration;
			}
		}

		protected static IEnumerable<IStorageConfiguration> BuildConfigurationByAllowRead(bool allowUploadWithoutOwner, bool allowReadWithoutOwner) {
			foreach (var configuration in BuildConfigurationByAllowUpload(allowUploadWithoutOwner, allowReadWithoutOwner, true)) {
				yield return configuration;
			}
			foreach (var configuration in BuildConfigurationByAllowUpload(allowUploadWithoutOwner, allowReadWithoutOwner, false)) {
				yield return configuration;
			}
		}

		protected static IEnumerable<IStorageConfiguration> BuildConfigurationByAllowReadWithoutOwner(bool allowUploadWithoutOwner) {
			foreach (var configuration in BuildConfigurationByAllowRead(allowUploadWithoutOwner, true)) {
				yield return configuration;
			}
			foreach (var configuration in BuildConfigurationByAllowRead(allowUploadWithoutOwner, false)) {
				yield return configuration;
			}
		}

		protected static IEnumerable<IStorageConfiguration> BuildConfigurationByAllowUploadWithoutOwner() {
			foreach (var configuration in BuildConfigurationByAllowReadWithoutOwner(true)) {
				yield return configuration;
			}
			foreach (var configuration in BuildConfigurationByAllowReadWithoutOwner(false)) {
				yield return configuration;
			}
		}
	}

	public class TestStorageConfigurations : IStorageConfiguration {
		public TestStorageConfigurations(bool allowUploadWithoutOwner, bool allowReadWithoutOwner, bool allowRead, bool allowUpload, bool allowRemove, long defaultStreamMaxSize) {
			AllowUploadWithoutOwner = allowUploadWithoutOwner;
			AllowReadWithoutOwner = allowReadWithoutOwner;
			AllowRead = allowRead;
			AllowUpload = allowUpload;
			AllowRemove = allowRemove;
			DefaultStreamMaxSize = defaultStreamMaxSize;
		}

		public bool AllowUploadWithoutOwner { get; }

		public bool AllowReadWithoutOwner { get; }

		public bool AllowRead { get; }

		public bool AllowUpload { get; }

		public bool AllowRemove { get; }

		public long DefaultStreamMaxSize { get; }
	}
}
