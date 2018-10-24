using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 设置当前子节点的权重
    /// </summary>
    public class DecoratorWeight : DecoratorNode
    {
        protected int m_weight = 0;

        public int GetWeight(Agent agent)
        {
            return m_weight;
        }

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "weight") m_weight = int.Parse(propertyValue);
        }
    }
}
