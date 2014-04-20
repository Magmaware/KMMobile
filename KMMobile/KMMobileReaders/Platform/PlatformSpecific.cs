using System;
using System.IO;
using System.Linq;

namespace KMMobile.Platform
{
	public class PlatformSpecific
	{
        public
            #if WINDOWS_PHONE
            async
            #endif
		static string[] Files(string filePattern)
        {
			#if __ANDROID__
			var folder = Android.OS.Environment.GetExternalStoragePublicDirectory("CF2").AbsolutePath;
			#elif __IOS__
			var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			#elif WINDOWS_PHONE
            var _files = new List<string>();
			var _sdCard = (await Microsoft.Phone.Storage.ExternalStorage.GetExternalStorageDevicesAsync ()).FirstOrDefault ();
			if (_sdCard != null) {
				try {
					var folder = await _sdCard.GetFolderAsync (@"\CF2");
					var files = await folder.GetFilesAsync ();
					foreach (var file in files) {
                        _files.Add(file.Path);
					}
				} catch (Exception ex) {
				}
			}
            return _files.Select(c => Path.GetFileName(c)).ToArray();
            #elif WINDOWS_STORE
            #else
            var folder = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "CF2");
			#endif
			#if !WINDOWS_PHONE && !WINDOWS_STORE
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
			return Directory.GetFiles(folder, filePattern).Select(c => Path.GetFileName(c)).ToArray();
            #endif
        }

		public 
			#if WINDOWS_PHONE
			async
			#endif
		static string[] ReadCf2 (string filename)
		{
			#if __ANDROID__
			var document = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory("CF2").AbsolutePath, filename);
			#elif __IOS__
			var document = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filename);
			#elif WINDOWS_PHONE
			var _sdCard = (await Microsoft.Phone.Storage.ExternalStorage.GetExternalStorageDevicesAsync ()).FirstOrDefault ();
			if (_sdCard != null) {
				try {
					var folder = await _sdCard.GetFolderAsync (@"\CF2");
					var files = await folder.GetFilesAsync ();
					foreach (var file in files) {
						if (Path.GetFileName (file.Path) == filename) {
							using (var stream = await file.OpenForReadAsync ()) {
								using (var textReader = new StreamReader (stream)) {
									var res = textReader.ReadToEnd ().Split ('\r').Select (r => r.Trim ()).ToArray ();
                                    return res;
								}
							}
						}
					}
				} catch (Exception ex) {
				}
			}
			return new string[0];
			#elif WINDOWS_STORE
			#else
			var document = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "CF2", filename);
			#endif
			#if !WINDOWS_PHONE && !WINDOWS_STORE
            if (!Directory.Exists(Path.GetDirectoryName(document)))
                Directory.CreateDirectory(Path.GetDirectoryName(document));
			if (File.Exists (document)) {
				using (var stream = File.OpenText (document)) {
					return stream.ReadToEnd ().Split ('\r').Select (r => r.Trim ()).ToArray ();
				}
			}
			return new string[0];
			#endif
		}
	}
}
