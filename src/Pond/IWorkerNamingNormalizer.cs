namespace Pond
{
    public interface IWorkerNamingNormalizer
    {
        /// <summary>
        /// 格式化WorkerId存储名称
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        string NormalizeWorkerName(int workerId);
    }
}
