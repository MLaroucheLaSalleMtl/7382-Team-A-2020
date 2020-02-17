using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TileManager tilemanager;
    [SerializeField] private GameManager code;
    [SerializeField] private SquadSelection squadManager;
    private bool tilecheck = false;
    private GameObject currentTile;
    [SerializeField]private bool canBeSelected=true;
    public static TileSelector instance;

    public GameObject CurrentTile { get => currentTile; set => currentTile = value; }

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
    void Start()
    {
        tilemanager = tilemanager.GetComponent<TileManager>();
      code= GameManager.instance;
        squadManager = SquadSelection.instance;
    }
    private void MoveTile(Vector2 tilecoordinates,Tile currentTileShort)
    {
        #region ZachNotes
        //This function allows the cursor to move from tile to tile 
        //pass it the the coordinates of the tile you want to go to 
        //and the current tile
        #endregion ZachNotes
        GameObject nextTile = tilemanager.GetTileDic(tilecoordinates);
        if (!nextTile.GetComponent<Tile>().IsObstacle)
        {
            currentTileShort.IsSelected = false;
            nextTile.GetComponent<Tile>().IsSelected = true;
            CurrentTile = nextTile;
        }
    }
    // Update is called once per frame
    void Update()
    {
        #region ZachNotes
        //We would only want to see the characters movement range if hes selected and we are in 
        //a movement phase I would think
        #endregion ZachNotes
        if (code.AttackPhase || code.MovementPhase)
        {
            canBeSelected = true;
            
        }
        else
        {
            canBeSelected = false;
            tilecheck = false;
            if (CurrentTile != null)
            {
                CurrentTile.GetComponent<Tile>().IsSelected = false;
                squadManager.Squad[squadManager.Selected].GetComponent<Movement>().DisplayMovementRange();
                CurrentTile = null;
            }

        }
      
        if (canBeSelected)
        {
            if (!tilecheck)
            {
                #region ZachNotes
                //Replace this later with selected character position
                //needs to be sent from an outside script the one that switches to movement phase
                CurrentTile = tilemanager.GetTileDic(squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Position);
                #endregion ZachNotes
                CurrentTile.GetComponent<Tile>().IsSelected = true;
                tilecheck = true;

            }
            Tile currentTileShort = CurrentTile.GetComponent<Tile>();
            #region ZachNotes
            //We can replace this with the new input system later
            //Also maybe support mouse if you guys want
            #endregion ZachNotes
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
            {

                MoveTile(new Vector2(currentTileShort.Loc.x - 1, currentTileShort.Loc.y), currentTileShort);
            }
            else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
            {
                MoveTile(new Vector2(currentTileShort.Loc.x + 1, currentTileShort.Loc.y), currentTileShort);
            }
            else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Abs(Input.GetAxisRaw("Vertical")))
            {
                MoveTile(new Vector2(currentTileShort.Loc.x, currentTileShort.Loc.y + 1), currentTileShort);
            }
            else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Abs(Input.GetAxisRaw("Vertical")))
            {
                MoveTile(new Vector2(currentTileShort.Loc.x, currentTileShort.Loc.y - 1), currentTileShort);
            }
            if (Input.GetButtonDown("Jump"))
            {

                if (currentTileShort.Movementvalue > 0&&!currentTileShort.IsPlayer &&!currentTileShort.IsEnemy)
                {
                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().MoveDestination = CurrentTile;
                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().ExecuteMovement = true;
                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().ExecuteMovement = true;
                }
                if(currentTileShort.Attackvalue>0 &&currentTileShort.IsPlayer||currentTileShort.IsEnemy)
                {
                    
                }
            }
         
        }
        //else
        //{

        //}
    }
}
