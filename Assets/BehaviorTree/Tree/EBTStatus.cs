using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum EBTStatus
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        Invalid,

        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 失败
        /// </summary>
        Failure,

        /// <summary>
        /// 运行中
        /// </summary>
        Running
    }
}
