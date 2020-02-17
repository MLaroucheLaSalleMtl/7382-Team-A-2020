using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBear : MonoBehaviour, IBear
{
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
