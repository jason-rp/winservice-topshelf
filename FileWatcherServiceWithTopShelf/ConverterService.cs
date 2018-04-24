using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherServiceWithTopShelf
{
    public class ConverterService
    {
        private FileSystemWatcher _watcher;

        public bool Start()
        {
            _watcher = new FileSystemWatcher(@"C:\temp\a", "*_in.txt");
            _watcher.Created += FileCreated;

            _watcher.IncludeSubdirectories = false;
            _watcher.EnableRaisingEvents = true;

            return true;
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            var content = File.ReadAllText(e.FullPath);
            var uperContent = content.ToUpperInvariant();
            var dir = Path.GetDirectoryName(e.FullPath);
            var convertFileName = Path.GetFileName(e.FullPath) + ".converted";
            var convertPath = Path.Combine(dir, convertFileName);
            File.WriteAllText(convertPath, uperContent);
            
        }

        public bool Stop()
        {
            _watcher.Dispose();
            return true;
        }
    }
}
