using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBear : MonoBehaviour, IBear
{
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
    int defense=7;
    int melee=150;
    int ranged=50;
    int movement;
    #endregion BearFields
     
    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Melee { get => melee; set => melee = value; }
    public int Ranged { get => ranged; set => ranged = value; }
    public int Movement { get => movement; set => movement = value; }
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
       
        //this.Movement = 
        //Special = 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
