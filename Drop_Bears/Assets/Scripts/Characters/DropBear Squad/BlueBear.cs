using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBear : MonoBehaviour, IBear
{
    #region Singleton
    public static BlueBear instance;
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
    int hp;
    int totalHp;
    int defense;
    int melee;
    int ranged;
    int movement;
    #endregion BearFields

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Melee { get => melee; set => melee = value; }
    public int Ranged { get => ranged; set => ranged = value; }
    public int Movement { get => movement; set => movement = value; }

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
        Hp = 100;
        TotalHP = 100;
        Defense = 7;
        Melee = 50;
        Ranged = 150;
        //Movement Should be the higest of the squad
        //this.Movement = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
