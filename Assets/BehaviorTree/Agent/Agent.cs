using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    /// <summary>
    /// 数据代理,blackboard
    /// </summary>
    public class Agent
    {
        public enum DomainType{
            Global,
            Tree,
            Node
        }

        protected BehaviourTree m_tree = null;

        protected Dictionary<string, object> m_globals = new Dictionary<string, object>();

        protected Dictionary<string, object> m_trees = new Dictionary<string, object>();

        protected Dictionary<string, object> m_nodes = new Dictionary<string, object>();

        public void Bind(BehaviourTree tree)
        {
            if (tree != null)
            {
                BTG.Error("Agent repet bind tree.");
                return;
            }
            m_tree = tree;
        }

        public void SetValue(DomainType type, string key, object value)
        {
            if (type == DomainType.Global) m_globals[key] = value;
            else if (type == DomainType.Tree) m_trees[key] = value;
            else if (type == DomainType.Node) m_nodes[key] = value;
        }

        public object GetTreeValue(string key)
        {
            return m_trees[key];
        }

        public object GetGlobalValue(string key)
        {
            return m_globals[key];
        }

        public object GetNodeValue(string key)
        {
            return m_nodes[key];
        }

        public virtual void Reset()
        {
            m_globals.Clear();
            m_trees.Clear();
            m_nodes.Clear();
        }

        public bool ContairsTreeKey(string agentKey)
        {
            return this.m_trees.ContainsKey(agentKey);
        }
    }
}
