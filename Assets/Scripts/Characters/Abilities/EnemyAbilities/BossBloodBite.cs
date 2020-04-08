using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBloodBite :Ability
{
    public BossBloodBite()
    {
        Id = 12;
        Name = "Blood Bite";
        CastRange = 1;
        Aoe = 1;
    }




    public override string GetAbilityDesc(int damage)
    {
        int number = (int)(damage * 1.75);
        return "BLOOD " + number.ToString();
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        int damage = (int)(attack * (1.75));
        Bears hostbear = GetComponent<Bears>();
        hostbear.Hp += damage;
        if(hostbear.Hp>hostbear.TotalHP)
        {
            hostbear.Hp = hostbear.TotalHP;
        }
        Bears target = tileToCastOn.GetComponentInChildren<Bears>();

        if(!target.Invincible)
        {
            hostbear.DealDamage(damage, target);
        }
        else
            hostbear.DealDamage(0, target);
    }
}
