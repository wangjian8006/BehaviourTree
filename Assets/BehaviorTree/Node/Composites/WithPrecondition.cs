using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class WithPrecondition : CompositeNode
    {
        public BehaviourTreeNode PreconditionNode
        {
            get { return (this.m_childs)[0]; }
        }

        public BehaviourTreeNode ActionNode
        {
            get { return (this.m_childs)[1]; }
        }

        public WithPrecondition()
        {
            this.iMinChildCount = this.iMaxChildCount = 2;
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            return EBTStatus.Running;
        }
    }
}
