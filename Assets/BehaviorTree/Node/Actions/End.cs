using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 行为树返回成功或者失败
    /// </summary>
    public class End : ActionNode
    {
        protected EBTStatus m_endStatus = EBTStatus.Success;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "endstatus")
            {
                if (propertyValue == "success") m_endStatus = EBTStatus.Success;
                else if (propertyValue == "failure") m_endStatus = EBTStatus.Failure;
            }
            else base.ParserProperty(propertyName, propertyValue);
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            this.m_tree.SetEndStatus(this.m_endStatus);
            return EBTStatus.Running;
        }
    }
}
