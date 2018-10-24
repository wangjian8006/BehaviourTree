using System;
using System.Collections.Generic;
using UnityEngine;
using BTFrame;

public class PlayerObject : BaseGameObject
{
    private BaseGameObject m_target;

    protected override void InitBehaviourTree()
    {
        string content = Resources.Load<TextAsset>("player_behaviour").text;
        m_btree = BTG.xmlGenaral.Parser(content);
        Debug.Log("Parser completed.");
        Debug.Log(m_btree);

        SetTarget(this.m_target);
    }

    public void OnMove(Vector3 pos)
    {
        //Debug.Log(pos);
        pos.y = 0;
        (this.m_btree.Agent as PlayerAgent).OnMove(pos);
    }

    public void OnSpell(int idx)
    {
        (this.m_btree.Agent as PlayerAgent).OnSpell(idx);
    }

    public override void OnDead()
    {
        base.OnDead();
        (this.m_btree.Agent as PlayerAgent).OnDead();
    }

    public override void Start()
    {
        base.Start();

        RoleController.TPC m_CameraController = gameObject.AddComponent<RoleController.TPC>();
        m_CameraController.AttachGameObject(gameObject, false);
    }


    public void SetTarget(BaseGameObject target)
    {
        m_target = target;
        if (this.m_btree != null)
        {
            this.m_btree.Agent.SetValue(Agent.DomainType.Tree, "Target", this.m_target);
        }
    }
}
