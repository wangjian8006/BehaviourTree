using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 并行节点
    /// 等待所有子节点运行完毕后退出
    /// 以后可以根据子节点的结果来返回不同的东西
    /// </summary>
    public class Parallel : CompositeNode
    {
        public override bool CheckChildManager()
        {
            return true;
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            EBTStatus resultStatus = EBTStatus.Invalid;
            for (int i = 0; i < this.m_childs.Count; ++i)
            {
                BehaviourTreeNode node = m_childs[i];
                EBTStatus treeStatus = node.Status;
                if (treeStatus == EBTStatus.Running || treeStatus == EBTStatus.Invalid)
                {
                    EBTStatus s = m_childs[i].Tick(agent, childStatus);
                    if (s == EBTStatus.Running) resultStatus = s;
                }
            }

            if (resultStatus != EBTStatus.Running)
            {
                resultStatus = EBTStatus.Success;
                for (int i = 0; i < this.m_childs.Count; ++i)
                {
                    if (m_childs[i].Status == EBTStatus.Failure) resultStatus = EBTStatus.Failure;
                }
            }
            return resultStatus;
        }
    }
}
