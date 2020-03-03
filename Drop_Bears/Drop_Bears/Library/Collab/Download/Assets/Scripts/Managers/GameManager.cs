using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
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
   public enum Phase { menuPhase,attackPhase,enemyPhase,movementPhase}
    private Phase currPhase;
    //might be able to optimize code with enums
    // Start is called before the first frame update
    [SerializeField] private bool enemyPhase = false;
    [SerializeField] private GameObject selectedPlayer;
    [SerializeField] private GameObject selectedTile;
    [SerializeField] private TileManager tilemanager;
    //This could be a tell to say that your picking movement
    [SerializeField] private bool movementPhase = false;
    [SerializeField] private bool menuPhase = true;
    [SerializeField] private bool attackPhase = false;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    //public delegate void switchToPlayerTurnDelegate();
    //public switchToPlayerTurnDelegate playerTurnSwitch;
    #region Buttons
    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private Button moveBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button Ability1Btn;
    [SerializeField] private Button Ability2Btn;
    [SerializeField] private Button endTurnBtn;
    private BtnManager btnManager;
    #endregion Buttons
    [SerializeField] private SquadSelection squadSelector;
    //For the turn system
    [SerializeField] private bool playerTurn;
    private bool onlyOnce = false;



    //TurnSystem but to know when to change
    //When it reaches 0 then its the enemies turn
    private int endTurn;
    private EnemyManager enemyManager;
    public bool MovementPhase { get => movementPhase; set => movementPhase = value; }
    public bool MenuPhase { get => menuPhase; set => menuPhase = value; }
    public bool AttackPhase { get => attackPhase; set => attackPhase = value; }
    public GameObject InteractionMenu { get => interactionMenu; set => interactionMenu = value; }
    public bool EnemyPhase { get => enemyPhase; set => enemyPhase = value; }
    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }
    public Phase CurrPhase { get => currPhase; set => currPhase = value; }

    void Start()
    {
        squadSelector = SquadSelection.instance;
        btnManager = BtnManager.instance;
        enemyManager = EnemyManager.instance;
        playerTurn = true;
        EnemyPhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager.EnemiesAlive > 0 &&squadSelector.PlayersAlive>0)
             {
            if (PlayerTurn == true)
            {

                if (menuPhase)
                {
                    //Resets every turn
                    //endTurn = squadSelector.Squad.Length ;
                    //CheckPlayerStatus(5);
                    StartCoroutine(CheckTurns());
                    selectedPlayer = squadSelector.Squad[squadSelector.Selected];
                    interactionMenu.SetActive(true);
                    attackPhase = false;
                    movementPhase = false;
                    //this is where btn Move 
                    if (selectedPlayer.GetComponent<Bears>().HasAttacked)
                    {
                        attackBtn.interactable = false;
                        Ability1Btn.interactable = false;
                        Ability2Btn.interactable = false;
                        Ability2Btn.interactable = false;
                    }
                    else
                    {
                        attackBtn.interactable = true;
                        Ability1Btn.interactable = true;
                        Ability2Btn.interactable = true;
                    }
                    if (selectedPlayer.GetComponent<Movement>().HasMoved)
                        moveBtn.interactable = false;
                    else
                        moveBtn.interactable = true;
                }
                else if (attackPhase)
                {
                    interactionMenu.SetActive(false);
                }
                else if (MovementPhase)
                {
                    interactionMenu.SetActive(false);
                }
                if (!onlyOnce)
                {
                    enemyManager.Invoke("ResetEnemyTurns", .1f);
                    onlyOnce = true;
                }
            }
            else if (enemyPhase == true)
            {

                interactionMenu.SetActive(false);
                MovementPhase = false;
                AttackPhase = false;
                MenuPhase = false;
                for (int i = 0; i < enemyManager.Enemies.Length; i++)
                {
                    if (enemyManager.Enemies[i].GetComponent<Bears>().IsAlive && enemyManager.FirstEnemyHasActed == false)
                    {
                        enemyManager.Enemies[i].GetComponent<EnemyAIBase>().TakeTurn = true;
                        enemyManager.FirstEnemyHasActed = true;
                    }
                }
                StartCoroutine(enemyManager.WaitForTurn());
                if (enemyManager.Enemies[enemyManager.Enemies.Length - 1].GetComponent<EnemyAIBase>().TurnCompleted)
                {
                    onlyOnce = false;
                    enemyManager.SwitchToPlayerTurns();

                }

            }
        }
        else if(enemyManager.EnemiesAlive<=0)
        {
            winPanel.SetActive(true);
        }
        else if (squadSelector.PlayersAlive<=0)
        {
            losePanel.SetActive(true);
        }
    }
    

    //[SerializeField] private GameObject[] supportCheck;
    //[SerializeField] private GameObject[] specialCheck;
    //private SquadSelection selectedSquad;
    //void CheckAbilities()
    //{
    //    if (s == false)
    //    {
    //        supportCheck[squadSelector.Squad[squadSelector.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
    //    }
    //    else
    //    {
    //        supportCheck[squadSelector.Squad[squadSelector.Selected].GetComponent<Bears>().avatarNumber].SetActive(true);
    //    }

    //    if (Special == false)
    //    {
    //        specialCheck[squadSelector.Squad[squadSelector.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
    //    }
    //    else
    //    {
    //        supportCheck[squadSelector.Squad[squadSelector.Selected].GetComponent<Bears>().avatarNumber].SetActive(true);
    //    }
    //}

    void CheckPlayerStatus(int totalPlayers)
    {
        selectedPlayer = squadSelector.Squad[squadSelector.Selected];

        for (int i = 0; i < totalPlayers; i++)
        {
            //to see if selected character can play
            //Dont know which script do the bears inherit from so it can be changed after
            if (selectedPlayer.GetComponent<Bears>().IsAlive == true)
            {
                //Do the menu functions/actions

                //EndTurn button for each character = -1 endturn

            }
            else if (selectedPlayer.GetComponent<Bears>().IsAlive == false)
            {
                //If a character is dead endturn will countdown already
                //ex. if 4 are alive and 1 is dead, endturn will become 4
                endTurn -= 1;
            }
        }
    }
    IEnumerator CheckTurns()
    {
        bool check = false;
        for (int i = 0; i < squadSelector.Squad.Length; i++)
        {
            if (squadSelector.Squad[i].GetComponent<Bears>().IsAlive && !squadSelector.Squad[i].GetComponent<Bears>().TurnComplete)
            {
                check = true;
               

            }

        }
        if (check == false)
        {
            //this is when the player turn ends it swithces to enemy so put cooldownstuff here
            playerTurn = false;
            for (int i = 0; i < squadSelector.Squad.Length; i++)
            {
                squadSelector.Squad[i].GetComponent<Bears>().HasAttacked = false;
                squadSelector.Squad[i].GetComponent<Bears>().TurnComplete = false;
                squadSelector.Squad[i].GetComponent<Movement>().HasMoved = false;
                squadSelector.Squad[i].GetComponent<Movement>().Movementcheck = false;

                //Reset Stats
                squadSelector.Squad[i].GetComponent<Bears>().AttackStrength = squadSelector.Squad[i].GetComponent<Bears>().BearColor.AttackStrength;
                squadSelector.Squad[i].GetComponent<Bears>().Defense = squadSelector.Squad[i].GetComponent<Bears>().BearColor.Defense;
                squadSelector.Squad[i].GetComponent<Bears>().Movement = squadSelector.Squad[i].GetComponent<Bears>().BearColor.Movement;
                squadSelector.Squad[i].GetComponent<Bears>().Invincible = false;
            }
            EnemyPhase = true;


        }
        yield return new WaitForSeconds(.1f);
    }
}
