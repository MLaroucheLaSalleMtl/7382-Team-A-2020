using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBear : Bears
{
    //Deffensive Unit
    //High Defence, High HP,
    //Average Melee, Average Ranged
    //Low Movement, 

    //Support Ability 
    //Increases Defence (Itself Or Teammates)

    //Special Ability
    //Enrages Enemies (Pulls In All Enemies)

    void Start()
    {
        this.Hp = 150;
        this.TotalHP = 150;
        this.Defense = 10;
        this.Melee = 100;
        this.Ranged = 100;
        //this.Movement = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
