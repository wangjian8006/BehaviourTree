using System;
using System.Collections.Generic;
using BTFrame;
using UnityEngine;

public class BaseGameObject : MonoBehaviour
{
    /// <summary>
    /// 行为树状态
    /// </summary>
    protected EBTStatus m_btStatus = EBTStatus.Invalid;

    /// <summary>
    /// 步长
    /// </summary>
    public float walkLength = 2;

    public float Speed = 30;

    public BehaviourTree m_btree;

    public int TotalHP = 0;

    private int m_HP = 0;

    private string m_animName = "";

    public int HP
    {
        set
        {
            m_btree.Agent.SetValue(Agent.DomainType.Tree, "hp", value);
            m_HP = value;
            if (this.m_HP < 0)
            {
                m_HP = 0;
                this.OnDead();
            }
        }
        get { return m_HP; }
    }

    public virtual void OnDead() { }

    public bool isDead { get { return HP <= 0; } }

    protected virtual void InitBehaviourTree()
    {

    }

    public virtual void Start()
    {
        this.InitBehaviourTree();

        m_btree.Agent.SetValue(Agent.DomainType.Tree, "owner", this);
        this.HP = TotalHP;
    }

    public void SetForward(Vector3 vForward)
    {
        if (vForward.sqrMagnitude >= Mathf.Epsilon)
        {
            vForward.y = 0;
            this.transform.forward = vForward;
        }
    }

    public void PlayAnimation(string name, bool isLoop = true, float speed = 1.0f)
    {
        if (name == m_animName) return;
        //Debug.Log(name);
        m_animName = name;
        Animation anim = this.gameObject.GetComponent<Animation>();
        anim.CrossFade(m_animName, 0);
        if (isLoop == true) anim[m_animName].wrapMode = WrapMode.Loop;
        else anim[m_animName].wrapMode = WrapMode.Once;

        anim[m_animName].speed = speed;
    }



    public virtual void Update()
    {
        //if (this is PlayerObject) return;
        if (m_btStatus == EBTStatus.Success ||
            m_btStatus == EBTStatus.Failure ||
            m_btree == null) return;
        m_btStatus = m_btree.Tick();
    }
}