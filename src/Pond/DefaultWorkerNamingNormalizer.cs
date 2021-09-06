namespace Pond
{
    public class DefaultWorkerNamingNormalizer : IWorkerNamingNormalizer
    {

        /// <summary>
        /// 格式化WorkerId存储名称
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        public string NormalizeWorkerName(int workerId)
        {
            return workerId.ToString().PadLeft(8, '0');
        }
    }
}
