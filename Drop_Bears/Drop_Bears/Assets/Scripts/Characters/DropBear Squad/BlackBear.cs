using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackBear : BearColor
{
    #region UnitNotes
    //Offensive Melee Unit 
    //High Attack
    //Average Movement, Average Defence, Average HP, 

    //Support Ability
    //Increases Melee Damage (Itself or Teammates)

    //Special Ability
    //High Damage Melee Attack
    #endregion UnitNotes

    public BlackBear()
    {
        Hp = 100;
        TotalHp = 100;
        Defense = 7;
        AttackStrength = 40;
        Movement = 3;
        AttackRange = 3;
        BearRace = Color.black;
        Countdown = 2;
        FirstAbility = new BeefUp();
        Special = new DriveBy();
    }


    public override string GetAttackName()
    {
        return "Black Lightning";
    }
    public override string GetAttackDesc(int attack)
    {
        return "As swift as they come: \nDamage = " + attack.ToString();
    }
    public override string GetAbility1Name()
    {
        return "Beef Up";
    }
    public override string GetAbility1Desc(int attack)
    {
        return "Eat Your Beans: \nAttack Up x1.5 (Two Turns)";
    }
    public override string GetAbility2Name()
    {
        return "Drive By";
    }
    public override string GetAbility2Desc(int attack)
    {
        int damage = (int)(attack * 1.5);
        return "Load Up A SPECIAL bullet:\nDamage = " + damage;
    }


    // Start is called before the first frame update

    public override void Ability1(Bears Target,int attack)
    {
        //Increases Attack Damage of Target
        Target.AttackStrength = (int)(1.5*Target.AttackStrength);
        Target.themBuffs["buffAttack"] = 2;
    }

    public override void Ability2(Bears Target,int attack)
    {
        //High Damage Melee Ability
        Target.Hp -= (int)(attack * 1.5);
    }
}
