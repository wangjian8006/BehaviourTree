using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 单分支节点
    /// </summary>
    public class SingleBrachNode : BehaviourNode
    {
        protected BehaviourTreeNode m_nextChildNode;

        public SingleBrachNode()
        {
            this.iMaxChildCount = this.iMinChildCount = 1;
        }

        protected override bool OnEnter(Agent agent)
        {
            m_nextChildNode = this.GetChild(0);
            return base.OnEnter(agent);
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            if (m_nextChildNode != null)
            {
                EBTStatus s = this.m_nextChildNode.Tick(agent, childStatus);
                return s;
            }

            return EBTStatus.Failure;
        }
    }
}
