﻿using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 随机顺序节点
    /// </summary>
    public class SequenceStochastic : CompositeNode
    {

        protected override bool OnEnter(Agent agent)
        {
            m_activeChildIndex = 0;
            this.RandomChild(agent);
            return base.OnEnter(agent);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            EBTStatus s = childStatus;
            for (; ; )
            {
                if (s == EBTStatus.Running)
                {
                    int childIndex = this.m_set[this.m_activeChildIndex];

                    if (this.CheckIfInterrupted(pAgent))
                    {
                        return EBTStatus.Failure;
                    }

                    s = this.m_childs[childIndex].Tick(pAgent, EBTStatus.Running);
                }

                if (s != EBTStatus.Success)
                {
                    return s;
                }

                ++this.m_activeChildIndex;

                if (this.m_activeChildIndex >= this.m_childs.Count)
                {
                    return EBTStatus.Success;
                }

                s = childStatus;
            }
        }
    }
}
