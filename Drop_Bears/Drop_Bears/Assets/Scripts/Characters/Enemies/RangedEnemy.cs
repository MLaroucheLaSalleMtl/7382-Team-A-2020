﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BearColor
{
    // Start is called before the first frame update
    #region UnitNotes
    //basic melee Enemy for game
    #endregion UnitNotes
    #region Stats
    public RangedEnemy()
    {
        Hp = 70;
        TotalHp = 70;
        Defense = 3;
        AttackStrength = 35;
        Movement = 4;
        AttackRange = 3;
        BearRace = Color.white;
        Countdown = 2;
    }

    public override void Ability1(Bears target, int attack)
    {
        throw new System.NotImplementedException();
    }

    public override void Ability2(Bears target, int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAbility1Desc(int attack)
    {
        throw new System.NotImplementedException();
    }

    #endregion Stats





    public override string GetAbility1Name()
    {
        throw new System.NotImplementedException();
    }

    public override string GetAbility2Desc(int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAbility2Name()
    {
        throw new System.NotImplementedException();
    }

    public override string GetAttackDesc(int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAttackName()
    {
        throw new System.NotImplementedException();
    }
}
