using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackBear : MonoBehaviour, IBear
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
    #region Singleton
    public static BlackBear instance;
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
    int hp = 100;
    int totalHp = 100;
    int defense = 7;
    int attackStrength = 40;
    int movement = 3;
    int attackRange = 3;
    Color bearRace = Color.black;
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
    

    // Start is called before the first frame update

    public void Ability1(Bears Target)
    {
        //Increases Attack Damage of Target
        Target.AttackStrength *= (int)1.5;
    }

    public void Ability2(Bears Target)
    {
        //High Damage Melee Ability
        Target.Hp -= 60;
    }
}
