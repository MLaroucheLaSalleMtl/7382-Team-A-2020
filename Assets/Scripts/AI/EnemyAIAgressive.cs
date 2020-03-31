using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class EnemyAIAgressive : EnemyAIBase
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        GetVariables();
//    }

//    // Update is called once per frame
//    void Update()
//    {
     
//        //So he will only act during enemy phases
//        if (code.EnemyPhase &&TakeTurn)
//        {
//            //I only want him to do his stuff once
//            if (!OnlyOnce)
//            {
//                Acting = true;
//                GameObject startingtile;
//                Tile FinalTile;
//                #region ZachNotes
//                //this section will combine the enemies movement range and attack range together 
//                //to see if there are players in range
//                #endregion ZachNotes
//                OnlyOnce = true;
//                     startingtile = tileManager.TileDic[new Vector2(position.x, position.y)];
               
//                AssignTileMovementValue(startingtile, moveRange + 1);
//              //timer FinalTile= AssignEnemyAttackSpaces(startingtile);
//              //  CheckForPlayersInRange();
              
//                #region LessEfficient
//                //           foreach (GameObject tile in tileManager.Tilearray)
//                //           {
//                //               Tile tileshort = tile.GetComponent<Tile>();
//                //               if (tileshort.IsPlayer && tileshort.Attackvalue > 0)
//                //               {
//                //                   #region ZachNotes
//                //                   //then from here it checks if there is a player on a tile an enemy can hit
//                //                   #endregion ZachNotes
//                ////                   if(playersInRange[tileshort.Loc]==null)
//                //                   PlayersInRange.Add(tileshort.Loc, tileshort);

//                //                   Debug.Log(tileshort.Loc);
//                //               }
//                //           }
//                #endregion LessEfficient
//               // if (playersInRange.Count > 0)//only executes if there were players in range
//               if(FinalTile!=null)
//                {
//                    // FindWeakestPlayerInRange();
//                    ////////ALWAYS CLEAR ATTACK VALUES WHEN THEY ARE NO LONGER IN USE 
//                    ///////SAME THING FOR TILE MOVEMENT VALUES WILL CAUSE CRASHES IF NOT
//                    //FindAttackPosition();                 
//                   timer= mover.MoveToFinalTile(FinalTile, startingtile.GetComponent<Tile>(), TileManager.instance);
//                    finalAttackTarget = GetTileDicToAttack(FinalTile);
//                    //timer = mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingtile, TileManager.instance);
//                    stats.Attack(finalAttackTarget);
//                    AttackRange.ClearTileAttackValues(tileManager);
//                    Movement.ClearTileMovementValues(tileManager);
//                    Invoke("EndTurn", timer+.1f);

//                }
//                //if there are no players in range and the enemy can move go towards a weak bear
//               else if (playersInRange.Count <= 0 &&stats.Movement>0)
//                {
//                    Vector2 playerPos = FindWeakestPlayerOnMap();
//                    FindTileNearWeakestPlayerOnMap(playerPos);
//                   timer= mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingtile, TileManager.instance);
//                    AttackRange.ClearTileAttackValues(tileManager);
//                    Movement.ClearTileMovementValues(tileManager);
//                    Invoke("EndTurn", timer);
//                }
             
               
//            }
          
//        }
//    }
//}

