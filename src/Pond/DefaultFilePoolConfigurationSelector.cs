using Microsoft.Extensions.Options;
using Pond.Utilities;

namespace Pond
{
    public class DefaultFilePoolConfigurationSelector : IFilePoolConfigurationSelector
    {
        private readonly PondOptions _options;

        public DefaultFilePoolConfigurationSelector(IOptions<PondOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Get filePool configuration by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FilePoolConfiguration Get(string name)
        {
            Ensure.NotNullOrWhiteSpace(name, nameof(name));
            return _options.FilePools.GetConfiguration(name);
        }
    }
}
