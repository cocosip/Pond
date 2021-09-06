using Microsoft.Extensions.Logging;
using Pond.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pond
{
    public class Worker
    {
        public FilePoolConfiguration Configuration { get; }

        /// <summary>
        /// Worker Key
        /// </summary>
        public WorkerKey Key { get; }

        /// <summary>
        /// Worker类型
        /// </summary>
        public WorkerStyle Style { get; private set; }


        /// <summary>
        /// 当前Worker的路径地址
        /// </summary>
        public string CurrentPath { get; private set; }

        private readonly int _workerId;
        private bool _loaded = false;

        private readonly ConcurrentQueue<PondFile> _pendingQueue;

        private readonly ConcurrentDictionary<string, PondFile> _processDict;


        private readonly ILogger _logger;
        private readonly IWorkerNamingNormalizer _workerNamingNormalizer;
        public Worker(
            ILogger<Worker> logger,
            IWorkerNamingNormalizer workerNamingNormalizer,
            FilePoolConfiguration configuration,
            int workerId)
        {
            _logger = logger;
            _workerNamingNormalizer = workerNamingNormalizer;

            Configuration = configuration;
            _workerId = workerId;

            var name = _workerNamingNormalizer.NormalizeWorkerName(workerId);
            CurrentPath = Path.Combine(configuration.Path, name);

            Key = new WorkerKey(configuration.Name, workerId);
            Style = WorkerStyle.Default;
            _pendingQueue = new ConcurrentQueue<PondFile>();
            _processDict = new ConcurrentDictionary<string, PondFile>();

        }

        /// <summary>
        /// 是否已经加载
        /// </summary>
        /// <returns></returns>
        public bool Loaded()
        {
            return _loaded;
        }

        /// <summary>
        /// 加载目录下的文件(索引信息)到队列中
        /// </summary>
        public void Load()
        {
            if (_loaded)
            {
                _logger.LogInformation("Worker '{0}' 已经加载文件,将不会重复加载.", Key);
                return;
            }

            var files = Directory.GetFiles(CurrentPath);
            foreach (var file in files)
            {
                var pondFile = new PondFile(Configuration.Name, _workerId, file);
                _pendingQueue.Enqueue(pondFile);
            }

            _loaded = true;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public PondFile Write(Stream stream, string ext)
        {
            //当前的Worker不能写,则直接返回
            if (Style != WorkerStyle.WriteOnly || Style != WorkerStyle.ReadWrite)
            {

            }


            var filePath = CreateFilePath(ext);
            try
            {

                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    stream.CopyTo(fs);
                }

                //添加索引
                var pondFile = new PondFile(Configuration.Name, _workerId, filePath);
                _pendingQueue.Enqueue(pondFile);

                //判断当前的Worker是否已经写满

                if (_pendingQueue.Count + _processDict.Count >= Configuration.QueueMaxFile)
                {
                    //当Worker已经写满的操作,  只写->只读 |  读写->只读
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker '{0}' 写入文件出错,异常信息 {1}.", Key, ex.Message);

                //如果写入失败,文件都需要被删除,否则内存中的索引跟文件将不一致
                FileHelper.DeleteIfExists(filePath);
            }

            return default;
        }

        public List<PondFile> Get(int count)
        {
            if (Style != WorkerStyle.ReadOnly || Style != WorkerStyle.ReadWrite)
            {
                //不是可读的类型
            }

            if (count < 1)
            {
                throw new ArgumentException("获取的 count 数量不能小于1 .");
            }

            var files = new List<PondFile>();
            var takeCount = _pendingQueue.Count >= count ? count : _pendingQueue.Count;

            for (int i = 0; i < takeCount; i++)
            {
                if (_pendingQueue.TryDequeue(out PondFile file))
                {
                    files.Add(file);
                    //添加到处理中的集合
                    if (!_processDict.TryAdd(file.FilePath, file))
                    {
                        _logger.LogDebug("Worker '{0}' 添加处理中的文件 '{0}' 失败.", Key, file);
                    }
                }
            }

            if (files.Count < 0)
            {

            }


            return files;

        }


        /// <summary>
        /// 生成新建文件的文件路径
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        private string CreateFilePath(string ext)
        {
            return Path.Combine(CurrentPath, $"{Guid.NewGuid():D}{ext}");
        }



    }
}
