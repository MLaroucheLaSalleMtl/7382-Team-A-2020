using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveBy :Ability
{
   public DriveBy()
    {
        AltRange = false;
        Name = "Drive By";
        CastRange = 4;
        Aoe = 0;
        Alt = false;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        int damage;
        if (!Target.Invincible)
            damage = (int)(attack * 2);
        else
            damage = 0;
        GetComponent<Bears>().DealDamage(damage, Target);
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 2);
        return "Load Up A SPECIAL bullet:\nDamage = " + damage;
    }
}
