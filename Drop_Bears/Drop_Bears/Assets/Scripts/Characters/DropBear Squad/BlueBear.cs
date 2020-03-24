using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBear : BearColor
{
    #region UnitNotes
    //Speed Unit
    //High Movement Ability, High Attack
    //Average HP, Average Defence

    //Support Abilities
    //Increases Movement Ability (Teammates)

    //Special Ability
    //High Damaged Ranged Attack
    #endregion UnitNotes

    #region BearFields
    public BlueBear()
    {
        Hp = 100;
        TotalHp = 100;
        Defense = 7;
        AttackStrength = 40;
        Movement = 5;
        AttackRange = 5;
        BearRace = Color.blue;
        Countdown = 2;
        FirstAbility = new GottaGoQuick();
        Special = new PowerStrike();
    }
    #endregion BearFields

    public override string GetAttackName()
    {
        return "Blue Balls";
    }
    public override string GetAttackDesc(int attack)
    {
        return "Give em the balls:\nDamage = "+attack.ToString();
    }
    public override string GetAbility1Name()
    {
        return "Gotta Go Quick";
    }
    public override string GetAbility1Desc(int attack)
    {
        return "Give an ally +2\nMovement (One Turn)";
    }
    public override string GetAbility2Name()
    {
        return "Power Strike";
    }
    public override string GetAbility2Desc(int attack)
    {
        int damage = (int)(attack * 1.5);
        return "It's stronger: \nDamage = "+damage;
    }


    public override void Ability1(Bears Target, int attack)
    {
        //Increases the Movement of A Target
        Target.Movement += 2;
        Target.themBuffs["buffMovement"] = 2;
    }

    public override void Ability2(Bears Target,int attack)
    {
        //High Damage Range Attack
        Target.Hp -= (int)(attack* 1.5);
    }
}
