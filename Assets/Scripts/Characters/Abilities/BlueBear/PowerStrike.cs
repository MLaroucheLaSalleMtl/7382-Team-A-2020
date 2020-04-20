using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStrike : Ability
{

    public PowerStrike()
    {
        AltRange = false;
        Name = "Power Strike";
        CastRange = 1;
        Aoe = 0;
        Alt = false;
        DamageMod = 2f;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
       
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        int damage;
        if (Target != null)
        {
            if (Target.Invincible == false)
                damage = (int)(attack * DamageMod);
            else
                damage = 0;
            GetComponent<Bears>().DealDamage(damage, Target);
        }
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * DamageMod);
        return "It's stronger: \nDamage = " + damage;
    }
}
