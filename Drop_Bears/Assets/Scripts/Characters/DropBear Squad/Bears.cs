using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bears : MonoBehaviour
{
    //Stats
    private IBear bearColor;
    [Header("Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int totalHP;
    [SerializeField] private int defense;
    [SerializeField] private int attackStrength;
    [SerializeField] private int attackRange;
    [SerializeField] private int movement;
    [SerializeField] private Color bearRace;
    [SerializeField] private bool turnComplete;

    [Space()]

    [SerializeField] private bool selected;

    //Needed for turn system
    [SerializeField] private bool isAlive;

    public enum EnumColour { Green, Black, Blue, Pink, Red, MeleeEnemy };
    [SerializeField] private EnumColour color;
    //[Range(1, 6)] [Tooltip(" 1 = Green \n 2 = Black \n 3 = Blue \n 4 = Pink \n 5 = Red \n 6 = Melee Enemy")]
    //[SerializeField] private int colour;
    [SerializeField] private Bears Target;

    //Ablilities
    private bool support; //Can be activated every "n" turns;
    private bool special; //Only Happens once every Battle

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public int Movement { get => movement; set => movement = value; }
    public int Defense { get => defense; set => defense = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public bool Special { get => special; set => special = value; }
    public bool Support { get => support; set => support = value; }
    public bool Selected { get => selected; set => selected = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public bool TurnComplete { get => turnComplete; set => turnComplete = value; }
   

    public void DistributeStats()
    {
        this.hp = this.bearColor.Hp;
        this.attackStrength = this.bearColor.AttackStrength;
        this.totalHP = this.bearColor.TotalHP;       
        this.defense = this.bearColor.Defense;
        this.Movement = this.bearColor.Movement;
        this.AttackRange = this.bearColor.AttackRange;
        this.bearRace = this.bearColor.BearRace;
    }

    private void Start()
    {        
        GiveColour(color);
        DistributeStats();      
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

    private void GiveColour(EnumColour colour)
    {
        if(colour == EnumColour.Green)
        {
             this.bearColor=GreenBear.instance;
        }
        else if(colour == EnumColour.Black)
        {
            this.bearColor=BlackBear.instance;
        }
        else if (colour == EnumColour.Blue)
        {
            this.bearColor=BlueBear.instance;
        }
        else if (colour == EnumColour.Pink)
        {
            this.bearColor = PinkBear.instance;
        }
        else if (colour == EnumColour.Red)
        {
            this.bearColor = RedBear.instance;
        }
        else if (colour == EnumColour.MeleeEnemy)
        {
            this.bearColor = MeleeEnemy.instance;
        }
        else
        {
            this.bearColor = RedBear.instance;
        }
      

        
    }

    public void GetTarget(Tile selectedTile)
    {
        Target = selectedTile.GetComponentInChildren<Bears>();
    }

    public void GetTarget(Vector2 selectedTile)
    {
        Target = TileManager.instance.TileDic[selectedTile].GetComponentInChildren<Bears>();
    }

    public void MeleeAttack()
    {
        this.bearColor.MeleeAttack();
    }

public void Attack(Tile tileToAttack)
    {
        GetTarget(tileToAttack);
        Target.hp -= this.AttackStrength - Target.Defense;

    }
    public void Attack(Vector2 tileToAttack)
    {
        GetTarget(tileToAttack);
        Target.hp -= this.AttackStrength - Target.Defense;
    }

    public void Abiity1(Tile selectedTile)
    {
        GetTarget(selectedTile);
        this.bearColor.Ability1(Target);
        support = false;
    }

    public void Ability1(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        this.bearColor.Ability1(Target); // it checks which bear instance is using ability and then executes it
        support = false;
    }

    public void Abiity2(Tile selectedTile)
    {
        GetTarget(selectedTile);
        this.bearColor.Ability1(Target);
        special = false;
    }

    public void Ability2(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        this.bearColor.Ability1(Target); // it checks which bear instance is using ability and then executes it
        special = false;
    }


}