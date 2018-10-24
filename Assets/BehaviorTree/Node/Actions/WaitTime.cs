using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 等待时间
    /// </summary>
    public class WaitTime : ActionNode
    {
        protected float m_start = 0;

        protected float m_time = 0;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "time") m_time = float.Parse(propertyValue);
            else base.ParserProperty(propertyName, propertyValue);
        }

        protected override bool OnEnter(Agent pAgent)
        {
            this.m_start = BTG.NowTime;
            return (this.m_time >= 0);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            if (BTG.NowTime - this.m_start >= this.m_time)
            {
                return EBTStatus.Success;
            }
            return EBTStatus.Running;
        }
    }
}