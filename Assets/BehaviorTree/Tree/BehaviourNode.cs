using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    public class BehaviourNode : BehaviourTreeNode
    {
        /// <summary>
        /// 名字
        /// </summary>
        protected string m_sName = "";

        /// <summary>
        /// ID
        /// </summary>
        protected int m_ID = -1;

        /// <summary>
        /// 名字
        /// </summary>
        protected internal string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        /// <summary>
        /// ID
        /// </summary>
        protected internal int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        /// <summary>
        /// 解析节点属性
        /// </summary>
        /// <param name="propertyName">属性名字</param>
        /// <param name="propertyValue">属性值 </param>
        public virtual void ParserProperty(string propertyName, string propertyValue)
        {

        }

        /// <summary>
        /// 根据名字获得孩子节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BehaviourTreeNode GetChildByName(string name)
        {
            if (m_childs == null) return null;
            int len = m_childs.Count;
            for (int i = 0; i < len; ++i)
            {
                if (m_childs[i] is BehaviourNode)
                {
                    if ((m_childs[i] as BehaviourNode).Name == name) return m_childs[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 根据ID获得孩子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BehaviourTreeNode GetChildByID(int id)
        {
            if (m_childs == null) return null;
            int len = m_childs.Count;
            for (int i = 0; i < len; ++i)
            {
                if (m_childs[i] is BehaviourNode)
                {
                    if ((m_childs[i] as BehaviourNode).ID == id) return m_childs[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 进入节点
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public bool Enter(Agent agent) 
        {
            //BTG.Log("Enter:" + this.ID);
            //这里可以加附带前置条件
            return this.OnEnter(agent);
        }

        /// <summary>
        /// 离开节点
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public void Leave(Agent agent)
        {
            //这里可以加附带的后置条件
            //BTG.Log("Leave:" + this.ID);
            this.OnLeave(agent);
            this.Status = EBTStatus.Invalid;
        }

        protected BehaviourTreeNode GetTopManagerNode()
        {
            BehaviourTreeNode node = this;
            BehaviourTreeNode parentNode = this.GetParent();
            while (parentNode != null)
            {
                if (parentNode.CheckChildManager() == true) node = parentNode;
                else break;
                parentNode = node.GetParent();
            }
            return node;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="childStatus"></param>
        /// <returns></returns>
        protected internal override EBTStatus Tick(Agent agent, EBTStatus childStatus) 
        {
            //BTG.Log("Tick:" + this.ID);
            if (this.Status == EBTStatus.Invalid)
            {
                if (this.Enter(agent) == false)
                {
                    return EBTStatus.Failure;
                }
            }

            EBTStatus s = this.OnExec(agent, childStatus);        //深入
            this.Status = s;

            if (s == EBTStatus.Running)
            {
                BehaviourTreeNode mrgNode = GetTopManagerNode();
                this.m_tree.SetNowNode(mrgNode);                       //每次不从根节点开始运行
            }
            else
            {
                this.Leave(agent);                                  //离开
                /*BehaviourTreeNode parent = this.GetParent();
                if (parent != null) return parent.Tick(agent, this.Status);*/
            }

            return s;
        }

        /// <summary>
        /// 执行，用于继承逻辑实现
        /// </summary>
        /// <returns></returns>
        protected virtual EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            return EBTStatus.Success;
        }

        /// <summary>
        /// 响应进入节点,用于继承逻辑实现
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="childStatus"></param>
        protected virtual bool OnEnter(Agent agent)
        {
            return true;
        }

        /// <summary>
        /// 响应节点离开，用于集成逻辑实现
        /// </summary>
        /// <param name="agent"></param>
        protected virtual void OnLeave(Agent agent)
        {

        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <returns></returns>
        protected internal override bool Destory()
        {
            if (base.Destory() == false) return false;
            BTG.DestoryNode(this);
            m_sName = "";
            m_ID = -1;
            return true;
        }
    }
}