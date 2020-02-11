using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    #region ZachFootnote
    //I feel like we will need a character select system so imma just put this here for now Zach
    #endregion ZachFootnote
   [SerializeField] private GameObject selectedPlayer;
    [SerializeField]private GameObject selectedTile;
    [SerializeField] private TileManager tilemanager;
    [SerializeField] private PlayerManager playermanager;
    //This could be a tell to say that your picking movement
    [SerializeField] private bool movementPhase=true;
    //For the turn system
    private bool playerTurn;
    private bool enemyTurn;

    public bool MovementPhase { get => movementPhase; set => movementPhase = value; }

    void Start()
    {
        tilemanager = tilemanager.GetComponent<TileManager>();
        playermanager = playermanager.GetComponent<PlayerManager>();
    }
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerTurn==true)
        {

        }
        else if (enemyTurn==true)
        {

        }
    }
}
