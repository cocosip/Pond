namespace Pond
{
    /// <summary>
    /// Worker 类型
    /// </summary>
    public enum WorkerStyle
    {
        /// <summary>
        /// 默认,初始化的时候默认为Default
        /// </summary>
        Default = 1,

        /// <summary>
        /// 只读
        /// </summary>
        ReadOnly = 2,

        /// <summary>
        /// 只写
        /// </summary>
        WriteOnly = 4,

        /// <summary>
        /// 同时读写
        /// </summary>
        ReadWrite = 8
    }
}
