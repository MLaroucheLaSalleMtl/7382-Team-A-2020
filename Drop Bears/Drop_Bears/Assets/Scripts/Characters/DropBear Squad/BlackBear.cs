using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBear : Bears
{
    //Offensive Unit 
    //High Melee Attack
    //Average Movement, Average Defence, Average HP, 
    //Low Ranged Attack

    //Support Ability
    //Increases Melee Damage (Itself or Teammates)

    //Special Ability
    //High Damage Melee Attack

    // Start is called before the first frame update
    void Start()
    {
        this.Hp = 100;
        this.TotalHP = 100;
        this.Defense = 7;
        this.Melee = 150;
        this.Ranged = 50;
        //this.Movement = 
        //Special = 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
