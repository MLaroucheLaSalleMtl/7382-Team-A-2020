using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBear : BearColor
{
    #region UnitNotes
    //All-Rounder Unit
    //Average Everything

    //Support Ability
    //High Damage Attack but loses HP

    //Special Abilities   
    //Makes One teammates special Ability Avaliable Again
    #endregion UnitNotes
    #region BearFields
    public RedBear()
    {
        Hp = 100;
        TotalHp = 100;
        Defense = 7;
        AttackStrength = 30;
        Movement = 3;
        AttackRange = 3;
        BearRace = Color.red;
        Countdown = 2;
        FirstAbility = new BodyFire();
        Special = new Energize();
    }
    #endregion BearFields

    public override string GetAttackName()
    {
        return "Play With Matches";
    }
    public override string GetAttackDesc(int attack)
    {
        return "Start a small fire \n" +
            "on your enemies\n" +
            "Damage= " + attack.ToString();
    }
    public override string GetAbility1Name()
    {
        return "Body Fire";
    }
    public override string GetAbility1Desc(int attack)
    {
        int damage = (int)(attack * 1.5);
        return "DAMNIT IT BURNS:\n" +
            "Self damage = 20\n" +
            "Damage = "+damage.ToString();
    }
    public override string GetAbility2Name()
    {
        return "Energize";
    }
    public override string GetAbility2Desc(int attack)
    {
       
        return "Let an ally use there special again";
    }


    public override void Ability1(Bears Target, int attack)
    {
        Target.Hp -= (int)(attack * (1.67));
        GetComponent<Bears>().Hp -= 20;
    }

    public override void Ability2(Bears Target,int attack)
    {
        Target.Special = true;

    }
}