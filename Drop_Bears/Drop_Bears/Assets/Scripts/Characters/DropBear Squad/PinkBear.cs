using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinkBear : BearColor
{
    #region UnitNotes
    //Support Ranged Unit (Healer)
    //High HP, High Range
    //Average Movement, Average Attack

    //Support Ability
    //Individual Heal (Itself or Teammates)

    //Special Abilities
    //Revive (Can Revive One Teammate per Battle)
    #endregion UnitNotes

    #region BearFields
    public PinkBear()
    {

        Hp = 150;
        TotalHp = 150;
        Defense = 5;
        AttackStrength = 30;
        Movement = 3;
        AttackRange = 4;
        BearRace = Color.magenta;
        Countdown = 2;
        FirstAbility = new Heal();
        Special = new Resurrect();
    }
    #endregion BearFields


    public override string GetAttackName()
    {
        return "Love Beam";
    }
    public override string GetAttackDesc(int attack)
    {
        return "OWWW Love Hurts: \nDamage = " + attack.ToString();
    }
    public override string GetAbility1Name()
    {
        return "Heal";
    }
    public override string GetAbility1Desc(int attack)
    {
        int number = (int)(attack * 1.334);
        return "Well it heals: \nAlly/Own HP+ " + number.ToString();
    }
    public override string GetAbility2Name()
    {
        return "Resurrect";
    }
    public override string GetAbility2Desc(int attack)
    {
        return "Fully Heal \nor revive ally";
    }


    public override void Ability1(Bears Target,int attack)
    {
        int healamount = (int)(attack * 1.334);
        //Heals Target
        if (Target.IsAlive)
        {
            Target.Hp += healamount;
        }
    }

    public override void Ability2(Bears Target, int attack)
    {
        //Revives Fallen Target
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
}
