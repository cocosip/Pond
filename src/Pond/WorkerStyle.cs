namespace Pond
{
    /// <summary>
    /// Worker 类型
    /// </summary>
    public enum WorkerStyle
    {
        /// <summary>
        /// 默认类型
        /// </summary>
        Default = 1,

        /// <summary>
        /// 只读类型
        /// </summary>
        ReadOnly = 2,

        /// <summary>
        /// 只写类型
        /// </summary>
        WriteOnly = 4,

        /// <summary>
        /// 混合类型,同时可读,可写
        /// </summary>
        Mixed = 8
    }
}
