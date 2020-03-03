using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bears : MonoBehaviour
{
    //Stats
    [SerializeField] private GameObject job;
    [SerializeField] private IBear bearColor;
    [SerializeField] private int hp;
    [SerializeField] private int totalHP;
    [SerializeField] private int defense;
    [SerializeField] private int melee;
    [SerializeField] private int ranged;
    [SerializeField]private int movement;

    [SerializeField] private bool selected;

    //Needed for turn system
    [SerializeField] private bool isAlive;

    [Range(1, 5)] [Tooltip(" 1 = Green \n 2 = Black \n 3 = Blue \n 4 = Pink \n 5 = Red")] [SerializeField] private int colour = 1;

    //Ablilities
    private bool support; //Can be activated every turn once
    private bool special; //Only Happens every "N" turns

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public int Melee { get => melee; set => melee = value; }
    public int Ranged { get => ranged; set => ranged = value; }
    public int Movement { get => movement; set => movement = value; }
    public int Defense { get => defense; set => defense = value; }
    public bool Special { get => special; set => special = value; }
    public bool Support { get => support; set => support = value; }
    public bool Selected { get => selected; set => selected = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public void DistributeStats()
    {
        this.hp = this.bearColor.Hp;
        this.ranged = this.bearColor.Ranged;
        this.totalHP = this.bearColor.TotalHP;
        this.melee = this.bearColor.Melee;
        this.movement = this.bearColor.Movement;
        this.defense = this.bearColor.Defense;
    }
    private void Start()
    {        
         GiveColour(colour);
        DistributeStats();
        //this.hp =this.bearColor.Hp;
        //this.ranged = this.bearColor.Ranged;
    }

    //Added this for the turn system
    private void Update()
    {
        if (Hp > 0)
        {
            IsAlive = true;
        }
        else if (Hp <= 0)
        {
            IsAlive = false;
        }
    }

    private void GiveColour(int colour)
    {
        if(colour == 1)
        {
             this.bearColor=GreenBear.instance;
        }
        else if(colour == 2)
        {
            this.bearColor=BlackBear.instance;
        }
        else if (colour == 3)
        {
            this.bearColor=BlueBear.instance;
        }
        else if (colour == 4)
        {
            this.bearColor = PinkBear.instance;
        }
        else if (colour == 5)
        {
            this.bearColor = RedBear.instance;
        }
      

        
    }

    public void MeleeAttack()
    {
        this.bearColor.MeleeAttack();
    }

public void Attack(Tile tileToAttack)
    {
        tileToAttack.GetComponentInChildren<Bears>().hp -= this.Ranged;
    }
    public void Attack(Vector2 tileToAttack)
    {
        TileManager.instance.TileDic[tileToAttack].GetComponentInChildren<Bears>().hp -= this.ranged;
    }



}
