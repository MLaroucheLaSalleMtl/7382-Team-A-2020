using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TileManager tilemanager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerManager playermanager;
    private bool tilecheck = false;
    private GameObject currentTile;
    void Start()
    {
        tilemanager = tilemanager.GetComponent<TileManager>();
        gameManager = GameManager.instance;
        playermanager = playermanager.GetComponent<PlayerManager>();
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
            currentTile = nextTile;
        }
    }
    // Update is called once per frame
    void Update()
    {
        #region ZachNotes
        //We would only want to see the characters movement range if hes selected and we are in 
        //a movement phase I would think
        #endregion ZachNotes
        //if(gameManager.MovementPhase)
        //{

        if (!tilecheck)
            {
            #region ZachNotes
            //Replace this later with selected character position
            //needs to be sent from an outside script the one that switches to movement phase
            currentTile = tilemanager.GetTileDic(new Vector2(0, 0));
            #endregion ZachNotes
            currentTile.GetComponent<Tile>().IsSelected = true;
            tilecheck = true;
          
        }
        Tile currentTileShort = currentTile.GetComponent<Tile>();
        #region ZachNotes
        //We can replace this with the new input system later
        //Also maybe support mouse if you guys want
        #endregion ZachNotes
        if (Input.GetButtonDown("Horizontal")&&Input.GetAxisRaw("Horizontal") < 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
        {
           
            MoveTile(new Vector2(currentTileShort.Loc.x - 1, currentTileShort.Loc.y),currentTileShort);
        }
     else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
        {
            MoveTile(new Vector2(currentTileShort.Loc.x + 1, currentTileShort.Loc.y), currentTileShort);
        }
      else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Abs(Input.GetAxisRaw("Vertical")))
        {
            MoveTile(new Vector2(currentTileShort.Loc.x, currentTileShort.Loc.y + 1), currentTileShort);
        }
      else  if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Abs(Input.GetAxisRaw("Vertical")))
        {
            MoveTile(new Vector2(currentTileShort.Loc.x, currentTileShort.Loc.y - 1), currentTileShort);
        }
       if (Input.GetButtonDown("Jump"))
        {
            
            if (currentTileShort.Movementvalue>0)
            {
                playermanager.Players[0].GetComponent<Movement>().MoveDestination = currentTile;
                playermanager.Players[0].GetComponent<Movement>().ExecuteMovement = true;
            }
        }
        //}
    }
}
