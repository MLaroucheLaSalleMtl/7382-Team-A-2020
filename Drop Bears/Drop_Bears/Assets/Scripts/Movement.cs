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
    [SerializeField]private List<GameObject> tiles = new List<GameObject>();
    private bool movementcheck = false;
    private bool moving;
    [SerializeField]private Vector2 pathfindtest;
    public int Move { get => move; set => move = value; }
    //this gets all the tiles moveable to not accounting for obstacles
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

       if (tile!=null)
        {
            moving = true;
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
        moving = false;
        return;
       
    }
    //This will determine how the character will move to the final tile 
    private void MoveToFinalTile(GameObject tile,GameObject playertile)
    {
        Stack<Vector2> moves=new Stack<Vector2>();
        Tile originpoint = playertile.GetComponent<Tile>();
        Tile tileshort = tile.GetComponent<Tile>();
        moves.Push(tileshort.Loc);
        moving = true;
        while (tileshort.X!=originpoint.X ||tileshort.Y!=originpoint.Y)
        {
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
            
            StartCoroutine(MoveToIndividualTile(moves.Pop(),i));
           
            i += 2;
        }
        moving = false;
    }
    //So this lerp is weird needs work to get the player from point a to be
    //Smooth movement would need to be added  in this MoveToIndividualTile;
    private IEnumerator MoveToIndividualTile(Vector2 tile,float i)
    {
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

        foreach(GameObject tile in tilemanager.TileDic.Values)
        {
            if(tile.GetComponent<Tile>().Movementvalue>0)
            {
                tile.GetComponent<MeshRenderer>().material = tile.GetComponent<Tile>().Movemat;
            }
            else if(tile.GetComponent<Tile>().Movementvalue == 0)
            {
                tile.GetComponent<MeshRenderer>().material = tile.GetComponent<Tile>().Defaultmat;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Tile")
        {
            this.x = other.GetComponent<Tile>().X;
            this.y = other.GetComponent<Tile>().Y;
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
    }

    // Update is called once per frame
    void Update()
    {

        //if (selected==true&&!Moving) we will need a selection system
        if (movementcheck==false&&!moving)
        {
            #region ZachFuckUps
            //GetMovementRange1();
            //for (int i = 0; i < moveList.Count; i++)
            //    tiles.Add(tilemanager.GetTile(new Vector2(moveList[i].x, moveList[i].y)));

            //}
            //int half = tiles.Count / 2;
            //starting tile
            #endregion ZachFuckUps
            GameObject startingtile = tilemanager.TileDic[new Vector2(this.x, this.y)];
            AssignTileMovementValue(startingtile, move+1);
            movementcheck = true;
            //AssignTileMovementValue(tiles[half],move+1);
            DisplayMovementRange();
            ///////TEST//////
            GameObject testmove = tilemanager.TileDic[pathfindtest];
            MoveToFinalTile(testmove, startingtile);

        }
       
    }
}
