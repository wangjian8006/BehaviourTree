using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 如果子节点失败，那么此节点返回成功。
    /// 如果子节点成功，那么此节点返回失败。
    /// 如果子节点返回正在执行，则同样返回正在执行。
    /// </summary>
    public class DecoratorNot : DecoratorNode
    {
        protected override EBTStatus OnExec(Agent agent, EBTStatus status)
        {
            if (status == EBTStatus.Success) return EBTStatus.Failure;
            if (status == EBTStatus.Failure) return EBTStatus.Success;
            return status;
        }
    }
}
