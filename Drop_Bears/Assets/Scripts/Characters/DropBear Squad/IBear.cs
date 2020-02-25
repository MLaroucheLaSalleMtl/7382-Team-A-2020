using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBear 
{
    int Hp {get; set;}
    int TotalHP { get; set; }
    int Defense { get; set; }
    int AttackStrength { get; set; }
    int Movement { get; set; }
    int AttackRange { get; set; }
    Color BearRace { get; set; }

    void MeleeAttack();
    void Ability1(Bears Target);
    void Ability2(Bears Target);

}
