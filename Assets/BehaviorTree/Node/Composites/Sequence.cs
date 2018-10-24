using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 顺序节点
    /// </summary>
    public class Sequence : CompositeNode
    {

        protected override bool OnEnter(Agent agent)
        {
            this.m_activeChildIndex = 0;
            return base.OnEnter(agent);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            EBTStatus s = childStatus;

            for (; ; )
            {
                if (s == EBTStatus.Running)
                {
                    if (this.CheckIfInterrupted(pAgent))
                    {
                        return EBTStatus.Failure;
                    }

                    s = this.m_childs[m_activeChildIndex].Tick(pAgent, EBTStatus.Running);
                }

                if (s != EBTStatus.Success) return s;

                ++m_activeChildIndex;

                if (m_activeChildIndex >= this.m_childs.Count)
                {
                    return EBTStatus.Success;
                }

                s = EBTStatus.Running;
            }
        }
    }
}
