using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour,IBear
{
    // Start is called before the first frame update
    #region UnitNotes
    //basic melee Enemy for game
    #endregion UnitNotes
    #region Stats
    int hp = 70;
    int totalHp = 70;
    int defense = 3;
    int attackStrength = 35;
    int movement = 4;
    int attackRange = 3;
    Color bearRace = Color.white;
    int countdown = 2;
    #endregion Stats

    public static RangedEnemy instance = null;

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

    public void MeleeAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Ability1(Bears Target)
    {
        throw new System.NotImplementedException();
    }

    public void Ability2(Bears Target)
    {
        throw new System.NotImplementedException();
    }

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Movement { get => movement; set => movement = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }
    public int CountDown { get => countdown; set => countdown = value; }
}
