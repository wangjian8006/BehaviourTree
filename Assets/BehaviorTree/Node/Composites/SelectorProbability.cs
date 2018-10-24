using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 概率选择节点，每个节点有不同的概率
    /// 子节点必须是权重装饰节点
    /// </summary>
    public class SelectorProbability : CompositeNode
    {
        /// <summary>
        /// 子节点的权重
        /// </summary>
        protected List<int> m_weightingMap = new List<int>();

        /// <summary>
        /// 总权重
        /// </summary>
        protected int m_totalSum = 0;

        protected internal override bool AddChild(BehaviourTreeNode node)
        {
            DecoratorWeight pDW = (DecoratorWeight)(node);
            if (pDW != null) return base.AddChild(node);
            else return false;
        }

        protected override bool OnEnter(Agent pAgent)
        {
            this.m_activeChildIndex = -1;
            this.m_weightingMap.Clear();
            this.m_totalSum = 0;

            for (int i = 0; i < this.m_childs.Count; ++i)
            {
                DecoratorWeight child = this.m_childs[i] as DecoratorWeight;
                int weight = child.GetWeight(pAgent);
                this.m_weightingMap.Add(weight);
                this.m_totalSum += weight;
            }

            return true;
        }

        protected override void OnLeave(Agent pAgent)
        {
            this.m_activeChildIndex = -1;
            base.OnLeave(pAgent);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            if (childStatus != EBTStatus.Running) return childStatus;

            //如果已经选择了子节点
            if (this.m_activeChildIndex != -1)
            {
                DecoratorWeight pNode = this.m_childs[this.m_activeChildIndex] as DecoratorWeight;
                EBTStatus status = pNode.Tick(pAgent, EBTStatus.Running);
                return status;
            }

            ///如果没有选择,则选择一个
            float chosen = this.m_totalSum * RandomGenerator.Instance.GetRandom();

            float sum = 0;
            for (int i = 0; i < this.m_childs.Count; ++i)
            {
                int w = this.m_weightingMap[i];

                sum += w;

                if (w > 0 && sum >= chosen)
                {
                    EBTStatus status = this.m_childs[i].Tick(pAgent, EBTStatus.Running);

                    if (status == EBTStatus.Running) this.m_activeChildIndex = i;
                    else this.m_activeChildIndex = -1;

                    return status;
                }
            }

            return EBTStatus.Failure;
        }
    }
}
