using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class IfElse : CompositeNode
    {
        public IfElse()
        {
            this.iMinChildCount = this.iMaxChildCount = 3;
        }

        protected override bool OnEnter(Agent pAgent)
        {
            m_activeChildIndex = -1;
            return base.OnEnter(pAgent);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            EBTStatus conditionResult = EBTStatus.Invalid;

            if (childStatus == EBTStatus.Success || 
                childStatus == EBTStatus.Failure)
            {
                conditionResult = childStatus;
            }

            if (this.m_activeChildIndex == -1)
            {
                //检测判断条件
                BehaviourTreeNode pCondition = this.m_childs[0];
                if (conditionResult == EBTStatus.Invalid)
                    conditionResult = pCondition.Tick(pAgent, EBTStatus.Running);

                if (conditionResult == EBTStatus.Success) this.m_activeChildIndex = 1;
                else if (conditionResult == EBTStatus.Failure) this.m_activeChildIndex = 2;
            }
            else
            {
                return childStatus;
            }

            if (this.m_activeChildIndex != -1)
            {
                //执行if或者else
                BehaviourTreeNode pBehavior = this.m_childs[this.m_activeChildIndex];
                EBTStatus s = pBehavior.Tick(pAgent, EBTStatus.Running);
                return s;
            }

            return EBTStatus.Running;
        }
    }
}
