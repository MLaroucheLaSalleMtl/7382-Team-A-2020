using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrect : Ability
{

    public Resurrect()
    {
        Id = 6;
        Name = "Resurrect";
        CastRange = 3;
        Aoe = 0;
    }
    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears Target = tileTooCastOn.GetComponentInChildren<Bears>();
        if (!Target.IsAlive)
        {
            Target.Hp = Target.TotalHP;
            Target.onlyOnce = true;
            Target.transform.Rotate(-90f, 0f, 0f);
            SquadSelection.instance.PlayersAlive++;
        }
        else
        {
            Target.Hp = Target.TotalHP;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Fully Heal \nor revive ally";
    }
}
