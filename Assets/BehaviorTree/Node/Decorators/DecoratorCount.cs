using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 计数节点
    /// 当进入这个节点的时候
    /// Count不等于0的时候返回true
    /// Count等于0放回false
    /// </summary>
    public class DecoratorCount : DecoratorNode
    {
        protected int m_count = 0;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "count") m_count = int.Parse(propertyValue);
        }

        protected override bool OnEnter(Agent pAgent)
        {
            base.OnEnter(pAgent);
            if (m_count == 0) return false;
            return true;
        }
    }
}
