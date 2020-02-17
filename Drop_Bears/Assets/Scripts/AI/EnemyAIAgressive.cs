using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIAgressive : EnemyAIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (code.EnemyPhase)
        {
            
            if (!onlyOnce)
            {
                onlyOnce = true;
                GameObject startingtile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingtile, moveRange+1);
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
                    atkRangeMethods.ClearTileAttackValues(tileManager);
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
                   timer= mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingtile, TileManager.instance);
                    stats.Attack(finalAttackTarget);
                    Invoke("EndTurn", timer);

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
                   timer= mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingtile, TileManager.instance);
                    Invoke("EndTurn", timer);
                }
                atkRangeMethods.ClearTileAttackValues(tileManager);
                mover.ClearTileMovementValues(tileManager);
               
            }
           // mover.ClearTileMovementValues(tileManager);
        }
    }
}

