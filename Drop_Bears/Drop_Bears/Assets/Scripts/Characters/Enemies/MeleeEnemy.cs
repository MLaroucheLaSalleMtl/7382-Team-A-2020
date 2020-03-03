using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : MonoBehaviour, IBear
{
    #region UnitNotes
    //basic melee Enemy for game
    #endregion UnitNotes
    #region Stats
    int hp = 80;
    int totalHp = 80;
    int defense = 5;
    int attackStrength = 20;
    int movement = 3;
    int attackRange = 1;
    Color bearRace = Color.white;
    #endregion Stats

    public static MeleeEnemy instance=null;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Movement { get => movement; set => movement = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }

    public void MeleeAttack()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability1(Bears Target)
    {
        
    }

    public void Ability2(Bears Target)
    {
        
    }
}
