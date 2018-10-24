using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 描述树的入口节点
    /// </summary>
    public class EntryNode : SingleBrachNode
    {
        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            if (childStatus != EBTStatus.Running)
            {
                return childStatus;
            }

            EBTStatus s = base.OnExec(agent, childStatus);
            return s;
        }
    }
}
