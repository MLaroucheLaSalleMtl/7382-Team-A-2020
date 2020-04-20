using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrect : Ability
{

    public Resurrect()
    {
        AltRange = false;
        Name = "Resurrect";
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
        if (!Target.IsAlive)
        {
            Target.Hp = Target.TotalHP;
            Target.onlyOnce = true;
            Target.transform.Rotate(-90f, 0f, 0f);
            SquadSelection.instance.PlayersAlive++;
            Target.Anim.SetBool("isDead", false);
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
