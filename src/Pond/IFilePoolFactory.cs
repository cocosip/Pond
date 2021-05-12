namespace Pond
{
    public interface IFilePoolFactory
    {
        /// <summary>
        /// GetOrCreate filePool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IFilePool GetOrCreate(string name);
    }
}
