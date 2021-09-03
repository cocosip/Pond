using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Pond
{
    public class Worker
    {

        /// <summary>
        /// 当前Worker的Id
        /// </summary>
        private readonly int _workerId;

        /// <summary>
        /// 文件数据队列
        /// </summary>
        private readonly ConcurrentQueue<PondFile> _pendingQueue;


        /// <summary>
        /// Worker所在文件池的配置信息
        /// </summary>
        public FilePoolConfiguration Configuration { get; }

        /// <summary>
        /// 当前Worker的Key
        /// </summary>
        public WorkerKey Key { get; }

        /// <summary>
        /// 当前Worker的存储路径
        /// </summary>
        public string CurrentPath { get; }

        /// <summary>
        /// 类型
        /// </summary>
        public WorkerStyle Style { get; }

        /// <summary>
        /// 是否已经加载
        /// </summary>
        public bool Loaded { get; }


        private readonly ILogger _logger;

        public Worker(
            ILogger<Worker> logger,
            FilePoolConfiguration configuration,
            int workerId)
        {

            _logger = logger;

            _workerId = workerId;
            Configuration = configuration;
            Key = new WorkerKey(configuration.Name, workerId);
            CurrentPath = Path.Combine(configuration.Path, workerId.ToString());
            Style = WorkerStyle.Default;
            Loaded = false;

            _pendingQueue = new ConcurrentQueue<PondFile>();
        }


        /// <summary>
        /// 加载Worker下的文件信息到Worker当中
        /// </summary>
        public void Load()
        {
            if (Loaded)
            {
                _logger.LogWarning("Worker '{0}' 已经被加载,将不会重复加载.", Key);
                return;
            }

            var files = Directory.GetFiles(CurrentPath);

            try
            {

                foreach (var file in files)
                {
                    var pondFile = new PondFile(Configuration.Name, _workerId, file);
                    _pendingQueue.Enqueue(pondFile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker '{0}' 加载文件信息出错, {1}.", Key, ex.Message);
            }
        }



    }
}
