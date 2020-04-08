using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefUp : Ability
{
    public BeefUp()
    {
        Name = "Beef Up";
        Id = 3;
        CastRange = 3;
        Aoe = 0;
        Alt = false;
    }
    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        
        Bears Target = tileToCastOn.GetComponentInChildren<Bears>();       
        Target.AttackStrength = (int)(1.5 * Target.AttackStrength);
        Target.themBuffs["buffAttack"] = 2;
       
    }

   public override string GetAbilityDesc(int damage)
    {
        return "Eat Your Beans: \nAttack Up x1.5 (Two Turns)";
    }
}
