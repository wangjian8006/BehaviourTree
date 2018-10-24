using System;
using System.Collections.Generic;

public class SpellData
{
    public float cd = 0;

    public float maxDistance = 0;

    public int attack = 0;

    public float duration;

    public int id = 0;

    public float preReleaseTime = -1;

    public static SpellData GetData(int id)
    {
        if (id == 1)
        {
            if (spell1 == null)
            {
                spell1 = new SpellData();
                spell1.id = 1;
                spell1.cd = 0.5f;
                spell1.attack = 10;
                spell1.maxDistance = 5;
                spell1.duration = 0.2f;
            }
            return spell1;
        }
        else if (id == 2)
        {
            if (spell2 == null)
            {
                spell2 = new SpellData();
                spell2.id = 2;
                spell2.cd = 3;
                spell2.attack = 50;
                spell2.maxDistance = 10;
                spell2.duration = 1;
            }
            return spell2;
        }
        else if (id == 3)
        {
            if (spell3 == null)
            {
                spell3 = new SpellData();
                spell3.id = 3;
                spell3.cd = 3;
                spell3.duration = -1f;
            }
            return spell3;
        }

        return null;
    }

    public static SpellData spell1;

    public static SpellData spell2;

    public static SpellData spell3;
}