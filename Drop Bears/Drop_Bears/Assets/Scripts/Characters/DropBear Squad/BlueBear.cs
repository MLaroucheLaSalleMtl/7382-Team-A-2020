using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBear : Bears
{
    //Speed Unit
    //High Movement Ability, High Ranged Attack
    //Average HP, Average Defence
    //Low Melee Attack

    //Support Abilities
    //Increases Movement Ability (Teammates)
    //Increases Ranged Damage  (Itself or Teammates)

    //Special Ability
    //High Damaged Ranged Attack

    void Start()
    {
        this.Hp = 100;
        this.TotalHP = 100;
        this.Defense = 7;
        this.Melee = 50;
        this.Ranged = 150;
        //Movement Should be the higest of the squad
        //this.Movement = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
