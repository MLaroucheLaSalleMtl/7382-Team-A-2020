using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeal :Ability
{
    public BossHeal()
    {
        Id = 14;
        Name = "Heal The bad bears";
        CastRange = 3;
        Aoe = 1;
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        Bears target = tileToCastOn.GetComponentInChildren<Bears>();
        if (target != null)
        {
            target.Hp += (int)(attack * 2);
            if (target.Hp > target.TotalHP)
                target.Hp = target.TotalHP;
            target.AttackStrength *= (int)(1.05);
            target.Defense *= (int)(1.15);
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Heals the big bad and buffs his attack a smigen but that shit permanent";
    }
}
