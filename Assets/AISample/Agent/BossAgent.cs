using System;
using System.Collections.Generic;
using BTFrame;

public class BossAgent : Agent
{
    public static bool BossIsDead(Agent agent)
    {
        return ((agent as BossAgent).GetTreeValue("owner") as BaseGameObject).HP <= 0;
    }

    public static bool BossSpellIsCD(Agent agent)
    {
        if (agent.ContairsTreeKey("preSpellTime") == false) return false;
        return (UnityEngine.Time.time - (float)agent.GetTreeValue("preSpellTime")) < 3;
    }

    public static bool CanAttackDistance(Agent agent)
    {
        float dis = UnityEngine.Vector3.Distance(((agent as BossAgent).GetTreeValue("owner") as BaseGameObject).transform.position,
                                    ((agent as BossAgent).GetTreeValue("target") as BaseGameObject).transform.position);

        if (dis < 15) return true;
        return false;
    }

    private static bool m_bIsRegister = false;

    public static void RegisterMethod()
    {
        if (m_bIsRegister == true) return;
        m_bIsRegister = true;

        BTG.RegisterMethods("BossIsDead", new BTEvaluatesMethods(BossIsDead));
        BTG.RegisterMethods("BossSpellIsCD", new BTEvaluatesMethods(BossSpellIsCD));
        BTG.RegisterMethods("CanAttackDistance", new BTEvaluatesMethods(CanAttackDistance));
    }

    public BossAgent()
    {
        RegisterMethod();
    }
}
