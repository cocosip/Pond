using Microsoft.Extensions.Logging;
using Pond.Utilities;
using System.Collections.Concurrent;

namespace Pond
{
    public class FilePoolFactory : IFilePoolFactory
    {
        private readonly ILogger _logger;
        private readonly IFilePoolConfigurationSelector _configurationSelector;

        private readonly object _sync = new();
        private readonly ConcurrentDictionary<string, IFilePool> _filePools;

        public FilePoolFactory(
            ILogger<FilePoolFactory> logger,
            IFilePoolConfigurationSelector configurationSelector)
        {
            _logger = logger;
            _configurationSelector = configurationSelector;
            _filePools = new ConcurrentDictionary<string, IFilePool>();
        }

        /// <summary>
        /// GetOrCreate filePool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IFilePool GetOrCreate(string name)
        {
            Ensure.NotNullOrWhiteSpace(name, nameof(name));

            if (!_filePools.TryGetValue(name, out IFilePool filePool))
            {
                lock (_sync)
                {
                    if (_filePools.TryGetValue(name, out filePool))
                    {
                        //Create new filePool

                        if (!_filePools.TryAdd(name, filePool))
                        {
                            _logger.LogError("Could not add filePool '{0}' to dict.", name);
                        }
                    }
                }

            }

            Ensure.NotNull<IFilePool>(filePool, "filePool");

            return default;
        }


        //private IFilePool BuildFilePool(string name)
        //{

        //}

    }
}
