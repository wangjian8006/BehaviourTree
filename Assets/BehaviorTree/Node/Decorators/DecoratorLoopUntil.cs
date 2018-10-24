using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 循环次数为止，-1无限循环
    /// 子节点如果返回不是运行，也跳出循环
    /// </summary>
    public class DecoratorLoopUntil : DecoratorNode
    {
        protected int m_loops = 0;

        protected bool isBreak = false;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "break") isBreak = bool.Parse(propertyValue);
            else if (propertyName == "loops") m_loops = int.Parse(propertyValue);
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            if (this.m_loops > 0) this.m_loops--;
            if (this.m_loops == 0) return EBTStatus.Success;

            if (this.isBreak == true)
                if (status == EBTStatus.Success) 
                    return EBTStatus.Success;
            else
                if (status == EBTStatus.Failure)
                    return EBTStatus.Failure;

            return EBTStatus.Running;
        }
    }
}
