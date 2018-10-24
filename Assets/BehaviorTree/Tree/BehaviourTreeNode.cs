using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    /// <summary>
    /// 树节点基类
    /// </summary>
    public abstract class BehaviourTreeNode : BehaviourPoolNode
    {
        /// <summary>
        /// 父亲节点
        /// </summary>
        protected BehaviourTreeNode m_parent;

        /// <summary>
        /// 孩子节点列表
        /// </summary>
        protected List<BehaviourTreeNode> m_childs;

        /// <summary>
        /// 最小的孩子节点数
        /// </summary>
        public int iMinChildCount = -1;

        /// <summary>
        /// 最大的孩子节点数
        /// </summary>
        public int iMaxChildCount = -1;

        /// <summary>
        /// 当前节点归属的树
        /// </summary>
        protected BehaviourTree m_tree;

        /// <summary>
        /// 状态
        /// </summary>
        protected EBTStatus m_nowStatus = EBTStatus.Invalid;

        /// <summary>
        /// 状态接口
        /// </summary>
        public EBTStatus Status { 
            get { return m_nowStatus; }
            set { m_nowStatus = value; }
        }

        /// <summary>
        /// 子节点总数
        /// </summary>
        public int ChildCount
        {
            get
            {
                if (this.m_childs == null) return 0;
                return this.m_childs.Count;
            }
        }

        /// <summary>
        /// 绑定一棵树
        /// </summary>
        /// <param name="tree"></param>
        public void BindTree(BehaviourTree tree)
        {
            m_tree = tree;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="childStatus"></param>
        /// <returns></returns>
        protected internal abstract EBTStatus Tick(Agent agent, EBTStatus childStatus);

        /// <summary>
        /// 判断函数，用于条件判断
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public virtual bool Evaluate(Agent agent) { return false; }

        /// <summary>
        /// 当前节点是否是管理节点
        /// 用于装饰、组合节点
        /// </summary>
        public virtual bool CheckChildManager()
        {
            return false;
        }

        #region Parent
        /// <summary>
        /// 设置父类节点
        /// </summary>
        /// <param name="parent"></param>
        protected internal void SetParent(BehaviourTreeNode parent) { m_parent = parent; }

        /// <summary>
        /// 获得父类节点
        /// </summary>
        /// <returns></returns>
        protected internal BehaviourTreeNode GetParent() { return m_parent; }
        #endregion

        #region Child
        /// <summary>
        /// 添加一个孩子节点
        /// </summary>
        /// <param name="child"></param>
        protected internal virtual bool AddChild(BehaviourTreeNode child)
        {
            if (child == null) return false;
            if (this.iMaxChildCount == 0 ||
                (this.m_childs != null && this.m_childs.Count == this.iMaxChildCount))
            {
                BTG.Error("add child failed, the child out limit child count.");
                return false;
            }

            if (this.m_childs != null && this.m_childs.Contains(child) == true)
            {
                BTG.Error("add child failed, the child exsists.");
                return false;
            }
            if (child.GetParent() != null)
            {
                BTG.Error("add child failed, the child have parent.");
                return false;
            }
            child.SetParent(this);
            if (this.m_childs == null) this.m_childs = new List<BehaviourTreeNode>();
            this.m_childs.Add(child);

            return true;
        }

        /// <summary>
        /// 获得孩子节点数量
        /// </summary>
        /// <returns></returns>
        public int GetChildrenCount()
        {
            if (m_childs != null) return m_childs.Count;
            return 0;
        }

        /// <summary>
        /// 根据索引获得孩子节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BehaviourTreeNode GetChild(int index)
        {
            if (m_childs != null && index < m_childs.Count) return m_childs[index];
            return null;
        }

        /// <summary>
        /// 清理子节点
        /// </summary>
        protected void ClearChild()
        {
            if (m_childs != null)
            {
                int len = m_childs.Count;
                for (int i = 0; i < len; ++i) m_childs[i].ClearChild();
                m_childs = null;
            }
            this.m_parent = null;
        }
        #endregion

        /// <summary>
        /// 当前节点的事件
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="eventName"></param>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        protected internal virtual bool OnEvent(Agent agent, string eventName, object eventParams)
        {
            return true;
        }

        /// <summary>
        /// 来自整棵树的事件
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="eventName"></param>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        protected internal virtual bool OnTreeEvent(Agent agent, string eventName, object eventParams)
        {
            if (this.OnTreeEvent(agent, eventName, eventParams) == false) return false;
            if (m_childs == null) return true;
            int len = m_childs.Count;
            for (int i = 0; i < len; ++i)
            {
                if (m_childs[i] is BehaviourNode)
                {
                    (m_childs[i] as BehaviourNode).OnTreeEvent(agent, eventName, eventParams);
                }
            }
            return true;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <returns></returns>
        protected internal override bool Destory()
        {
            if (base.Destory() == false) return false;
            if (m_childs != null)
            {
                int len = m_childs.Count;
                for (int i = 0; i < len; ++i) m_childs[i].Destory();
                m_childs = null;
            }
            this.m_parent = null;

            return true; 
        }
    }
}