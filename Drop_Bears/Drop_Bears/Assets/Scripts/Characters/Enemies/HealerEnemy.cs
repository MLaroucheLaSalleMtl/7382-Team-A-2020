using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy : MonoBehaviour,IBear
{
    // Start is called before the first frame update
    #region UnitNotes
    //basic melee Enemy for game
    #endregion UnitNotes
    #region Stats
    int hp = 70;
    int totalHp = 70;
    int defense = 6;
    int attackStrength = 10;
    int movement = 4;
    int attackRange = 2;
    Color bearRace = Color.white;
    int countdown = 2;
    #endregion Stats

    public static HealerEnemy instance = null;

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

    }

    public void Ability2(Bears Target)
    {

    }
}
