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
        Alt = false;
    }
    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
       
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        int damage;
        if (Target != null)
        {
            if (Target.Invincible == false)
                damage = (int)(attack * 1.75);
            else
                damage = 0;
            GetComponent<Bears>().DealDamage(damage, Target);
        }
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 1.75);
        return "It's stronger: \nDamage = " + damage;
    }
}
