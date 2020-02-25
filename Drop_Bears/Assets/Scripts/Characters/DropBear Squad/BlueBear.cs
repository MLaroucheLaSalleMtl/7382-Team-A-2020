using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBear : MonoBehaviour, IBear
{
    #region UnitNotes
    //Speed Unit
    //High Movement Ability, High Attack
    //Average HP, Average Defence

    //Support Abilities
    //Increases Movement Ability (Teammates)

    //Special Ability
    //High Damaged Ranged Attack
    #endregion UnitNotes
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
    int hp = 100;
    int totalHp = 100;
    int defense = 7;
    int attackStrength = 40;
    int movement = 5;
    int attackRange = 5;
    Color bearRace = Color.blue;
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
        //Movement Should be the higest of the squad
        //this.Movement = 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   

    public void Ability1(Bears Target)
    {
        //Increases the Movement of A Target
        Target.Movement += 4;
    }

    public void Ability2(Bears Target)
    {
        //High Damage Range Attack
        Target.Hp -= 50;
    }
}
