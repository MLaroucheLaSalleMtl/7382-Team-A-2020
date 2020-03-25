using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenBear : BearColor
{
    #region UnitNotes
    //Deffensive Unit
    //High Defence, High HP,
    //Average Attack
    //Low Movement, Low Range 

    //Support Ability 
    //Increases Defence (Itself Or Teammates)

    //Special Ability
    //Nullifies Damage for 1 Turn
    #endregion UnitNotes


    public GreenBear()
    {
        Hp = 150;
        TotalHp = 150;
        Defense = 10;
        AttackStrength = 30;
        Movement = 2;
        AttackRange = 2;
        BearRace = Color.green;
        Countdown = 2;
        FirstAbility = new HunkerDown();
        Special = new Juggernaut();
    }

    public override string GetAttackName()
    {
        return "Skull Smash";
    }
    public override string GetAttackDesc(int attack)
    {
        return "I HATE SKULLS\nDamage = " + attack.ToString();
    }
    public override string GetAbility1Name()
    {
        return "Hunker Down";
    }
    public override string GetAbility1Desc(int attack)
    {
        return "Double Defence (One Turn)";
    }
    public override string GetAbility2Name()
    {
        return "Juggernaut";
    }
    public override string GetAbility2Desc(int attack)
    {
       
        return "Im the juggernaut bitch\nBecome Immune (one turn) ";
    }



    public override void Ability1(Bears Target,int attack)
    {
        //Increases Defense
        Target.Defense *= 2;
        Target.themBuffs["buffDefence"] = 2;
    }

    public override void Ability2(Bears Target,int attack)
    {
        //Nullifies Damage For 1 Turn 
        Target.Invincible = true;
        Target.themBuffs["invincible"] = 2;

    }
}
