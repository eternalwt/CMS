using System;
using System.Collections.Generic;
using System.Text;
 

namespace CMS.Data {
	public partial class DataSettingsHelper {
		private static bool? _databaseIsInstalled;
		public static bool DatabaseIsInstalled() {
			//if (!_databaseIsInstalled.HasValue) {
				var manager = new DataSettingsManager();
				var settings = manager.LoadSettings();
				_databaseIsInstalled = settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
				return false;
			//}
			//return _databaseIsInstalled.Value;
		}

		public static void ResetCache() {
			_databaseIsInstalled = null;
		}

		public static string ConnectionString {
			get {
				ICacheManager cacheManager = new MemoryCacheManager();
				return cacheManager.Get("ConnectionString", () => {
					return new DataSettingsManager().LoadSettings().DataConnectionString;
				});
			}
		}
	}
}
