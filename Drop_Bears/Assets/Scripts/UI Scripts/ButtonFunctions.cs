using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameManager code;
    [SerializeField]private bool selected = false;

    public bool Selected { get => selected; set => selected = value; }

    void Start()
    {
        code = GameManager.instance;
    }
   public void SwitchToMovement()
    {
        code.MovementPhase = true;
        code.MenuPhase = false;
    }
    public void SwitchToAttack()
    {
        code.AttackPhase = true;
        code.MenuPhase = false;
    }
   
    // Update is called once per frame
    
}
