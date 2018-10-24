using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 行为节点
    /// </summary>
    public class ActionNode : BehaviourNode
    {
        protected EBTStatus m_resultStatus = EBTStatus.Success;

        public ActionNode()
        {
            this.iMinChildCount = this.iMaxChildCount = 0;
        }

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "ResultStatus")
            {
                if (propertyValue == "Success") m_resultStatus = EBTStatus.Success;
                else if (propertyValue == "Failure") m_resultStatus = EBTStatus.Failure;
                else if (propertyValue == "Running") m_resultStatus = EBTStatus.Running;
                else BTG.Error("Can't found ResultStatus enum value.");
            }
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            return m_resultStatus;
        }
    }
}
