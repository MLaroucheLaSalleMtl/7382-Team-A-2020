using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDecisionTree : MonoBehaviour
{
    public bool isPlayerInRange = false;
    public bool canAttack = false;
    private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInRange = CheckIfPlayerIsThere();
        if(enemyAI.PlayersInRange==null)
        {
            //Need Method to move enemy to player
        }
        else if(enemyAI.PlayersInRange != null)
        {
            if(canAttack)
            {
                //attack the player
            }
            else
            {
                //end his turn
            }
        }
    }

    public bool CheckIfPlayerIsThere()
    {
        //check if player is in the range of the melee enemy so that he can attack
        return false;
    }
}
