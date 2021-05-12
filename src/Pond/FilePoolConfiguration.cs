using System;

namespace Pond
{
    [Serializable]
    public class FilePoolConfiguration
    {
        /// <summary>
        /// FilePool name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// FilePool storage path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Each worker max file count
        /// </summary>
        public int WorkerMaxFile { get; set; } = 5000;

        /// <summary>
        /// Enable file watcher
        /// </summary>
        public int EnableFileWatcher { get; set; }

        /// <summary>
        /// File watcher path
        /// </summary>
        public int FileWatcherPath { get; set; }

        /// <summary>
        /// File watcher work thread
        /// </summary>
        public int FileWatcherThread { get; set; } = 1;

        /// <summary>
        /// File watcher file last write seconds.
        /// </summary>
        public int FileWatcherLastWrite { get; set; } = 30;

        /// <summary>
        /// Skip file size is zero
        /// </summary>
        public bool FileWatcherSkipZeroFile { get; set; } = true;

        /// <summary>
        /// Scan file watecher path interval(ms)
        /// </summary>
        public int ScanFileWatcherMillSeconds { get; set; } = 5000;

        /// <summary>
        /// Enable automatic return file to file pool
        /// </summary>
        public bool EnableAutoReturn { get; set; }

        /// <summary>
        /// Scan wait return file interval(ms)
        /// </summary>
        public int ScanReturnFileMillSeconds { get; set; } = 3000;

        /// <summary>
        /// Reutrn file expired time(s), beyond this time will automatic return file
        /// </summary>
        public int AutoReturnSeconds { get; set; } = 300;
    }
}
