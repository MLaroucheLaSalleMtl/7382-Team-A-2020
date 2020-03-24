﻿using System.Collections;
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
        if (code.CurrPhase==GameManager.Phase.enemyPhase && TakeTurn )
        {
            if (!OnlyOnce && stats.IsAlive)
            {
                Acting = true;
               GameObject startingTile;
                OnlyOnce = true;
                startingTile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingTile, stats.Movement + 1);
                AssignEnemyAttackSpaces();

                if (Pairs.Count != 0 &&tilesInMovementRange.Count!=0)
                {

                    FindWeakestPlayerInRange();
                    timer = mover.MoveToFinalTile(tileToMoveTo, startingTile.GetComponent<Tile>(), TileManager.instance);
                   StartCoroutine( EnemyAttackEnum(timer+.5f, tileToAttack));
                    tileManager.TileDic[FinalMoveTarget].GetComponent<Tile>().IsEnemy = true;
                    AttackRange.ClearTileAttackValues(tileManager);
                    Movement.ClearTileMovementValues(tileManager);
                    //this would need to be commented
                    Invoke("EndTurn", timer+.5f);
                }
                else if (Pairs.Count==0 && stats.Movement > 0)
                {
                    Vector2 playerPos = FindWeakestOnMap(PlayerFind);
                    FindTileNearWeakestPlayerOnMap(playerPos,startingTile);                
                    timer = mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingTile, TileManager.instance);
                    tileManager.TileDic[FinalMoveTarget].GetComponent<Tile>().IsEnemy = true;
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