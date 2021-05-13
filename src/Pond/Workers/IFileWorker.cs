using System.IO;
using System.Threading.Tasks;

namespace Pond.Workers
{
    public interface IFileWorker
    {
        /// <summary>
        /// FilePool name
        /// </summary>
        string FilePoolName { get; }

        /// <summary>
        /// FileWorker index
        /// </summary>
        int Index { get; }

        /// <summary>
        /// FileWorker name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// FileWorker path
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Pending files count
        /// </summary>
        int PendingCount { get; }

        /// <summary>
        /// Progressing files count
        /// </summary>
        int ProgressingCount { get; }

        /// <summary>
        /// Write file
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        Task<PondFile> WriteFileAsync(Stream stream, string fileExt);

        /// <summary>
        /// Get file
        /// </summary>
        /// <returns></returns>
        (bool, PondFile) GetFile();

        /// <summary>
        /// Return file
        /// </summary>
        /// <param name="file"></param>
        void ReturnFile(PondFile file);

        /// <summary>
        /// Release file
        /// </summary>
        /// <param name="file"></param>
        void ReleaseFile(PondFile file);
    }
}
