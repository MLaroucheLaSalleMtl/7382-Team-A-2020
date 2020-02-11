using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBear : Bears
{
    //Support Unit (Healer)
    //High HP, 
    //Average Movement, Average Ranged
    //Low Attack, Low Defence

    //Support Ability
    //Individual Heal (Itself or Teammates)

    //Special Abilities
    //Area Heal (Heals All Teammates)
    //Revive (Can Revive One Teammate per Battle)

    void Start()
    {
        this.Hp = 150;
        this.TotalHP = 150;
        this.Defense = 5;
        this.Melee = 50;
        this.Ranged = 100;
        //this.Movement = 
        //Special = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
