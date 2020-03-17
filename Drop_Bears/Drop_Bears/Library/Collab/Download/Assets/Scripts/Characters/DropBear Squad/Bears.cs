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
    private AudioSource audioS;
    private Color bearRace;
    [SerializeField] private bool turnComplete;
    [SerializeField] private int countDown;
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] ability1Sounds;
    [SerializeField] private AudioClip[] ability2Sounds;
    private BearSFX bearSFX;

    [Space()]

    [SerializeField] private bool selected;

    //Needed for turn system
    [SerializeField] private bool isAlive=true;

    public enum EnumColour { Green, Black, Blue, Pink, Red, MeleeEnemy,StrongMeleeEnemy,RangedEnemy };
    [SerializeField] private EnumColour color;
    //[Range(1, 6)] [Tooltip(" 1 = Green \n 2 = Black \n 3 = Blue \n 4 = Pink \n 5 = Red \n 6 = Melee Enemy")]
    //[SerializeField] private int colour;
    [SerializeField] private Bears Target;
    public int avatarNumber;
    public bool onlyOnce = true;

    public int counterSupport = 0;
    
    public Dictionary<string, int> themBuffs = new Dictionary<string, int>();

    public string[] buffNames = new string[] { "buffAttack", "buffMovement", "buffDefence","invincible", };

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
    public int CountDown { get => countDown; set => countDown = value; }

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
        this.CountDown = this.bearColor.CountDown;
        //if (color != EnumColour.MeleeEnemy)
        //{
        //    this.Avatar = avatarCode.Avatar[avatarNumber];
        //}

    }
    /// <summary>
    /// populates the bears idle sounds hurt sounds and attack sounds arrays
    /// </summary>
    private void GetSoundFX ()
    {
        string type = color.ToString()+"IdleClips";
        idleSounds=GetSpecificSoundClips(type);
        type = color.ToString() + "AttackClips";
        attackSounds=GetSpecificSoundClips(type);
        type= color.ToString() + "HurtClips";
        hurtSounds= GetSpecificSoundClips(type);
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

                this.BearColor = GreenBear.instance;
                this.avatarNumber = 3;
                break;

            case EnumColour.Black:

                this.BearColor = BlackBear.instance;
                this.avatarNumber = 0;
                break;

            case EnumColour.Blue:

                this.BearColor = BlueBear.instance;
                this.avatarNumber = 1;
                break;

            case EnumColour.Pink:

                this.BearColor = PinkBear.instance;
                this.avatarNumber = 2;
                break;

            case EnumColour.Red:

                this.BearColor = RedBear.instance;
                this.avatarNumber = 4;
                break;

            case EnumColour.MeleeEnemy:

                this.BearColor = MeleeEnemy.instance;
                break;

            case EnumColour.StrongMeleeEnemy:

                this.BearColor = StrongMeleeEnemy.instance;
                break;

            case EnumColour.RangedEnemy:

                this.BearColor = RangedEnemy.instance;
                break;

            default:

                this.BearColor = RedBear.instance;
                break;
        }



    }
    public override string ToString()
    {
        string info = "HP: " + totalHP.ToString() + "/" + Hp.ToString() + "\n" +
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
            Target.hp -= this.AttackStrength - Target.Defense;
        
            if (attackSounds.Length>0)
            {
                int random = Random.Range(0, attackSounds.Length);
                audioS.PlayOneShot(attackSounds[random]);
            }
            if (Target.hp <= 0)
            {
                SquadSelection.instance.PlayersAlive--;
            }
        }


    }
    public IEnumerator EnemyAttack(Tile tileToAttack, float timer)
    {
        yield return new WaitForSeconds(timer);
        if (Invincible == false)
        {
            GetTarget(tileToAttack);
            if(Target.hurtSounds.Length>0)
            {
                int random = Random.Range(0, hurtSounds.Length);
                Target.audioS.PlayOneShot(hurtSounds[random]);
            }
            Target.hp -= this.AttackStrength - Target.Defense;
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
            Target.audioS.PlayOneShot(ability1Sounds[random]);
        }
        this.BearColor.Ability1(Target);
        support = false;
        counterSupport = CountDown;
    }

    public void Ability1(Vector2 selectedTile)
    {
        GetTarget(selectedTile); //Gets the player you want to use thew ability on
        if (ability1Sounds.Length > 0)
        {
            int random = Random.Range(0, ability1Sounds.Length);
            Target.audioS.PlayOneShot(ability1Sounds[random]);
        }
        this.BearColor.Ability1(Target); // it checks which bear instance is using ability and then executes it
        support = false;
        counterSupport = CountDown;
    }

    public void Ability2(Tile selectedTile)
    {
        GetTarget(selectedTile);
        if (ability2Sounds.Length > 0)
        {
            int random = Random.Range(0, ability2Sounds.Length);
            Target.audioS.PlayOneShot(ability2Sounds[random]);
        }
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
        for(int i = 0; i < buffNames.Length; i++)
        {
            themBuffs[buffNames[i]] = 0;
        }
        
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
        if(counterSupport <= 0)
        {
            support = true;
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
                        this.AttackStrength = this.bearColor.AttackStrength;
                        break;
                    case 1:
                        this.Movement = this.bearColor.Movement;
                        break;
                    case 2:
                        this.Defense = this.BearColor.Defense;
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