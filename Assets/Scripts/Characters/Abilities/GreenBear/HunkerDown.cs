using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkerDown : Ability
{
    public HunkerDown()
    {
        AltRange = false;
        Name = "Hunker Down";
        CastRange = 1;
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
        Target.Defense *= 2;
        Target.themBuffs["buffDefence"] = 2;
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Double Defence (Two Turns)";
    }
}
