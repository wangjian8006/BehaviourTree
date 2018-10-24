using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 等待信号行为
    /// </summary>
    public class WaitForSignal : ActionNode
    {
        private bool m_bTriggered = false;

        protected override bool OnEnter(Agent pAgent)
        {
            this.m_bTriggered = false;
            return true;
        }

        /// <summary>
        /// 等待信号好
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        protected bool CheckIfSignaled(Agent agent)
        {
            return true;
        }

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "triggered") this.m_bTriggered = bool.Parse(propertyValue);
            else base.ParserProperty(propertyName, propertyValue);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            if (childStatus != EBTStatus.Running) return childStatus;

            if (!this.m_bTriggered) this.m_bTriggered = this.CheckIfSignaled(pAgent);
            if (this.m_bTriggered) return base.OnExec(pAgent, childStatus);

            return EBTStatus.Running;
        }
    }
}
