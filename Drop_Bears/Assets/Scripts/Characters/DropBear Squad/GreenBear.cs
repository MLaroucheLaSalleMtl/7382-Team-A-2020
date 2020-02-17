using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBear : MonoBehaviour, IBear
{
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

    #region UnitNotes
    //Deffensive Unit
    //High Defence, High HP,
    //Average Melee, Average Ranged
    //Low Movement, 

    //Support Ability 
    //Increases Defence (Itself Or Teammates)

    //Special Ability
    //Enrages Enemies (Pulls In All Enemies)
    #endregion UnitNotes

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
