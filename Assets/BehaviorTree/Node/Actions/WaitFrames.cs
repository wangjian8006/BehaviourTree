using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 等待帧数
    /// </summary>
    public class WaitFrames : ActionNode
    {
        protected int m_start = 0;

        protected int m_frames = 0;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "frames") m_frames = int.Parse(propertyValue);
            else base.ParserProperty(propertyName, propertyValue);
        }

        protected override bool OnEnter(Agent pAgent)
        {
            this.m_start = BTG.FrameSinceStartup;
            return (this.m_frames >= 0);
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            if (BTG.FrameSinceStartup - this.m_start + 1 >= this.m_frames)
            {
                return EBTStatus.Success;
            }

            return EBTStatus.Running;
        }
    }
}
