using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
