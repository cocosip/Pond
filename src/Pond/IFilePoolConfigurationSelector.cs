namespace Pond
{
    public interface IFilePoolConfigurationSelector
    {
        /// <summary>
        /// Get filePool configuration by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        FilePoolConfiguration Get(string name);
    }
}
