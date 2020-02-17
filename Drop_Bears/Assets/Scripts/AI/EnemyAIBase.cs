using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    protected GameManager code;
    protected int moveRange;
    [SerializeField] protected int attackRange;
    [SerializeField] protected int x;
    [SerializeField] protected int y;
    protected Vector2 position;
    protected TileManager tileManager;
    [SerializeField] protected List<GameObject> tilesInMovementRange = new List<GameObject>();
    protected Dictionary<Vector2, Tile> playersInRange = new Dictionary<Vector2, Tile>();
    protected bool onlyOnce = false;
    //private List<Vector2> playerPositions;
    protected AttackRange atkRangeMethods;
    protected SquadSelection squadManager;
   [SerializeField] protected Vector2 finalAttackTarget;
    [SerializeField] protected Vector2 FinalMoveTarget;
    protected Movement mover;
    [SerializeField] protected bool turnCompleted=false;
    protected float timer;
    protected Bears stats;
    public Dictionary<Vector2, Tile> PlayersInRange { get => playersInRange; set => playersInRange = value; }
    protected void EndTurn()
    {
        turnCompleted = true;
    }
    public void AssignTileMovementValue(GameObject tile, int move)
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
        if (tile != null)
        {

            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                if (tileshort.Movementvalue >= 0 && tileshort.Movementvalue < move)
                {
                    tileshort.Movementvalue = move;
                    if (move >= 0)
                    {
                        tilesInMovementRange.Add(tile);
                        tileshort.Moveable = true;
                    }

                    GameObject nexttile = tileManager.GetTile(new Vector2(tileshort.X - 1, tileshort.Y));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tileManager.GetTile(new Vector2(tileshort.X + 1, tileshort.Y));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tileManager.GetTile(new Vector2(tileshort.X, tileshort.Y - 1));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tileManager.GetTile(new Vector2(tileshort.X, tileshort.Y + 1));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Movementvalue = -1;
            }
        }



    }

    protected void IndividualRangeCheck(int atkRange, GameObject currentTile)
    {
        atkRangeMethods.AssignDescendingTileAttackRange(currentTile, atkRange);
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            #region ZachNotes

            #endregion ZachNotes
            this.x = other.GetComponent<Tile>().X;
            this.y = other.GetComponent<Tile>().Y;
            position = new Vector2(x, y);
            other.GetComponent<Tile>().IsEnemy = true;
            transform.parent = other.transform;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
       
            if (other.tag == "Tile")
            {
                other.GetComponent<Tile>().IsEnemy = false;
            }
        

    }
    // Start is called before the first frame update

    protected void GetVariables()
    {
        atkRangeMethods = GetComponent<AttackRange>();
        tileManager = TileManager.instance;
        code = GameManager.instance;
        moveRange = GetComponent<Bears>().Movement;
        attackRange = GetComponent<AttackRange>().Range;
        squadManager = SquadSelection.instance;
        mover = GetComponent<Movement>();
        stats = GetComponent<Bears>();
    }
    void Start()
    {
       // GetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
