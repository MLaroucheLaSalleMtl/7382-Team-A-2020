using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{

   [SerializeField] private List<AttackTilePairings> pairs = new List<AttackTilePairings>();
    protected bool getstats = false;
    protected GameManager code;
    
    [SerializeField] protected int attackRange;
    [SerializeField] protected int x;
    [SerializeField] protected int y;
    protected Vector2 position;
    protected TileManager tileManager;
    [SerializeField] protected List<GameObject> tilesInMovementRange = new List<GameObject>();
    protected Dictionary<Vector2, Tile> playersInRange = new Dictionary<Vector2, Tile>();
    private bool onlyOnce = false;
    //private List<Vector2> playerPositions;
    protected AttackRange atkRangeMethods;
    protected SquadSelection squadManager;
   [SerializeField] protected Tile finalAttackTarget;
    [SerializeField] private Vector2 finalMoveTarget;
   protected bool getvariables=false;
    protected Movement mover;
    [SerializeField] private bool turnCompleted = false;
    protected float timer;
    protected Bears stats;
    private bool acting=false;
    [SerializeField] private bool takeTurn = false;
    public Dictionary<Vector2, Tile> PlayersInRange { get => playersInRange; set => playersInRange = value; }
    public bool TakeTurn { get => takeTurn; set => takeTurn = value; }
    public bool OnlyOnce { get => onlyOnce; set => onlyOnce = value; }
    public bool TurnCompleted { get => turnCompleted; set => turnCompleted = value; }
    public bool Acting { get => acting; set => acting = value; }
    public List<AttackTilePairings> Pairs { get => pairs; set => pairs = value; }
    protected Vector2 FinalMoveTarget { get => finalMoveTarget; set => finalMoveTarget = value; }

    protected void ClearArrays()
    {
        tilesInMovementRange.Clear();
        playersInRange.Clear();
        pairs.Clear();
    }
    protected void EndTurn()
    {
        AttackRange.ClearTileAttackValues(tileManager);
        Movement.ClearTileMovementValues(tileManager);
        ClearArrays();
        Acting = false;
        TurnCompleted = true;
     
    }
    //protected Tile AssignEnemyAttackSpaces(GameObject startingtile)
    //{
    //    Tile tiletogoto=null;
    //    foreach (GameObject tile in tilesInMovementRange)
    //    {
    //        if (!tile.GetComponent<Tile>().IsPlayer && !tile.GetComponent<Tile>().IsEnemy)
    //        {
    //            #region ZachNotes
    //            //this calculates every space the the enemy can attack
    //            #endregion ZachNotes
    //            if(tiletogoto==null)
    //            tiletogoto=atkRangeMethods.GetAttackRangeIgnoreObstaclesMovementEnemies(attackRange, tile.GetComponent<Tile>());
    //        }

    //    }
    //    return tiletogoto;
    //}
    protected void CheckForWeakestPlayerInRange()
    {

    }
   protected void AssignEnemyAttackSpaces ()
    {
        foreach (GameObject tile in tilesInMovementRange)
        {
            atkRangeMethods.GetAttackRangeIgnoreObstaclesEnemies(stats.Range, tile.GetComponent<Tile>(), this);
        }
    }
 
    protected void CheckForPlayersInRange()
    {
        foreach (GameObject playertile in squadManager.Squad)
        {
            #region ZachNotes
            //this takes the players positions and cross refernces them with the enemies attack range
            #endregion ZachNotes
            if (tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponent<Tile>().Attackvalue > 0&&tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponentInChildren<Bears>().IsAlive)
            {
                PlayersInRange.Add(playertile.GetComponent<Movement>().Position, tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponent<Tile>());
            }
        }
    }

    protected void FindAttackPosition()
    {
        AttackRange.ClearTileAttackValues(tileManager);
        atkRangeMethods.AssignDescendingTileAttackRange(finalAttackTarget, attackRange + 1);
        int furthestblock = 0;
        #region ZachNotes
        //So this section has the enemy move to his max range to attack the player instead of min range.
        //basically earlier I assigned from the player attack values to blocks
        //I just tell the enemy go to the block with the lowest attack value in your movement range
        #endregion ZachNotes
        foreach (GameObject tile in tilesInMovementRange)
        {
            if (tile.GetComponent<Tile>().Attackvalue > 0 && !tile.GetComponent<Tile>().IsEnemy && !tile.GetComponent<Tile>().IsPlayer)
            {
                if (furthestblock == 0)
                {
                    furthestblock = tile.GetComponent<Tile>().Attackvalue;
                    FinalMoveTarget = tile.GetComponent<Tile>().Loc;
                }
                else if (furthestblock > tile.GetComponent<Tile>().Attackvalue)
                {
                    furthestblock = tile.GetComponent<Tile>().Attackvalue;
                    FinalMoveTarget = tile.GetComponent<Tile>().Loc;
                }
            }
        }
        
    }
    protected Vector2 FindWeakestPlayerOnMap()
    {
        Vector2 playerPos = new Vector2();
        int lowestHp = -1;
        //Im thinking of having the opponent rush down the player so move closest to player with lowest hp
        foreach (GameObject player in squadManager.Squad)
        {
            #region ZachNotes
            //checks all players for lowest hp takes that position
            #endregion ZachNotes
            if (lowestHp == -1 &&player.GetComponent<Bears>().IsAlive)
            {
                lowestHp = player.GetComponent<Bears>().Hp;
                playerPos = player.GetComponent<Movement>().Position;

            }
            else if (lowestHp > player.GetComponent<Bears>().Hp && player.GetComponent<Bears>().IsAlive)
            {
                lowestHp = player.GetComponent<Bears>().Hp;
                playerPos = player.GetComponent<Movement>().Position;
            }
        }
        return playerPos;
    }
    protected void FindTileNearWeakestPlayerOnMap(Vector2 playerPos,GameObject startingtile)
    {

        int lowestdistance = -1;
        Vector2 finalMoveTarget = startingtile.GetComponent<Tile>().Loc;
        foreach (GameObject tile in tilesInMovementRange)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsEnemy && !tileshort.IsPlayer)
            {
                #region ZachNotes
                //now i check all the tile in the bears movement range that arent occupied
                //And i see how far they are to the weakest bear
                //I then so go to the closest tile
                #endregion ZachNotes
                int distance = ((int)Mathf.Abs(tileshort.X - playerPos.x)) + ((int)Mathf.Abs(tileshort.Y - playerPos.y));
                if (lowestdistance == -1)
                {
                    lowestdistance = distance;
                    FinalMoveTarget = tileshort.Loc;
                }
                else if (lowestdistance > distance)
                {
                    lowestdistance = distance;
                    FinalMoveTarget = tileshort.Loc;
                }
            }
        }
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
                    if (move >= 0&&!tileshort.IsPlayer&&!tileshort.IsEnemy)
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
