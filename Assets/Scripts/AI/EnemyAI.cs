using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    private int moveRange;
   [SerializeField] private int attackRange;
  [SerializeField]  private int x;
   [SerializeField] private int y;
    private Vector2 position;
    private TileManager tileManager;
    [SerializeField]private List<GameObject> tilesInMovementRange=new List<GameObject>();
    private Dictionary<Vector2, Tile> playersInRange = new Dictionary<Vector2, Tile>();
    private bool onlyOnce = false;
    //private List<Vector2> playerPositions;
    private AttackRange atkRangeMethods;
    private SquadSelection squadManager;
    private Vector2 finalAttackTarget;
    [SerializeField] private Vector2 FinalMoveTarget;
    private Movement mover;
    public Dictionary<Vector2, Tile> PlayersInRange { get => playersInRange; set => playersInRange = value; }

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
               
                    GameObject nexttile = tileManager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tileManager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tileManager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                    nexttile = tileManager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                    AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Movementvalue = -1;
            }
        }

        

    }
    
    protected void IndividualRangeCheck(int atkRange,GameObject currentTile)
    {
        atkRangeMethods.AssignDescendingTileAttackRange(currentTile, atkRange);
    }
    void Start()
    {
        atkRangeMethods = GetComponent<AttackRange>();
        tileManager = TileManager.instance;
        code = GameManager.instance;
        moveRange = GetComponent<Bears>().Movement;
        attackRange = GetComponent<AttackRange>().Range;
        squadManager = SquadSelection.instance;
        mover = GetComponent<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            #region ZachNotes
            
            #endregion ZachNotes
            this.x = other.GetComponent<Tile>().X;
            this.y = other.GetComponent<Tile>().Y;
            position = new Vector2(x, y);
            other.GetComponent<Tile>().IsEnemy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (tag == "Player")
        {
            if (other.tag == "Tile")
            {
                other.GetComponent<Tile>().IsEnemy = false;
            }
        }

    }
   

    void Update()
    {
        if (code.CurrPhase==GameManager.Phase.enemyPhase)
        {
            if (!onlyOnce)
            {
                onlyOnce = true;
                GameObject startingtile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingtile, moveRange);
                foreach (GameObject tile in tilesInMovementRange)
                {
                    if (!tile.GetComponent<Tile>().IsPlayer && !tile.GetComponent<Tile>().IsEnemy)
                    {
                        #region ZachNotes
                        //this calculates every space the the enemy can attack
                        #endregion ZachNotes
                        atkRangeMethods.GetAttackRangeIgnoreObstacles(attackRange, tile.GetComponent<Tile>().X, tile.GetComponent<Tile>().Y);
                    }

                }
                foreach (GameObject playertile in squadManager.Squad)
                {
                    #region ZachNotes
                    //this takes the players positions and then sees if they are in range
                    #endregion ZachNotes
                    if (tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponent<Tile>().Attackvalue > 0)
                    {
                        PlayersInRange.Add(playertile.GetComponent<Movement>().Position, tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponent<Tile>());
                    }
                }
                #region LessEfficient
                //           foreach (GameObject tile in tileManager.Tilearray)
                //           {
                //               Tile tileshort = tile.GetComponent<Tile>();
                //               if (tileshort.IsPlayer && tileshort.Attackvalue > 0)
                //               {
                //                   #region ZachNotes
                //                   //then from here it checks if there is a player on a tile an enemy can hit
                //                   #endregion ZachNotes
                ////                   if(playersInRange[tileshort.Loc]==null)
                //                   PlayersInRange.Add(tileshort.Loc, tileshort);

                //                   Debug.Log(tileshort.Loc);
                //               }
                //           }
                #endregion LessEfficient
                if (playersInRange.Count > 0)//only executes if there were players in range
                {
                    int lowesthp = -1;
                    foreach (KeyValuePair<Vector2, Tile> playertile in PlayersInRange)
                    {
                        #region ZachNotes
                        //Tells the enemy to head hunt the weakest bear
                        #endregion ZachNotes

                        for (int i = 0; i < squadManager.Squad.Length; i++)
                        {
                            if (playertile.Value.Loc == squadManager.Squad[i].GetComponent<Movement>().Position)
                            {
                                if (lowesthp == -1)
                                {
                                    lowesthp = squadManager.Squad[i].GetComponent<Bears>().Hp;
                                    finalAttackTarget = playertile.Value.Loc;
                                }
                                else if (squadManager.Squad[i].GetComponent<Bears>().Hp < lowesthp)
                                {
                                    lowesthp = squadManager.Squad[i].GetComponent<Bears>().Hp;
                                    finalAttackTarget = playertile.Value.Loc;
                                }
                            }
                        }
                    }
                    AttackRange.ClearTileAttackValues(tileManager);
                    atkRangeMethods.AssignDescendingTileAttackRange(tileManager.TileDic[finalAttackTarget], attackRange + 1);
                    int furthestblock = 0;
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
                    mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingtile, TileManager.instance);


                }
                else
                {
                    Vector2 playerPos = new Vector2(0, 0);
                    int lowestHp = -1;
                    //     //Im thinking of having the opponent rush down the player so move closest to player with lowest hp
                    foreach (GameObject player in squadManager.Squad)
                    {
                        if (lowestHp == -1)
                        {
                            lowestHp = player.GetComponent<Bears>().Hp;
                            playerPos = player.GetComponent<Movement>().Position;

                        }
                        else if (lowestHp > player.GetComponent<Bears>().Hp)
                        {
                            lowestHp = player.GetComponent<Bears>().Hp;
                            playerPos = player.GetComponent<Movement>().Position;
                        }
                    }
                    int lowestdistance = -1;
                    Vector2 finalMoveTarget = new Vector2(0, 0);
                    foreach (GameObject tile in tilesInMovementRange)
                    {
                        Tile tileshort = tile.GetComponent<Tile>();
                        if (!tileshort.IsEnemy && !tileshort.IsPlayer)
                        {
                            //int diffx = (int)Mathf.Abs(tileshort.X - playerPos.x);
                            //int diffy = (int)Mathf.Abs(tileshort.Y - playerPos.y);
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
                    mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingtile, TileManager.instance);
                }
                Movement.ClearTileMovementValues(tileManager);

            }
        }
}
}
