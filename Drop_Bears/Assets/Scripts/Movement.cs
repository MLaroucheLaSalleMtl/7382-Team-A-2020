using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private List<Vector2> moveList;
    [SerializeField]private int x;
   [SerializeField] private int y;
    //here for testing purposes 
    [SerializeField]private int move = 5; 
    [SerializeField] private TileManager tilemanager;
   //Movementcheck is to say whether the player has already moved this turn
    [SerializeField]private bool movementcheck = false;
    [SerializeField]private bool moving;
  
    private bool executeMovement;
    private GameObject moveDestination;
    private float timer = .05f;
    private GameManager code;
    public int Move { get => move; set => move = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool ExecuteMovement { get => executeMovement; set => executeMovement = value; }
    public GameObject MoveDestination { get => moveDestination; set => moveDestination = value; }
    public bool Movementcheck { get => movementcheck; set => movementcheck = value; }
    #region ZachNotes
    //this gets all the tiles moveable to not accounting for obstacles
    //currently no need but may be usefull for aoe attacks and stuff
    //would need to be slightly modified for that though
    #endregion ZachNotes
    //private void GetMovementRange1()
    //{ 
    //    for (int i = x - Move; i <= x + Move; i++)
    //    {
    //        int cal1 = Mathf.Abs(x - i);
    //        int diff = Mathf.Abs(cal1 - Move);
    //              for (int k = this.y - diff; k <= this.y + diff; k++)
    //            {
    //                moveList.Add(new Vector2(i, k));
    //            }
    //    }
    //}


    private void AssignTileMovementValue(GameObject tile,int move)
    {
        #region ZachNotes
        //So this is the function that produces the movement range.
        //it works by starting from the player and assigning descending number values 
        //(starting from his movement range) to tiles around the 
        //player based on their distance to the player (further the tile lower the value).
        //With these movement values on the tiles we can determine if they are within the players
        //movement range if they are higher than 0
        //its a recursive function to get it to work pass it the tile the players currently on 
        //and the players movement range
        #endregion ZachNotes
        if (tile!=null)
        {
           
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                if (tileshort.Movementvalue>=0&& tileshort.Movementvalue<move)
                {
                    tileshort.Movementvalue = move;
                    if (move>=0)
                    {
                       tileshort.Moveable = true;
                    }
                    #region DicMethod(Broken)
                    //GameObject nexttile = tilemanager.GetTile(new Vector2(tileshort.X - 1, tileshort.Y));
                    //AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    //nexttile = tilemanager.GetTile(new Vector2(tileshort.X + 1, tileshort.Y));
                    //AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    //nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y - 1));
                    //AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    //nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y + 1));
                    //AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    #endregion DicMethod(Broken)
                    GameObject nexttile = tilemanager.GetTile(new Vector2(tileshort.X - 1, tileshort.Y));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X + 1, tileshort.Y));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y - 1));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y + 1));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Movementvalue = -1;
            }
        }
     
        return;
       
    }
    private void TurnOffMovement()
    {
        #region ZachNotes
        //I need this for an invoke it just tells the game the player aint moving
        #endregion ZachNotes
        moving = false;
        
    }
        private void ClearTileMovementValues()
    {
        #region ZachNotes
        //this function clears the movement values of all the tiles
        //this will need to be called whenever new movement ranges are calculated if not it can cause crashes
        //Because of the pathfinding system
        #endregion ZachNotes
        foreach (GameObject tile in tilemanager.Tilearray)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                tileshort.Movementvalue = 0;
            }
        }
    }

    private float MoveToFinalTile(GameObject tile,GameObject playertile)
    {
        #region ZachNotes
        //So this function takes the tile you want to go to and the tile your currently on
        //and will have the player move to that tile
        //It works by starting at the endtile and then using the movement values assigned to those
        //tiles by the assignmovementvalues method moves back to the players starting tile
        //by seeing if there is a tile adjacent to it with a higher movement value
        //(It kinda backwords engineers how to get back to the player) 
        //then it destacks itself pushing the player tile by tile to the destination
        #endregion ZachNotes
        Stack<Vector2> moves=new Stack<Vector2>();
        Tile originpoint = playertile.GetComponent<Tile>();
        Tile tileshort = tile.GetComponent<Tile>();
        moves.Push(tileshort.Loc);
        this.moving = true;
        while (tileshort.X!=originpoint.X ||tileshort.Y!=originpoint.Y)
        {
            #region ZachNotes
            //Creating the Stack
            #endregion ZachNotes
            Tile nexttile = tilemanager.GetTile(new Vector2(tileshort.X - 1, tileshort.Y)).GetComponent<Tile>();
            tileshort=PathChecker(tileshort, nexttile, moves);
            nexttile = tilemanager.GetTile(new Vector2(tileshort.X +1, tileshort.Y)).GetComponent<Tile>();
            tileshort = PathChecker(tileshort, nexttile, moves);
            nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y-1)).GetComponent<Tile>();
            tileshort = PathChecker(tileshort, nexttile, moves);
            nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y + 1)).GetComponent<Tile>();
            tileshort = PathChecker(tileshort, nexttile, moves);
        }
        float i = 0;
        while (moves.Count>0)
        {
            #region ZachNotes
            //So im using a coroutine to do the movement currently he just teleports to spots we will have 
            //to adjust that with animations later
            #endregion ZachNotes
            StartCoroutine(MoveToIndividualTile(moves.Pop(),i));
           
            i += 1;
        }
        return i;
       
    }
    //So this lerp is weird needs work to get the player from point a to be
    //Smooth movement would need to be added  in this MoveToIndividualTile;
    private IEnumerator MoveToIndividualTile(Vector2 tile,float i)
    {
        #region ZachNotes
        //So I tried using a lerp to get the player to each spot smoothly but that didnt work :(
        //this function will queue up a movement by giving it a tile to move to and a time to get there
        //but its abit broken currently. (Works but is ugly)
        #endregion ZachNotes
        yield return new WaitForSeconds(i);
        Debug.Log(tile);
        float timepassed=0;
        Vector3 travelpoint = new Vector3(tilemanager.TileDic[tile].transform.position.x, tilemanager.TileDic[tile].transform.position.y+2, tilemanager.TileDic[tile].transform.position.z);
        while (this.transform.position != travelpoint)
        {
            timepassed += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, travelpoint, timepassed/2);
        }
        
    }
    private Tile PathChecker(Tile tileshort,Tile nexttile,Stack<Vector2> moves)
    {
        #region ZachNotes
        //this checks if a tile has a lower movement value that itself
        //its the main component of the pathfinding system and its used in the 
        //MovetoFinalTile method
        //its adds the tile to the stack
        #endregion ZachNotes
        if (tileshort.Movementvalue < nexttile.GetComponent<Tile>().Movementvalue)
        {
            tileshort = nexttile;
            moves.Push(nexttile.Loc);
        }
        return tileshort;
    }

    //flood fill notes have the base movement value each square will branch out four new tiles till the movement stat is dead

    private void DisplayMovementRange()
    {
        #region ZachNotes
        //This function checks after a movement value has been assigned to a tile if its moveable
        //if it is it gives it a material to indicate so
        #endregion ZachNotes
        foreach (GameObject tile in tilemanager.TileDic.Values)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (tileshort.Movementvalue > 0 && !tileshort.IsSelected)
            {
                tile.GetComponent<MeshRenderer>().material = tile.GetComponent<Tile>().Movemat;
            }
            else if (tileshort.Movementvalue == 0 && !tileshort.IsObstacle && !tileshort.IsSelected)
            {
                tile.GetComponent<MeshRenderer>().material = tile.GetComponent<Tile>().Defaultmat;
            }
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Tile")
        {
            #region ZachNotes
            //these should be on the player stats script cause we may want to disable this script in the future
            //or we can have another check to see when to call the updates but that may be cumbersome
            #endregion ZachNotes
            this.X = other.GetComponent<Tile>().X;
            this.Y = other.GetComponent<Tile>().Y;
            other.GetComponent<Tile>().IsPlayer = true;
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
          other.GetComponent<Tile>().IsPlayer = false;
        }
    }
    void Start()
    {
        tilemanager = tilemanager.GetComponent<TileManager>();
        code = GameManager.instance;
       //move= this.GetComponent<Bears>().Movement;
       // InvokeRepeating("DisplayMovementRange", 0, timer);
    }

    // Update is called once per frame
    void Update()
    {
        //we need a system to say we are moving when we hit the move button and something to say display this bears movement range 
        //not the other bears
        //if (code.MovementPhase && GetComponent<Bears>().Selected)
        //{
            DisplayMovementRange();
            if (!Movementcheck && !moving)
            {
                #region ZachFuckUps
                //GetMovementRange1();
                //for (int i = 0; i < moveList.Count; i++)
                //    tiles.Add(tilemanager.GetTile(new Vector2(moveList[i].x, moveList[i].y)));

                //}
                //int half = tiles.Count / 2;
                //starting tile
                #endregion ZachFuckUps
                GameObject startingtile = tilemanager.TileDic[new Vector2(this.X, this.Y)];
                AssignTileMovementValue(startingtile, move + 1);
                Movementcheck = true;
                //AssignTileMovementValue(tiles[half],move+1);

                ///////TEST//////
                //GameObject testmove = tilemanager.TileDic[pathfindtest];
                //This needs to only happen if you hit move
                //MoveToFinalTile(testmove, startingtile);

            }
            if (executeMovement == true && !moving)
            {
                #region ZachNotes
                //This occurs when a tile has been selected to move to
                #endregion ZachNotes
                float movementTimer;

                GameObject startingtile = tilemanager.TileDic[new Vector2(this.X, this.Y)];
                movementTimer = MoveToFinalTile(MoveDestination, startingtile /*GetComponent<Bears>().Movement*/);
                executeMovement = false;
                //movementcheck = false;
                ClearTileMovementValues();
                Invoke("TurnOffMovement", movementTimer + .1f);

            }
      //  }

    }
}
