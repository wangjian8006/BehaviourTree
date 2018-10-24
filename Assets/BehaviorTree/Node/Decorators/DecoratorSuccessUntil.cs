using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 返回成功直到节点在指定的次数到达前返回成功
    /// 指定的次数到达后返回失败。
    /// 如果指定的次数小于0，则总是返回成功
    /// </summary>
    public class DecoratorSuccessUntil : DecoratorNode
    {
        protected int m_number = 0;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "number") m_number = int.Parse(propertyValue);
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            if (this.m_number > 0)
            {
                this.m_number--;
                if (this.m_number == 0) return EBTStatus.Failure;
                return EBTStatus.Success;
            }

            if (this.m_number == -1) return EBTStatus.Success;
            return EBTStatus.Failure;
        }
    }
}
