using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHealer : EnemyAIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }
    private bool healself = false;
    // Update is called once per frame
    void Update()
    {
        if(code.CurrPhase==GameManager.Phase.enemyPhase && TakeTurn)
        {
            if(!OnlyOnce && stats.IsAlive)
            {
                Acting = true;
                OnlyOnce = true;
             
                GameObject startingTile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingTile, stats.Movement+1);
                AssignEnemyAttackSpacesHealer();
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
                    if (tileToAttack.GetComponentInChildren<EnemyAI>() == null)
                    {
                        healself = true;
                    }
                    timer = mover.MoveToFinalTile(tileToMoveTo, startingTile.GetComponent<Tile>(), TileManager.instance);
                    if (!healself)
                        StartCoroutine(EnemyHeal(timer + .5f, tileToAttack));
                    else
                    {
                        StartCoroutine(SelfHeal(timer + .5f));
                        healself = false;
                    }
                        // stats.Attack(tileToAttack);
                        tileManager.TileDic[FinalMoveTarget].GetComponent<Tile>().IsEnemy = true;
                        AttackRange.ClearTileAttackValues(tileManager);
                        Movement.ClearTileMovementValues(tileManager);
                        //this would need to be commented
                        Invoke("EndTurn", timer + .5f);
                    
                }
                else 
                {
                    StartCoroutine(SelfHeal(timer));
                    Invoke("EndTurn", timer + .5f);
                }
              
            }
            else if (!stats.IsAlive)
            {
                EndTurn();
            }

        }
        
    }
}
