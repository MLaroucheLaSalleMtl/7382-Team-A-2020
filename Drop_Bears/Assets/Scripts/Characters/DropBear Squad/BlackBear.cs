using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBear : MonoBehaviour, IBear
{
    #region UnitNotes
    //Offensive Unit 
    //High Melee Attack
    //Average Movement, Average Defence, Average HP, 

    //Support Ability
    //Increases Melee Damage (Itself or Teammates)

    //Special Ability
    //High Damage Melee Attack
    #endregion UnitNotes
    #region Singleton
    public static BlackBear instance;
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
    int hp =100;
    int totalHp=100;
    int defense=15;
    int attackStrength=50;
    int movement = 5;
    int attackRange = 4;
    Color bearRace = Color.black;
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
    

    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability1(Bears Target)
    {
        //Increases Attack Damage of Target
        Target.AttackStrength += 10;
    }

    public void Ability2(Bears Target)
    {
        //High Damage Melee Ability
        Target.Hp -= 50;
    }
}
