using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energize : Ability
{
    public Energize()
    {
        Id = 2;
        Name = "Energize";
        CastRange = 3;
        Aoe = 0;
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        Target.Special = true;
    }

    public override string GetAbilityDesc(int damage)
    {

        return "Let an ally use there special again";
    }
}
