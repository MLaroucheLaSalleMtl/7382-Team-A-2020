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

    #region ZachFootnote
    //I feel like we will need a character select system so imma just put this here for now Zach
    #endregion ZachFootnote
    [SerializeField] private GameObject selectedPlayer;
    [SerializeField]private GameObject selectedTile;
    [SerializeField] private TileManager tilemanager;
    [SerializeField] private PlayerManager playermanager;
    //For the turn system
    private bool playerTurn;
    private bool enemyTurn;
    
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
