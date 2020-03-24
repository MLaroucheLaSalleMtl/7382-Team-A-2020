using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveBy :Ability
{
   public DriveBy()
    {
        Id = 4;
        Name = "Drive By";
        CastRange = 4;
        Aoe = 0;
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        Target.Hp -= (int)(attack * 1.5);
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 1.5);
        return "Load Up A SPECIAL bullet:\nDamage = " + damage;
    }
}
