using Microsoft.Extensions.Logging;
using Pond.Exceptions;
using Pond.Utilities;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Pond.Workers
{
    public class FileWorker : IFileWorker
    {

        private readonly ILogger _logger;

        /// <summary>
        /// FilePool name
        /// </summary>
        public string FilePoolName { get { return _configuration?.Name; } }

        /// <summary>
        /// FileWorker index
        /// </summary>
        public int Index { get; protected set; }

        /// <summary>
        /// FileWorker name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// FileWorker path
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Pending files count
        /// </summary>
        public int PendingCount { get { return _progressingFiles.Count; } }

        /// <summary>
        /// Progressing files count
        /// </summary>
        public int ProgressingCount { get { return _progressingFiles.Count; } }

        private readonly FilePoolConfiguration _configuration;

        private readonly ConcurrentQueue<PondFile> _pendingFiles;
        private readonly ConcurrentDictionary<string, PondFile> _progressingFiles;

        public FileWorker(ILogger<FileWorker> logger, int index, FilePoolConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            Index = index;
            Name = WorkerUtil.GenerateWorkerName(index);
            Path = WorkerUtil.GenerateWorkerPath(configuration.Path, Name);

            _pendingFiles = new ConcurrentQueue<PondFile>();
            _progressingFiles = new ConcurrentDictionary<string, PondFile>();
        }

        /// <summary>
        /// Write file
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public async Task<PondFile> WriteFileAsync(Stream stream, string fileExt)
        {
            Ensure.NotNullOrWhiteSpace(fileExt, nameof(fileExt));
            var filePath = GenerateFilePath(fileExt);
            var file = new PondFile(_configuration.Name, Index, filePath);

            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (stream.CanSeek)
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                    }
                    await stream.CopyToAsync(fs);
                }

                _pendingFiles.Enqueue(file);

                //TODO 文件写满时的操作

                return file;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FileWorker write file '{0}({1})' failed, exception:{2}.", Name, _configuration.Name, ex.Message);
                if (FileHelper.DeleteIfExists(filePath))
                {
                    _logger.LogDebug("Delete write failed file '{0}'.", filePath);
                }
                throw ex;
            }
            finally
            {
                stream?.Close();
                stream?.Dispose();
            }

        }

        /// <summary>
        /// Get file
        /// </summary>
        /// <returns></returns>
        public (bool, PondFile) GetFile()
        {
            if (_pendingFiles.TryDequeue(out PondFile file))
            {
                if (file != null)
                {
                    if (_progressingFiles.TryAdd(file.GenerateCode(), file))
                    {
                        return (true, file);
                    }
                    else
                    {
                        _logger.LogDebug("Add pending file to progressing failed.{0}({1})", file.Worker, file.FilePoolName);

                        //If file not exist in progressing files
                        if (!_progressingFiles.ContainsKey(file.GenerateCode()))
                        {
                            _pendingFiles.Enqueue(file);
                        }
                        return (false, null);
                    }
                }
            }

            return (false, null);
        }

        /// <summary>
        /// Return file
        /// </summary>
        /// <param name="file"></param>
        public void ReturnFile(PondFile file)
        {
            Ensure.NotNull(file, "PondFile");

            Match(file);

            var code = file.GenerateCode();

            if (_progressingFiles.TryGetValue(code, out PondFile progressFile))
            {
                _pendingFiles.Enqueue(progressFile);
                if (!_progressingFiles.TryRemove(code, out _))
                {
                    _logger.LogDebug("Return file try remove from progressing files failed. '{0}({1})'.", Name, FilePoolName);
                }
            }
            else
            {
                _logger.LogDebug("Return file not exist in progressing files, '{0}({1})'.", Name, FilePoolName);
            }
        }

        /// <summary>
        /// Release file
        /// </summary>
        /// <param name="file"></param>
        public void ReleaseFile(PondFile file)
        {
            Ensure.NotNull(file, "PondFile");

            Match(file);

            var code = file.GenerateCode();

            if (_progressingFiles.TryRemove(code, out PondFile progressFile))
            {
                if (!FileHelper.DeleteIfExists(progressFile.Path))
                {
                    _logger.LogDebug("Release file delete file '{0}' failed.", file.Path);
                }
            }
            else
            {
                _logger.LogDebug("Release file in progressing files failed, '{0}({1})'.", Name, FilePoolName);
            }
        }


        #region Private Methods
        /// <summary>
        /// Generate file storage path
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        private string GenerateFilePath(string fileExt)
        {
            var fileName = $"{Guid.NewGuid()}{fileExt}";
            var path = System.IO.Path.Combine(_configuration.Path, $"{Name}", fileName);
            return path;
        }


        private void Match(PondFile file)
        {
            if (file.Worker != Index)
            {
                throw new FileWorkerNotMatchException($"File '{file.Worker}({file.FilePoolName})' does not match current FileWorker '{Index}({FilePoolName})'");
            }
        }

        #endregion

    }
}
