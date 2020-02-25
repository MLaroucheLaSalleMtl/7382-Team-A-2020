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
    // Start is called before the first frame update
    [SerializeField] private bool enemyPhase = false;
    [SerializeField] private GameObject selectedPlayer;
    [SerializeField] private GameObject selectedTile;
    [SerializeField] private TileManager tilemanager;
    //This could be a tell to say that your picking movement
    [SerializeField] private bool movementPhase=false;
    [SerializeField] private bool menuPhase = true;
    [SerializeField] private bool attackPhase = false;
    #region Buttons
    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private Button moveBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button endTurnBtn;
    private BtnManager btnManager;
    #endregion Buttons
    [SerializeField] private SquadSelection squadSelector;
    //For the turn system
    [SerializeField]private bool playerTurn;
    private bool onlyOnce=false;
   

    //TurnSystem but to know when to change
    //When it reaches 0 then its the enemies turn
    private int endTurn;

    public bool MovementPhase { get => movementPhase; set => movementPhase = value; }
    public bool MenuPhase { get => menuPhase; set => menuPhase = value; }
    public bool AttackPhase { get => attackPhase; set => attackPhase = value; }
    public GameObject InteractionMenu { get => interactionMenu; set => interactionMenu = value; }
    public bool EnemyPhase { get => enemyPhase; set => enemyPhase = value; }
    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }

    void Start()
    {
        squadSelector = SquadSelection.instance;
        btnManager = BtnManager.instance;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn==true)
        {
            if (menuPhase)
            {
                //Resets every turn
                //endTurn = squadSelector.Squad.Length ;
                //CheckPlayerStatus(5);
               StartCoroutine (CheckTurns());
                selectedPlayer = squadSelector.Squad[squadSelector.Selected];
                interactionMenu.SetActive(true);
                attackPhase = false;
                movementPhase = false;
                
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
            //on end turn player.movementcheck and hasmoved have to be set to false
        }
        else if (enemyPhase==true)
        {
            interactionMenu.SetActive(false);
            MovementPhase = false;
            AttackPhase = false;
            MenuPhase = false;
        }

        
    }

    //For Turn System on the players side
    //Need To Discuss how to further improve it
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
   private IEnumerator CheckTurns()
    {
        bool check = false;
        for(int i=0;i<SquadSelection.instance.Squad.Length;i++)
        {
            if (squadSelector.Squad[i].GetComponent<Bears>().IsAlive && !squadSelector.Squad[i].GetComponent<Bears>().TurnComplete)
            {
                check = true;
            }

        }
        if (check==false)
        {
            playerTurn = false;
            for(int i=0;i<squadSelector.Squad.Length;i++)
            {
                squadSelector.Squad[i].GetComponent<Bears>().TurnComplete = false;
                squadSelector.Squad[i].GetComponent<Movement>().HasMoved = false;
                squadSelector.Squad[i].GetComponent<Movement>().Movementcheck = false;
            }
            EnemyPhase = true;
        }
        yield return new WaitForSeconds(.1f);
    }

    //Dont know if this would work
    void ChangeTurns()
    {
        if (endTurn == 0)
        {
            PlayerTurn = false;
            enemyPhase = true;
        }
        else if (endTurn > 0)
        {
            PlayerTurn = true;
            enemyPhase = false;
        }
    }
}
