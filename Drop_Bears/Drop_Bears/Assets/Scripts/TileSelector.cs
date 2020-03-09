using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private int abilityToUse;
    public GameObject CurrentTile { get => currentTile; set => currentTile = value; }
    public int AbilityToUse { get => abilityToUse; set => abilityToUse = value; }
    public int x = 0;
    public int y = 0;
    private bool isSubmitted = false;
    private bool isCancel = false;

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

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.started)
        {
          
                isCancel = !isCancel;
          
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
                isSubmitted = !isSubmitted;
            
        }
    }

    public void NavigateTile(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 selection = context.ReadValue<Vector2>();
            x = (int)selection.x;
            y = (int)selection.y;
        }
        
        
    }
    public void ResetXAndY()
    {
        x = 0;
        y = 0;
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
        GameObject nextTile = tilemanager.GetTileDicMove(tilecoordinates,currentTileShort);
        currentTileShort.IsSelected = false;
            nextTile.GetComponent<Tile>().IsSelected = true;
            CurrentTile = nextTile;
        
    }
    private void ClearValue()
    {
        x = 0;
        y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #region ZachNotes
        //We would only want to see the characters movement range if hes selected and we are in 
        //a movement phase I would think
        #endregion ZachNotes
        //  if (code.CurrPhase==GameManager.Phase.attackPhase||code.CurrPhase==GameManager.Phase.movementPhase)
        if (!PauseMenu.isGamePaused)
        {
            //canBeSelected is the variable that allows you to maneover the grid
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
                if (/*Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0*/ x < 0 && /*Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical"))*/ Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //MoveTile Moves the selected tile it takes a x and y coordinate to move too
                    MoveTile(new Vector2(currentTileShort.Loc.x - 1, currentTileShort.Loc.y), currentTileShort);
                    ClearValue();
                }
                else if (/*Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal")*/x > 0 && Mathf.Abs(x/*Input.GetAxisRaw("Horizontal")*/) > Mathf.Abs(y/*Input.GetAxisRaw("Vertical")*/))
                {
                    MoveTile(new Vector2(currentTileShort.Loc.x + 1, currentTileShort.Loc.y), currentTileShort);
                    ClearValue();
                }
                else if (/*Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical")*/y > 0 && Mathf.Abs(x/*Input.GetAxisRaw("Horizontal")*/) < Mathf.Abs(y/*Input.GetAxisRaw("Vertical")*/))
                {
                    MoveTile(new Vector2(currentTileShort.Loc.x, currentTileShort.Loc.y + 1), currentTileShort);
                    ClearValue();
                }
                else if (/*Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical")*/y < 0 && Mathf.Abs(x/*Input.GetAxisRaw("Horizontal")*/) < Mathf.Abs(y/*Input.GetAxisRaw("Vertical")*/))
                {
                    MoveTile(new Vector2(currentTileShort.Loc.x, currentTileShort.Loc.y - 1), currentTileShort);
                    ClearValue();
                }
                if (/*Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")*/isSubmitted)
                {
                    #region ZachNotes
                    //so this will pass an to the player depending on what we want to do 
                    //currently we only pass it the move;
                    #endregion ZachNotes
                    if (currentTileShort.Movementvalue > 0 && !currentTileShort.IsPlayer && !currentTileShort.IsEnemy)
                    {
                        squadManager.Squad[squadManager.Selected].GetComponent<Movement>().MoveDestination = CurrentTile;
                        squadManager.Squad[squadManager.Selected].GetComponent<Movement>().ExecuteMovement = true;


                    }
                    if (currentTileShort.Attackvalue > 0 && (currentTileShort.IsPlayer || currentTileShort.IsEnemy) && /*code.AttackPhase */code.CurrPhase == GameManager.Phase.attackPhase)
                    {
                        squadManager.Squad[squadManager.Selected].GetComponent<Bears>().PlayerAttack(currentTileShort, abilityToUse);
                        squadManager.Squad[squadManager.Selected].GetComponent<Bears>().HasAttacked = true;


                    }
                    
                }
                if (/*Input.GetButton("Back")*/isCancel)
                {
                    Movement.ClearTileMovementValues();
                    AttackRange.ClearTileAttackValues();
                    code.CurrPhase = GameManager.Phase.menuPhase;
                  
                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Movementcheck = false;
                    squadManager.Squad[squadManager.Selected].GetComponent<AttackRange>().JustOnce = false;
                   
                }

            }
                if (code.CurrPhase == GameManager.Phase.attackPhase || code.CurrPhase == GameManager.Phase.movementPhase||code.CurrPhase==GameManager.Phase.mapPhase)
                {
                canBeSelected = true;

            }
            else
            {
                isCancel = false;
                canBeSelected = false;
                tilecheck = false;
                isSubmitted = true;
                #region ZachNotes
                //So this makes it so that when you select an option the first tile selected is 
                //the players tile
                #endregion ZachNotes
                if (CurrentTile != null)
                {
                    CurrentTile.GetComponent<Tile>().IsSelected = false;
                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().DisplayMovementRange();
                    CurrentTile = null;
                }

            }
        }
        ResetXAndY();
    }
}
