using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoQuick : Ability
{
 public GottaGoQuick()
    {
        Id = 7;
        Name = "Gotta Go Quick";
        CastRange = 3;
        Aoe = 0;
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        Target.Movement += 2;
        Target.themBuffs["buffMovement"] = 2;
        
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Give an ally +2\nMovement (One Turn)";
    }
}
