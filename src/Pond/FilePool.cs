using Microsoft.Extensions.Logging;

namespace Pond
{
    public class FilePool<TFilePool> : IFilePool<TFilePool> where TFilePool : class
    {

    }

    public class FilePool : IFilePool
    {
        protected ILogger Logger { get; }
        public FilePool(ILogger<FilePool> logger)
        {
            Logger = logger;
        }


        public void Start()
        {

        }


        public void Shutdown()
        {

        }


    }
}
