﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBear : MonoBehaviour, IBear
{
    #region UnitNotes
    //Deffensive Unit
    //High Defence, High HP,
    //Average Attack
    //Low Movement, 

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
    int defense = 40;
    int attackStrength = 25;
    int movement = 2;
    int attackRange = 4;
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
        //Increases Defense
        Target.Defense += 10;
    }

    public void Ability2(Bears Target)
    {
        //Nullifies Damage For 1 Turn 
        
    }
}
