using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 条件节点
    /// </summary>
    public class ConditionNode : BehaviourNode
    {
        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            bool ret = this.Evaluate(agent);
            return ret ? EBTStatus.Success : EBTStatus.Failure;
        }
    }
}
