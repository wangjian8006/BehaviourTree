using System;
using System.Collections.Generic;
using BTFrame;
using UnityEngine;

/// <summary>
/// 玩家与行为树之间的代理
/// </summary>
public class PlayerAgent : Agent
{
    private static bool m_bIsRegister = false;

    public static bool IsMove(Agent agent)
    {
        return (agent as PlayerAgent).ActionType == ActionType.Move;
    }

    public static bool IsSpell(Agent agent)
    {
        return (agent as PlayerAgent).ActionType == ActionType.Spell;
    }

    public static bool IsDead(Agent agent)
    {
        return (agent as PlayerAgent).ActionType == ActionType.Dead;
    }

    public static bool IsStand(Agent agent)
    {
        return (agent as PlayerAgent).ActionType == ActionType.Stand;
    }

    private ActionType actionType = ActionType.Stand;

    public ActionType ActionType { get { return this.actionType; } }

    public static void RegisterMethod()
    {
        if (m_bIsRegister == true) return;
        m_bIsRegister = true;

        BTG.RegisterMethods("IsStand", new BTEvaluatesMethods(IsStand));
        BTG.RegisterMethods("IsMove", new BTEvaluatesMethods(IsMove));
        BTG.RegisterMethods("IsSpell", new BTEvaluatesMethods(IsSpell));
        BTG.RegisterMethods("IsDead", new BTEvaluatesMethods(IsDead));
    }

    public PlayerAgent()
    {
        RegisterMethod();
    }

    public void OnMove(Vector3 movePos)
    {
        if (actionType == ActionType.Dead || actionType == ActionType.Spell) return;
        actionType = ActionType.Move;
        this.SetValue(DomainType.Tree, "MovePos", movePos);
    }

    public void OnSpell(int idx)
    {
        if (actionType == ActionType.Dead || actionType == ActionType.Spell) return;
        actionType = ActionType.Spell;
        this.SetValue(DomainType.Tree, "SpellID", idx);
    }

    public void OnDead()
    {
        actionType = ActionType.Dead;
    }

    public void OnStand()
    {
        if (actionType == ActionType.Dead) return;
        actionType = ActionType.Stand;
    }

    public bool CheckMove()
    {
        if (actionType == ActionType.Dead ||
            actionType == ActionType.Spell) return false;
        return true;
    }
}