using System;
using System.Collections.Generic;
using System.Text;

namespace Pond
{
    [Serializable]
    public class FilePoolConfiguration
    {
        /// <summary>
        /// 文件池的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件池的存储路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 每个Worker下存储的最大的文件数量,当超过该值的时候,将不会进行写入。默认:5000个
        /// </summary>
        public int WorkerMaxFile { get; set; } = 5000;

        /// <summary>
        /// 是否开启自动归还文件的操作,当开启之后,如果没有主动调用释放文件的方法,在超过指定的时间后<see cref="AutoReturnSeconds"/>,将会自动归还(重新放到队列中)。 默认:false
        /// </summary>
        public bool EnableAutoReturn { get; set; } = false;

        /// <summary>
        /// 自动归还的时间,以秒为单位。默认:300秒
        /// </summary>
        public int AutoReturnSeconds { get; set; } = 300;

        /// <summary>
        /// 扫描过期的文件的时间,以毫秒为单位。默认3000毫秒
        /// </summary>
        public int ScanReturnFileMillSeconds { get; set; } = 3000;


    }
}
