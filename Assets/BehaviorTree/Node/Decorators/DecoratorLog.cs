using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 输出一个日志
    /// 子节点是啥状态就返回啥状态
    /// </summary>
    public class DecoratorLog : DecoratorNode
    {

        protected string m_message = "";

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "message") m_message = propertyValue;
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus childStatus)
        {
            BTG.Log(string.Format("Decorator Log :{0}\n", m_message));
            return childStatus;
        }
    }
}
