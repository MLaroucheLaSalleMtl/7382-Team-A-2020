using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{

    public Heal()
    {
        Id = 5;
        Name = "Heal";
        CastRange = 3;
        Aoe = 0;
        Alt = false;
    }
    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        int healamount = (int)(attack * 1.334);
        //Heals Target
        if (Target.IsAlive)
        {
            Target.Hp += healamount;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        int number = (int)(damage * 1.334);
        return "Well it heals: \nAlly/Own HP+ " + number.ToString();
    }
}
