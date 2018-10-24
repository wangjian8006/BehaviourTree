using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 装饰节点
    /// </summary>
    public class DecoratorNode : SingleBrachNode
    {
        /// <summary>
        /// 是否等子节点结束后返回
        /// </summary>
        protected bool m_bDecorateWhenChildEnds = false;

        public override bool CheckChildManager()
        {
            return true;
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            EBTStatus status = EBTStatus.Invalid;

            if (childStatus != EBTStatus.Running)
            {
                status = childStatus;

                if (!this.m_bDecorateWhenChildEnds || status != EBTStatus.Running)
                {
                    EBTStatus result = this.OnDecorator(agent, status);

                    if (result != EBTStatus.Running)
                    {
                        return result;
                    }

                    return EBTStatus.Running;
                }
            }

            status = base.OnExec(agent, childStatus);

            if (!this.m_bDecorateWhenChildEnds || status != EBTStatus.Running)
            {
                EBTStatus result = this.OnDecorator(agent, status);

                return result;
            }

            return EBTStatus.Running;
        }

        /// <summary>
        /// 用于子节点实现装饰逻辑
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="childStatus"></param>
        /// <returns></returns>
        protected virtual EBTStatus OnDecorator(Agent agent, EBTStatus childStatus)
        {
            return EBTStatus.Success;
        }
    }
}