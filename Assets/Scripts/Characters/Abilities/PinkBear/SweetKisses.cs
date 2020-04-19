using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetKisses : Ability
{
    public SweetKisses()
    {
        AltRange = false;
        Name = "Sweet Kisses";
        CastRange = 3;
        Aoe = 0;
        Alt = true;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears target = tileTooCastOn.GetComponentInChildren<Bears>();
      
        if (target.IsAlive && target != null)
        {
            
            target.themBuffs["heal"] = 3;
        }
    }

    public override string GetAbilityDesc(int damage)
    {       
        return "Heals 10% of total hp" + "\nfor 3 turns";
    }
}
