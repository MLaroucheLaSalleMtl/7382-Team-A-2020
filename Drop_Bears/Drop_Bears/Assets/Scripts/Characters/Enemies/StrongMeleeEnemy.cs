using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMeleeEnemy : MonoBehaviour,IBear
{
    #region Stats
    int hp = 140;
    int totalHp = 140;
    int defense = 9;
    int attackStrength = 35;
    int movement = 4;
    int attackRange = 1;
    Color bearRace = Color.white;
    int countdown = 2;
    #endregion Stats

    public static StrongMeleeEnemy instance = null;

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

    
    public void Ability1(Bears Target)
    {

    }

    public void Ability2(Bears Target)
    {

    }
    
}
