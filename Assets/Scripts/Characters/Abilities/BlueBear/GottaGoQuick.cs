using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoQuick : Ability
{
 public GottaGoQuick()
    {
        AltRange = false;
        Name = "Gotta Go Quick";
        CastRange = 3;
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
        Target.Movement += 2;
        Target.themBuffs["buffMovement"] = 2;
        
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Give an ally +2\nMovement (One Turn)";
    }
}
