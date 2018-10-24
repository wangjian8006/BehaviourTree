using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 空操作
    /// </summary>
    public class Noop : ActionNode
    {
        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            return EBTStatus.Success;
        }
    }
}
