using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStrike : Ability
{

    public PowerStrike()
    {
        Id = 8;
        Name = "Power Strike";
        CastRange = 1;
        Aoe = 0;
    }
    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        //High Damage Range Attack
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        if (Target != null)
        {
            if (Target.Invincible == false)
                Target.Hp -= (int)(attack * 1.75);
        }
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 1.75);
        return "It's stronger: \nDamage = " + damage;
    }
}
