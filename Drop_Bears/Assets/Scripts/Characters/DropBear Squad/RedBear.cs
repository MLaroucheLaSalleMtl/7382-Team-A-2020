using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBear : MonoBehaviour, IBear
{
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
    //All-Rounder Unit
    //Average Everything

    //Support Ability
    //

    //Special Abilities
    //Increases Teammates Stats
    //Makes One teammates special Ability Avaliable Again

    void Start()
    {
        this.Hp = 100;
        this.TotalHP = 100;
        this.Defense = 7;
        this.Melee = 100;
        this.Ranged = 100; 
        //this.Movement = 
        //Special =     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
