using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggernaut :Ability
{
  public Juggernaut()
    {
        AltRange = false;
        Name = "Juggernaut";
        CastRange = 2;
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
        Target.Invincible = true;
         Target.themBuffs["invincible"] = 2;
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Im the juggernaut bitch\nBecome Immune (one turn) ";
    }
}
