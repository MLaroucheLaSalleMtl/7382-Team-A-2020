using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiAgressiveV2 : EnemyAIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }

    // Update is called once per frame
    void Update()
    {
       //if(code.Phase=enemyPhase &&TakeTurn)
        if (code.EnemyPhase && TakeTurn )
        {
            if (!OnlyOnce && stats.IsAlive)
            {
                Acting = true;
               GameObject startingTile;
                OnlyOnce = true;
                startingTile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingTile, stats.Movement + 1);
                AssignEnemyAttackSpaces();

                if (Pairs.Count != 0)
                {
                    int lowesthp = Pairs[0].PlayerTile.GetComponentInChildren<Bears>().Hp;
                    Tile tileToAttack = Pairs[0].PlayerTile;
                    Tile tileToMoveTo = Pairs[0].EnemyTile;

                    foreach (AttackTilePairings attackpairs in Pairs)
                    {
                        if (lowesthp > attackpairs.PlayerTile.GetComponentInChildren<Bears>().Hp)
                        {
                            lowesthp = attackpairs.PlayerTile.GetComponentInChildren<Bears>().Hp;
                            tileToAttack = attackpairs.PlayerTile;
                            tileToMoveTo = attackpairs.EnemyTile;
                        }
                    }
                    //if (tileToMoveTo != startingTile.GetComponent<Tile>())
                    //    tileToMoveTo.IsEnemy = true;
                    timer = mover.MoveToFinalTile(tileToMoveTo, startingTile.GetComponent<Tile>(), TileManager.instance);
                   // stats.EnemyAttack(tileToAttack, timer);
                    stats.Attack(tileToAttack);
                    AttackRange.ClearTileAttackValues(tileManager);
                    Movement.ClearTileMovementValues(tileManager);
                    Invoke("EndTurn", timer);
                }
                else if (Pairs.Count==0 && stats.Movement > 0)
                {
                    Vector2 playerPos= FindWeakestPlayerOnMap();
                    FindTileNearWeakestPlayerOnMap(playerPos,startingTile);
                    //if (FinalMoveTarget != null)
                   // tileManager.TileDic[FinalMoveTarget].GetComponent<Tile>().IsEnemy = true;
                   
                    timer = mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingTile, TileManager.instance);
                    AttackRange.ClearTileAttackValues(tileManager);
                    Movement.ClearTileMovementValues(tileManager);
                    Invoke("EndTurn", timer);
                }

            }
            else if (!stats.IsAlive)
            {
                EndTurn();
            }
           

        }
     
    }
}