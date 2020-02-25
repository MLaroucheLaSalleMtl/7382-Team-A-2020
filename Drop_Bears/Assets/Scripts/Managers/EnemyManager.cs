using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyManager instance = null;
   [SerializeField] private GameObject[] enemies;
 [SerializeField]   private GameManager code;
    [SerializeField] private EnemyAIBase test;
    private GameObject actingEnemy;
    private bool onlyOnce = false;

    public GameObject ActingEnemy { get => actingEnemy; set => actingEnemy = value; }
    #region Singleton
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }       
    }
     IEnumerator WaitForTurn()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<EnemyAIBase>().Acting)
            {
                actingEnemy = enemies[i];
            }
            if (enemies[i].GetComponent<EnemyAIBase>().TurnCompleted == true && enemies.Length>i+1)
            {
                enemies[i + 1].GetComponent<EnemyAIBase>().TakeTurn = true;
            }
        }
        if(enemies[enemies.Length-1].GetComponent<EnemyAIBase>().TurnCompleted==true)
        {
            for(int i=0;i<enemies.Length;i++)
            {
                enemies[i].GetComponent<EnemyAIBase>().TakeTurn = false;
                enemies[i].GetComponent<EnemyAIBase>().OnlyOnce = false;
                enemies[i].GetComponent<EnemyAIBase>().TurnCompleted = false;
            }
            code.EnemyPhase = false;
            code.MenuPhase = true;
            code.PlayerTurn = true;
            
        }
        yield return new WaitForSeconds(.1f);
    }
    #endregion Singleton
    // Update is called once per frame
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        test = enemies[0].GetComponent<EnemyAIBase>();
        
    }
    void Update()
    {
        if (code.EnemyPhase )
        {
            enemies[0].GetComponent<EnemyAIBase>().TakeTurn = true;
            StartCoroutine(WaitForTurn());
        }
    }
}
