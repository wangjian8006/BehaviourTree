using System;
using System.Collections.Generic;
using UnityEngine;
using BTFrame;

public class BossObject : BaseGameObject
{
    public override void Start()
    {
        this.TotalHP = 1000;
        base.Start();
    }

    protected override void InitBehaviourTree()
    {
        string content = Resources.Load<TextAsset>("boss_behaviour").text;
        m_btree = BTG.xmlGenaral.Parser(content);
    }

    public void OnDamage(int hurtValue, BaseGameObject attacker)
    {
        this.HP = this.HP - hurtValue;
        this.m_btree.Agent.SetValue(Agent.DomainType.Tree, "target", attacker);
    }
}
