using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class Or : ConditionNode
    {
        public override bool Evaluate(Agent pAgent)
        {
            bool ret = true;
            for (int i = 0; i < this.m_childs.Count; ++i)
            {
                BehaviourTreeNode c = this.m_childs[i];
                ret = c.Evaluate(pAgent);
                if (ret) break;
            }

            return ret;
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            for (int i = 0; i < this.m_childs.Count; ++i)
            {
                EBTStatus s = this.m_childs[i].Tick(pAgent, EBTStatus.Running);
                if (s == EBTStatus.Success) return s;
            }
            return EBTStatus.Failure;
        }
    }
}
