using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using Debug = UnityEngine.Debug;

namespace BTFrame
{
    /// <summary>
    /// 行为树全局对象
    /// </summary>
    public class BTG
    {
        public delegate BehaviourNode CreateNodeFunc();

        public delegate Agent CreateAgent();

        private static Dictionary<Type, CreateNodeFunc> m_regNodeByType = new Dictionary<Type, CreateNodeFunc>();

        private static Dictionary<string, CreateNodeFunc> m_regNodeByName = new Dictionary<string, CreateNodeFunc>();

        private static Dictionary<string, BehaviourTree> m_regTree = new Dictionary<string, BehaviourTree>();

        private static Dictionary<string, CreateAgent> m_regAgents = new Dictionary<string, CreateAgent>();

        private static Dictionary<string, BTBaseMethod> m_regMethods = new Dictionary<string, BTBaseMethod>();

        public static BehaviourXmlGenaral xmlGenaral = new BehaviourXmlGenaral();

        public static BehaviourCodeGenaral codeGenaral = new BehaviourCodeGenaral();

        public static int FrameSinceStartup { get { return Time.frameCount; } }

        public static float NowTime { get { return Time.time; } }

        public static void Error(string error) { Debug.LogError(error); }

        public static void Log(string log) { Debug.Log(log); }

        public static void Assert(bool condition, string log) { Debug.Assert(condition, log); }

        public static bool RegisterTree(BehaviourTree tree)
        {
            if (m_regTree.ContainsKey(tree.TreeName) == true) return false;
            m_regTree.Add(tree.TreeName, tree);
            return true;
        }

        public static bool UnregisterTree(BehaviourTree tree)
        {
            BehaviourTree t = null;
            m_regTree.TryGetValue(tree.TreeName, out t);
            if (t == null ||
                t != tree) return false;
            m_regTree.Remove(tree.TreeName);
            return true;
        }

        public static BehaviourTree GetTree(string treeName)
        {
            BehaviourTree tree = null;
            m_regTree.TryGetValue(treeName, out tree);
            return tree;
        }

        public static bool RegisterNode<T>(string typeName, CreateNodeFunc func) where T : BehaviourNode
        {
            if (m_regNodeByType.ContainsKey(typeof(T)) == true ||
                m_regNodeByName.ContainsKey(typeName) == true) return false;
            m_regNodeByType.Add(typeof(T), func);
            m_regNodeByName.Add(typeName, func);
            return true;
        }

        public static BehaviourNode GetNode<T>()
        {
            Type t = typeof(T);
            CreateNodeFunc func = null;
            m_regNodeByType.TryGetValue(t, out func);
            if (func == null) return null;
            return func();
        }

        public static BehaviourNode GetNode(string typeName)
        {
            CreateNodeFunc func = null;
            m_regNodeByName.TryGetValue(typeName, out func);
            if (func == null) return null;
            return func();
        }

        public static void DestoryNode(BehaviourNode node)
        {

        }

        public static bool RegisterAgent(string agentType, CreateAgent func)
        {
            if (m_regAgents.ContainsKey(agentType) == true) return false;
            m_regAgents.Add(agentType, func);
            return true;
        }

        public static Agent GetAgent(string agentType)
        {
            if (agentType == null) return new Agent();
            CreateAgent func;
            m_regAgents.TryGetValue(agentType, out func);
            if (func == null)
            {
                return new Agent();
            }
            return func();
        }

        public static void DestoryAgent(Agent agent)
        {

        }

        public static void RegisterMethods(string name, BTBaseMethod method)
        {
            if (m_regMethods.ContainsKey(name) == true)
            {
                return;
            }
            m_regMethods.Add(name, method);
        }

        public static BTBaseMethod GetMethods(string name)
        {
            BTBaseMethod method = null;
            m_regMethods.TryGetValue(name, out method);
            return method;
        }
    }
}