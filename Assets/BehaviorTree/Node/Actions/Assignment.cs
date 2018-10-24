using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 赋值
    /// </summary>
    public class Assignment : ActionNode
    {
        protected int m_nodeID = -1;

        protected string m_propertyName = "";

        protected string m_propertyValue = "";

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "name") m_propertyName = propertyValue;
            else if (propertyName == "value") m_propertyValue = propertyValue;
            else if (propertyName == "nodeid") m_nodeID = int.Parse(propertyValue);
            else base.ParserProperty(propertyName, propertyValue);
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            BehaviourNode node = (this.m_tree.Root as BehaviourNode).GetChildByID(this.m_nodeID) as BehaviourNode;
            if (node == null) return base.OnExec(agent, childStatus);
            node.ParserProperty(m_propertyName, m_propertyValue);
            return base.OnExec(agent, childStatus);
        }
    }
}
