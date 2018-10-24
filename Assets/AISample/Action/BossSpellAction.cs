using System;
using System.Collections.Generic;
using BTFrame;
using BTExtends;
using UnityEngine;

public class BossSpellAction : GameAction
{
    protected float duration = 0;

    protected float startTime = 0;

    public override void ParserProperty(string propertyName, string propertyValue)
    {
        base.ParserProperty(propertyName, propertyValue);
        if (propertyName == "duration") duration = float.Parse(propertyValue);
    }

    protected override bool OnEnter(Agent agent)
    {
        base.OnEnter(agent);
        this.startTime = UnityEngine.Time.time;
        object obj = agent.GetTreeValue("target");
        GameObject target = (obj as BaseGameObject).gameObject;
        this.Owner.SetForward(target.transform.position - this.Owner.transform.position);
        return true;
    }

    protected override void OnLeave(Agent agent)
    {
        base.OnLeave(agent);
        agent.SetValue(Agent.DomainType.Tree, "preSpellTime", UnityEngine.Time.time);
    }

    protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
    {
        if (UnityEngine.Time.time - this.startTime > this.duration) return EBTStatus.Success;
        this.Owner.PlayAnimation("attack1");
        return EBTStatus.Running;
    }
}