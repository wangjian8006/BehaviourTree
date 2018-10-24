using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 选择节点
    /// 该节点以给定的顺序依次调用其子节点
    /// 直到其中一个成功返回
    /// 那么该节点也返回成功
    /// 如果所有的子节点都失败，那么该节点也失败
    /// 类似于Or
    /// </summary>
    public class Selector : CompositeNode
    {
        protected override bool OnEnter(Agent pAgent)
        {
            this.m_activeChildIndex = 0;
            return true;
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

                    s = m_childs[m_activeChildIndex].Tick(pAgent, EBTStatus.Running);
                }

                if (s != EBTStatus.Failure) return s;

                ++m_activeChildIndex;

                if (m_activeChildIndex >= m_childs.Count) return EBTStatus.Failure;
                s = EBTStatus.Running;
            }
        }
    }
}
