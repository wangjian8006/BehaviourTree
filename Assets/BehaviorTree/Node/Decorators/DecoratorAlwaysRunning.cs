using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 不管子节点返回啥，只返回运行状态
    /// </summary>
    public class DecoratorAlwaysRunning : DecoratorNode
    {
        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            return EBTStatus.Running;
        }
    }
}
