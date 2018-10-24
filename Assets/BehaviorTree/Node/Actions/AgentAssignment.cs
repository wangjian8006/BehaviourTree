using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class AgentAssignment : ActionNode
    {
        protected string agentKey = "";

        protected string agentValue = "";

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            base.ParserProperty(propertyName, propertyValue);
            if (propertyName == "agentKey") agentKey = propertyValue;
            else if (propertyName == "agentValue") agentValue = propertyValue;
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            agent.SetValue(Agent.DomainType.Tree, agentKey, agentValue);
            return m_resultStatus;
        }
    }
}
