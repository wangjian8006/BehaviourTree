using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 返回失败直到节点在指定的次数到达前返回失败
    /// 指定的次数到达后返回成功。
    /// 如果指定的次数小于0，则总是返回失败
    /// </summary>
    public class DecoratorFailureUntil : DecoratorNode
    {
        protected int m_number;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "number") m_number = int.Parse(propertyValue);
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            if (this.m_number > 0)
            {
                this.m_number--;

                if (this.m_number == 0)
                {
                    return EBTStatus.Success;
                }

                return EBTStatus.Failure;
            }

            if (this.m_number == -1)
            {
                return EBTStatus.Failure;
            }

            return EBTStatus.Failure;
        }
    }
}
