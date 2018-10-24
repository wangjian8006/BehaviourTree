using System;
using System.Collections.Generic;
using UnityEngine;
using BTFrame;

public class SpellAction : GameAction
{
    protected SpellData spellData;

    protected float startTime = 0;

    protected BaseGameObject target;

    protected override bool OnEnter(BTFrame.Agent agent)
    {
        base.OnEnter(agent);
        int id = (int)(agent as PlayerAgent).GetTreeValue("SpellID");
        spellData = SpellData.GetData(id);

        if (spellData == null || 
            (spellData.preReleaseTime > 0 && 
                Time.time - spellData.preReleaseTime < spellData.cd))
        {
            Debug.LogError("CD未到");
            (agent as PlayerAgent).OnStand();
            return false;
        }

        target = agent.GetTreeValue("Target") as BaseGameObject;

        if (target == null || 
            (spellData.maxDistance > 0 &&
                spellData.maxDistance < Vector3.Distance(target.transform.position, this.Owner.transform.position)))
        {
            Debug.LogError("距离不够");
            (agent as PlayerAgent).OnStand();
            return false;
        }

        startTime = Time.time;
        spellData.preReleaseTime = startTime;
        this.Owner.SetForward(this.target.transform.position - this.Owner.transform.position);
        return true;
    }

    protected override void OnLeave(Agent agent)
    {
        base.OnLeave(agent);
        if (spellData.id == 3)
        {
            Vector3 pos = this.Owner.transform.position;
            pos += this.Owner.transform.forward.normalized * 10;
            this.Owner.transform.position = pos;
        }

        GamePlayer.Instance.OnAttackBoss(spellData.id);

        (agent as PlayerAgent).OnStand();
    }

    protected override BTFrame.EBTStatus OnExec(BTFrame.Agent agent, BTFrame.EBTStatus childStatus)
    {
        if (Time.time - startTime > spellData.duration) return EBTStatus.Success;
        if (spellData.id == 1)
        {
            this.Owner.PlayAnimation("attack1", true, 3);
        }else if (spellData.id == 2)
        {
            this.Owner.PlayAnimation("attack2", false, 1);
        }else if (spellData.id == 3)
        {
            this.Owner.PlayAnimation("run", false, 50);
        }

        return EBTStatus.Running;
    }
}