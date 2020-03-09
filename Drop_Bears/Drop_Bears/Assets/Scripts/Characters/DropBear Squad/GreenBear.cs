using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenBear : MonoBehaviour, IBear
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
    #region Singleton
    public static GreenBear instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion Singleton

    #region BearFields
    int hp = 150;
    int totalHp = 150;
    int defense = 10;
    int attackStrength = 30;
    int movement = 2;
    int attackRange = 2;
    Color bearRace = Color.green;
    int countdown = 2;
    #endregion BearFields

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Movement { get => movement; set => movement = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }
    public int CountDown { get => countdown; set => countdown = value; }

    public void MeleeAttack()
    {
        
    }
    

 

    public void Ability1(Bears Target)
    {
        //Increases Defense
        Target.Defense *= 2;
    }

    public void Ability2(Bears Target)
    {
        //Nullifies Damage For 1 Turn 
        Target.Invincible = true;
        
    }
}
