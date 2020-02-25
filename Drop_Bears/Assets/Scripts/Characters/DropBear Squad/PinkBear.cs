﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBear : MonoBehaviour, IBear
{
    #region UnitNotes
    //Support Unit (Healer)
    //High HP, 
    //Average Movement, Average Attack

    //Support Ability
    //Individual Heal (Itself or Teammates)

    //Special Abilities
    //Revive (Can Revive One Teammate per Battle)
    #endregion UnitNotes
    #region Singleton
    public static PinkBear instance;
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
    int defense = 3;
    int attackStrength = 25;
    int movement = 3;
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
        //Heals Target
        Target.Hp += 40;
    }

    public void Ability2(Bears Target)
    {
        //Revives Fallen Target
        if(Target.IsAlive == false)
        {
            Target.Hp = TotalHP;
        }
    }
}
