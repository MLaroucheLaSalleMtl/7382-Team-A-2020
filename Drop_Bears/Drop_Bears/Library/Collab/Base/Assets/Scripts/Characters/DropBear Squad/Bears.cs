using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bears : MonoBehaviour
{
    //Stats
    private IBear bearColor;
    [Header("Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int totalHP;
    [SerializeField] private int defense;
    [SerializeField] private int attackStrength;
    [SerializeField] private int range;
    [SerializeField] private int movement;
    [SerializeField] private bool hasAttacked;
    private Color bearRace;
    [SerializeField] private bool turnComplete;
    

    [Space()]

    [SerializeField] private bool selected;

    //Needed for turn system
    [SerializeField] private bool isAlive=true;

    public enum EnumColour { Green, Black, Blue, Pink, Red, MeleeEnemy };
    [SerializeField] private EnumColour color;
    //[Range(1, 6)] [Tooltip(" 1 = Green \n 2 = Black \n 3 = Blue \n 4 = Pink \n 5 = Red \n 6 = Melee Enemy")]
    //[SerializeField] private int colour;
    [SerializeField] private Bears Target;
    public int avatarNumber;
    private bool onlyOnce = true;


    //Ablilities
    private bool support; //Can be activated every "n" turns;
    private bool special; //Only Happens once every Battle
    private bool invincible;

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public int Movement { get => movement; set => movement = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Range { get => range; set => range = value; }
    public bool Special { get => special; set => special = value; }
    public bool Support { get => support; set => support = value; }
    public bool Selected { get => selected; set => selected = value; }
    public bool IsAlive { get => (hp>0)?true:false; set => isAlive = value; }
    public bool TurnComplete { get => turnComplete; set => turnComplete = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }
    public bool HasAttacked { get => hasAttacked; set => hasAttacked = value; }
    public bool Invincible { get => invincible; set => invincible = value; }
    public IBear BearColor { get => bearColor; set => bearColor = value; }

    public void DistributeStats()
    {
        this.isAlive = true;
        this.hp = this.BearColor.Hp;
        this.attackStrength = this.BearColor.AttackStrength;
        this.totalHP = this.BearColor.TotalHP;       
        this.defense = this.BearColor.Defense;
        this.Movement = this.BearColor.Movement;
        this.Range = this.BearColor.AttackRange;
        this.BearRace = this.BearColor.BearRace;
        //if (color != EnumColour.MeleeEnemy)
        //{
        //    this.Avatar = avatarCode.Avatar[avatarNumber];
        //}

    }
    private void Awake()
    {
        //this.enabled = true;
        //GiveColour(color);
        //DistributeStats();
      //  this.avatarCode = AvatarManager.instance;

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
            if (this.color == EnumColour.MeleeEnemy)
            {
                if (onlyOnce)
                {
                    onlyOnce = false;
                    Die();
                }
            }
            else
            {
                if(onlyOnce)
                {
                    onlyOnce = false;
                    Die();
                }
            }
        }
    }

    private void GiveColour(EnumColour colour)
    {
        if(colour == EnumColour.Green)
        {
            this.BearColor=GreenBear.instance;
            this.avatarNumber = 3;
        }
        else if(colour == EnumColour.Black)
        {
            this.BearColor=BlackBear.instance;
            this.avatarNumber = 0;
        }
        else if (colour == EnumColour.Blue)
        {
            this.BearColor=BlueBear.instance;
            this.avatarNumber = 1;
        }
        else if (colour == EnumColour.Pink)
        {
            this.BearColor = PinkBear.instance;
            this.avatarNumber = 2;
        }
        else if (colour == EnumColour.Red)
        {
            this.BearColor = RedBear.instance;
            this.avatarNumber = 4;
        }
        else if (colour == EnumColour.MeleeEnemy)
        {
            this.BearColor = MeleeEnemy.instance;
        }
        else
        {
            this.BearColor = RedBear.instance;
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


public void Attack(Tile tileToAttack)
    {
        if (Invincible == false)
        {
            GetTarget(tileToAttack);
            Target.hp -= this.AttackStrength - Target.Defense;
            if (Target.hp <= 0)
                SquadSelection.instance.PlayersAlive--;
                
        }

        
    }
    public void PlayerAttack(Tile tileToAttack, int ability)
    {
        switch (ability)
        {
            case 1:
            Attack(tileToAttack);
                break;
            case 2:
                Ability1(tileToAttack);
                break;
            case 3:
        Ability2(tileToAttack);
                break;
    }
        if(Target.hp<=0)
        {
            EnemyManager.instance.EnemiesAlive--;
        }
        AttackRange.ClearTileAttackValues();
        GameManager.instance.AttackPhase = false;
        GameManager.instance.MenuPhase = true;
    }
    public void Attack(Vector2 tileToAttack)
    {
        if (Invincible == false)
        {
            GetTarget(tileToAttack);
            Target.hp -= this.AttackStrength - Target.Defense;
        }
       
    }

    public void Ability1(Tile selectedTile)
    {
        GetTarget(selectedTile);
        this.BearColor.Ability1(Target);
        support = false;
    }

    public void Ability1(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        this.BearColor.Ability1(Target); // it checks which bear instance is using ability and then executes it
        support = false;
    }

    public void Ability2(Tile selectedTile)
    {
        GetTarget(selectedTile);
        this.BearColor.Ability2(Target);
        special = false;
    }

    public void Ability2(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        this.BearColor.Ability1(Target); // it checks which bear instance is using ability and then executes it
        special = false;
    }

    public void Die()
    {
        this.transform.Rotate(90f, 0f, 0f);
        if (this.color == EnumColour.MeleeEnemy)
        {
            Invoke("Gone", 4f);
            Destroy(this.GetComponentInChildren<Canvas>());
        }
    }

    public void Gone()
    {
        this.gameObject.SetActive(false);
        Destroy(this.GetComponentInChildren<Canvas>());
    }

}