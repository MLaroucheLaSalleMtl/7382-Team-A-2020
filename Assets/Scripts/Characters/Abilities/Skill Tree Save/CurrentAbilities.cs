using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CurrentAbilities:MonoBehaviour
{
    public static Ability basicAbility;
    public static Ability specialAbility;
}

public class BlueBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public BlueBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new GottaGoQuick();
            specialAbility = new PowerStrike();
        }
    }
}
public class BlackBearAbilities :CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public BlackBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new BeefUp();
            specialAbility = new DriveBy();
        }
    }
}
public class PinkBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public PinkBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new Heal();
            specialAbility = new Resurrect();
        }
    }
}
public class GreenBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public GreenBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new HunkerDown();
            specialAbility = new Juggernaut();
        }
    }
}
public class RedBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public RedBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new BodyFire();
            specialAbility = new Energize();
        }
    }
}


