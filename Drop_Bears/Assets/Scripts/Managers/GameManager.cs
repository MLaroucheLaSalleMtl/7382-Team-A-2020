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
    [SerializeField]private GameObject selectedTile;
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
    private bool enemyTurn;

    public bool MovementPhase { get => movementPhase; set => movementPhase = value; }
    public bool MenuPhase { get => menuPhase; set => menuPhase = value; }
    public bool AttackPhase { get => attackPhase; set => attackPhase = value; }
    public GameObject InteractionMenu { get => interactionMenu; set => interactionMenu = value; }
    public bool EnemyPhase { get => enemyPhase; set => enemyPhase = value; }

    void Start()
    {
        squadSelector = SquadSelection.instance;
        tilemanager = tilemanager.GetComponent<TileManager>();
      
        btnManager = BtnManager.instance;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerTurn==true)
        {
            if (menuPhase)
            {
                
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
        else if (enemyTurn==true)
        {

        }
    }
}
