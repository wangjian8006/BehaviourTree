using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 组合节点
    /// </summary>
    public class CompositeNode : BehaviourNode
    {
        /// <summary>
        /// 当前选择子节点的索引
        /// </summary>
        protected int m_activeChildIndex = 0;

        protected List<int> m_set;

        public CompositeNode()
        {
            this.iMinChildCount = 2;
        }

        /// <summary>
        /// 自定义继承，判定条件
        /// </summary>
        /// <param name="pAgent"></param>
        /// <returns></returns>
        protected virtual bool CheckIfInterrupted(Agent pAgent)
        {
            return false;
        }

        /// <summary>
        /// 将子节点随机，打散
        /// </summary>
        /// <param name="pAgent"></param>
        protected void RandomChild(Agent pAgent)
        {
            if (m_set == null) m_set = new List<int>();

            int n = this.m_childs.Count;

            if (this.m_set.Count != n)
            {
                this.m_set.Clear();

                for (int i = 0; i < n; ++i)
                {
                    this.m_set.Add(i);
                }
            }

            for (int i = 0; i < n; ++i)
            {
                int index1 = (int)(n * RandomGenerator.Instance.GetRandom());
                int index2 = (int)(n * RandomGenerator.Instance.GetRandom());

                if (index1 != index2)
                {
                    int old = this.m_set[index1];
                    this.m_set[index1] = this.m_set[index2];
                    this.m_set[index2] = old;
                }
            }
        }
    }
}
