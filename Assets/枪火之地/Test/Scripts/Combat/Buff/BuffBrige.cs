using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBrige
{

    public static void DamageTiming(float attackRatio,CombatAbility selfAbility, ref CombatAbility targetAbility)
    {
        targetAbility.currentHp -= selfAbility.attBase * attackRatio;
    }


}
