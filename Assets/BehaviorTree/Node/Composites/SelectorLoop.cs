using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 循环选择节点，类似于switch...case
    /// 配合WithPrecondition使用
    /// </summary>
    public class SelectorLoop : CompositeNode
    {
        protected internal override bool AddChild(BehaviourTreeNode child)
        {
            if (child is WithPrecondition) return base.AddChild(child);
            return false;
        }

        protected override bool OnEnter(Agent pAgent)
        {
            this.m_activeChildIndex = -1;
            return base.OnEnter(pAgent);
        }

        public override bool CheckChildManager()
        {
            return true;
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            int idx = -1;

            if (childStatus != EBTStatus.Running)
            {
                if (childStatus == EBTStatus.Success) return EBTStatus.Success;
                else if (childStatus == EBTStatus.Failure) idx = this.m_activeChildIndex;
            }

            int index = -1;
            for (int i = (idx + 1); i < this.m_childs.Count; ++i)
            {
                BehaviourTreeNode precondition = ((WithPrecondition)this.m_childs[i]).PreconditionNode;

                EBTStatus status = precondition.Tick(pAgent, EBTStatus.Running);

                if (status == EBTStatus.Success)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                for (int i = index; i < this.m_childs.Count; ++i)
                {
                    WithPrecondition child = (WithPrecondition)this.m_childs[i];

                    if (i > index)
                    {
                        EBTStatus status = child.PreconditionNode.Tick(pAgent, EBTStatus.Running);
                        if (status != EBTStatus.Success) continue;
                    }

                    BehaviourTreeNode action = child.ActionNode;
                    EBTStatus s = action.Tick(pAgent, EBTStatus.Running);

                    if (s == EBTStatus.Running)
                    {
                        this.m_activeChildIndex = i;
                        child.Status = EBTStatus.Running;
                    }
                    else
                    {
                        child.Status = s;
                        if (s == EBTStatus.Failure) continue;
                    }
                    return s;
                }
            }

            return EBTStatus.Failure;
        }
    }
}
