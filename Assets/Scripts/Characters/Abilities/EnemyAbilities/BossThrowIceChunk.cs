using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowIceChunk :Ability
{
    // Start is called before the first frame update
    public BossThrowIceChunk()
    {
        Id = 13;
        Name = "Stuns and does damage";
        CastRange = 3;
        Aoe = 1;
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        Bears target = tileToCastOn.GetComponentInChildren<Bears>();
        if (target != null)
        {
            target.Hp -= (int)(attack * 1.25);
            target.themBuffs["stun"] = 1;
                }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Throws big ice you no move 1 turn";
    }

    // Update is called once per frame

}
