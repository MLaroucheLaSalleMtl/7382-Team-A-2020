using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBear : MonoBehaviour, IBear
{
    #region UnitNotes
    //All-Rounder Unit
    //Average Everything

    //Support Ability
    //High Damage Attack but loses HP

    //Special Abilities   
    //Makes One teammates special Ability Avaliable Again
    #endregion UnitNotes
    #region Singleton
    public static RedBear instance;
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
    int attackStrength = 30;
    int movement = 3;
    int attackRange = 3;
    Color bearRace = Color.black;
    #endregion BearFields

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Movement { get => movement; set => movement = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }

    public void MeleeAttack()
    {

    }
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability1(Bears Target)
    {
        Target.Hp -= 40;
        this.Hp -= 20;
    }

    public void Ability2(Bears Target)
    {
        Target.Special = true;
    }
}
