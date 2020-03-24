using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Shit to do neccesary
//Menus finish and make em perty (may neeed like intermission menus depends on what we go for, ambitions)
//story
//voice lines finish them (just need pinky)
//Levels
//Animations
//Boss Fight
//enviroments

//Shit to Do would be nice
//Leveling system.
//skill tree (would need to create a class abilities then assign them to an array for the bears)
//menus inbetween levels to assign skills and stuff
//cooler abilities aoe's poisons etc...
//due date april 21st

   //currnt Bugs 
   //conf menu 
   //and attack descs disapearing
public class Bears : MonoBehaviour
{
    //Stats
   [SerializeField] private BearColor bearJob;
    [Header("Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int totalHP;
    [SerializeField] private int defense;
    [SerializeField] private int attackStrength;
    [SerializeField] private int range;
    [SerializeField] private int movement;
    [SerializeField] private bool hasAttacked;
    [SerializeField] private Ability basicAbility;
    [SerializeField] private Ability specialAbility;
    private AudioSource audioS;
    private Color bearRace;
    [SerializeField] private bool turnComplete;
    [SerializeField] private int countDown;

    //Sounds Clips
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private AudioClip[] ability1Sounds;
    [SerializeField] private AudioClip[] ability2Sounds;
    private BearSFX bearSFX;
    private Animator anim;

    [Space()]

    [SerializeField] private bool selected;

    //Needed for turn system
    [SerializeField] private bool isAlive = true;

    public enum EnumColour { Green, Black, Blue, Pink, Red, MeleeEnemy, StrongMeleeEnemy, RangedEnemy };
    [SerializeField] private EnumColour color;
    //[Range(1, 6)] [Tooltip(" 1 = Green \n 2 = Black \n 3 = Blue \n 4 = Pink \n 5 = Red \n 6 = Melee Enemy")]
    //[SerializeField] private int colour;
    [SerializeField] private Bears Target;
    public int avatarNumber;
    public bool onlyOnce = true;

    public int counterSupport = 0;

    public Dictionary<string, int> themBuffs = new Dictionary<string, int>();

    public string[] buffNames = new string[] { "buffAttack", "buffMovement", "buffDefence", "invincible", };

    //Ablilities
    private bool support; //Can be activated every "n" turns;
    private bool special; //Only Happens once every Battle
    private bool invincible;
    #region Properties
    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public int Movement { get => movement; set => movement = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Range { get => range; set => range = value; }
    public bool Special { get => special; set => special = value; }
    public bool Support { get => support; set => support = value; }
    public bool Selected { get => selected; set => selected = value; }
    public bool IsAlive { get => (hp > 0) ? true : false; set => isAlive = value; }
    public bool TurnComplete { get => turnComplete; set => turnComplete = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }
    public bool HasAttacked { get => hasAttacked; set => hasAttacked = value; }
    public bool Invincible { get => invincible; set => invincible = value; }

    public int CountDown { get => countDown; set => countDown = value; }
    public AudioClip[] MoveSounds { get => moveSounds; }
    public AudioSource AudioS { get => audioS; }
    public BearColor BearJob { get => bearJob; set => bearJob = value; }
    public Ability BasicAbility { get => basicAbility; set => basicAbility = value; }
    public Ability SpecialAbility { get => specialAbility; set => specialAbility = value; }
    #endregion Properties
    public void DistributeStats()
    {
        this.isAlive = true;
        this.hp = this.bearJob.Hp;
        this.attackStrength = this.bearJob.AttackStrength;
        this.totalHP = this.bearJob.TotalHp;
        this.defense = this.bearJob.Defense;
        this.Movement = this.bearJob.Movement;
        this.Range = this.bearJob.AttackRange;
        this.BearRace = this.bearJob.BearRace;
        this.CountDown = this.bearJob.Countdown;
        

    }
    /// <summary>
    /// populates the bears idle sounds hurt sounds and attack sounds arrays
    /// </summary>
    private void GetSoundFX()
    {
        string type = color.ToString() + "IdleClips";
        idleSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "AttackClips";
        attackSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "MoveClips";
        moveSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "HurtClips";
        hurtSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "Ability1Clips";
        ability1Sounds = GetSpecificSoundClips(type);
        type = color.ToString() + "Ability2Clips";
        ability2Sounds = GetSpecificSoundClips(type);

    }
    /// <summary>
    /// Give it the name of the audioclip[] you want from the BearSfx and it will return the array
    /// </summary>
    /// <param name="typeofclip"></param>
    /// <returns></returns>
    AudioClip[] GetSpecificSoundClips(string typeofclip)
    {
        #region ZachWin
        //K so im super proud of this line of code it looks complicated but basically 
        //it allows the script to dynamically go into the sound script and populate the 
        //bears voice clips lines in like very little lines of code
        //as long as the string will match the name of the variable in the bearsfx script
        //as long as the audio clips exist in the audio manager
        #endregion ZachWin
        if (bearSFX.GetType().GetProperty(typeofclip) != null)
        {
            return (AudioClip[])bearSFX.GetType().GetProperty(typeofclip).GetValue(bearSFX);
        }
        else return null;
    }
    private void GiveColour(EnumColour colour)
    {
        switch (colour)
        {
            case EnumColour.Green:

                bearJob = gameObject.AddComponent<GreenBear>();
                BasicAbility = new HunkerDown();
                SpecialAbility = new Juggernaut();
                this.avatarNumber = 3;
                break;

            case EnumColour.Black:

                this.bearJob = gameObject.AddComponent< BlackBear>();
                BasicAbility = new BeefUp();
                SpecialAbility = new DriveBy();
                this.avatarNumber = 0;
                break;

            case EnumColour.Blue:

                bearJob = gameObject.AddComponent<BlueBear>();
                BasicAbility = new GottaGoQuick();
                SpecialAbility = new PowerStrike();
                this.avatarNumber = 1;
                break;

            case EnumColour.Pink:

                bearJob = gameObject.AddComponent<PinkBear>();
                BasicAbility = new Heal();
                SpecialAbility = new Resurrect();
                this.avatarNumber = 2;
                break;

            case EnumColour.Red:

                bearJob = gameObject.AddComponent<RedBear>();
                this.avatarNumber = 4;
                BasicAbility = new BodyFire();
                SpecialAbility = new Energize();
                break;

            case EnumColour.MeleeEnemy:

                bearJob = gameObject.AddComponent<MeleeEnemy>();
                break;

            case EnumColour.StrongMeleeEnemy:

                bearJob = gameObject.AddComponent<StrongMeleeEnemy>();
                break;

            case EnumColour.RangedEnemy:

                bearJob = gameObject.AddComponent<RangedEnemy>();
                break;

            default:

                bearJob = gameObject.AddComponent<RedBear>(); ;
                break;
        }



    }
    public override string ToString()
    {
        string info = "HP: " + Hp.ToString() + "/" + totalHP.ToString() + "\n" +
           "Attack: " + attackStrength.ToString() + "\n" +
           "Defense: " + defense.ToString() + "\n" +
           "Movement: " + movement + "\n" +
           "Range: " + range;
        return info;
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
            if (attackSounds != null)
                if (attackSounds.Length > 0)
                {
                    int random = Random.Range(0, attackSounds.Length);
                    AudioS.PlayOneShot(attackSounds[random]);
                }
            if (Target != null)
            {
                Target.hp -= this.AttackStrength - Target.Defense;
              
                if (Target.hp <= 0)
                {
                    EnemyManager.instance.EnemiesAlive--;
                }
            }
        }


    }
    public void EnemyAttack(Tile tileToAttack)
    {
        if (Invincible == false)
        {
            GetTarget(tileToAttack);
            if (Target != null)
                Target.hp -= this.AttackStrength - Target.Defense;
            if (attackSounds != null)
                if (attackSounds.Length > 0)
                {
                    int random = Random.Range(0, attackSounds.Length);
                    AudioS.PlayOneShot(attackSounds[random]);
                }
            if (Target.hp <= 0)
            {
                SquadSelection.instance.PlayersAlive--;
            }
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
        if(Target!=null)
        if (Target.hp <= 0)
        {
            EnemyManager.instance.EnemiesAlive--;
        }
        AttackRange.ClearTileAttackValues();
        GameManager.instance.CurrPhase = GameManager.Phase.menuPhase;
        BtnManager.instance.OnClickAttack();
        //GameManager.instance.AttackPhase = false;
        //GameManager.instance.MenuPhase = true;
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
        if (ability1Sounds.Length > 0)
        {
            int random = Random.Range(0, ability1Sounds.Length);
            Target.AudioS.PlayOneShot(ability1Sounds[random]);
        }
        BasicAbility.CastAbility(selectedTile, AttackStrength);
        support = false;
        counterSupport = CountDown;
    }

    public void Ability1(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        if (ability1Sounds.Length > 0)
        {
            int random = Random.Range(0, ability1Sounds.Length);
            Target.AudioS.PlayOneShot(ability1Sounds[random]);
        }
        this.bearJob.Ability1(Target,AttackStrength); // it checks which bear instance is using ability and then executes it
        support = false;
        counterSupport = CountDown;
    }

    public void Ability2(Tile selectedTile)
    {
        GetTarget(selectedTile);
        if (ability2Sounds.Length > 0)
        {
            int random = Random.Range(0, ability2Sounds.Length);
            Target.AudioS.PlayOneShot(ability2Sounds[random]);
        }
        SpecialAbility.CastAbility(selectedTile, AttackStrength);
        special = false;
    }

    public void Ability2(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        this.bearJob.Ability1(Target,AttackStrength); // it checks which bear instance is using ability and then executes it        
        special = false;
    }


    public void Die()
    {
        this.gameObject.transform.Rotate(90f, 0f, 0f);
        if (this.color == EnumColour.MeleeEnemy || this.color == EnumColour.RangedEnemy || this.color == EnumColour.StrongMeleeEnemy)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;

            GetComponentInParent<Tile>().IsEnemy = false;
            Invoke("Gone", 4f);
            this.GetComponentInChildren<Canvas>().enabled = false;

        }
    }

    public void Gone()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Light>().enabled = false;

    }
    private void Start()
    {
        GiveColour(color);
        DistributeStats();
        bearSFX = BearSFX.instance;
        audioS = GetComponent<AudioSource>();
        GetSoundFX();
        for (int i = 0; i < buffNames.Length; i++)
        {
            themBuffs[buffNames[i]] = 0;
        }
        anim = GetComponent<Animator>();
        
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

            if (onlyOnce)
            {
                onlyOnce = false;
                Die();
            }

        }
        if (counterSupport <= 0)
        {
            support = true;
        }
        if (anim != null)
        {

        }

    }

    public void CheckThemBuffs()
    {
        for (int i = 0; i < buffNames.Length; i++)
        {
            if (themBuffs[buffNames[i]] <= 0)
            {
                switch (i)
                {
                    case 0:
                        this.AttackStrength = this.bearJob.AttackStrength;
                        break;
                    case 1:
                        this.Movement = this.bearJob.Movement;
                        break;
                    case 2:
                        this.Defense = this.bearJob.Defense;
                        break;
                    case 3:
                        this.Invincible = false;
                        break;
                }
            }
        }
    }

    public void DeductThemBuffs()
    {
        for (int i = 0; i < buffNames.Length; i++)
        {
            themBuffs[buffNames[i]]--;
        }
    }





}