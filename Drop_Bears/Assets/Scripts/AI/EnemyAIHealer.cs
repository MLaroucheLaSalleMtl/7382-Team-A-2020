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

    // Update is called once per frame
    void Update()
    {
        if(code.EnemyPhase)
        {
            if(!OnlyOnce)
            {
                OnlyOnce = true;
                GameObject startingtile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingtile, moveRange);
            }
        }
    }
}
