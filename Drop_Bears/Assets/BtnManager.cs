using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string,Button> buttons;
    [SerializeField] private Button btnAttack, btnMove, btnEndTurn;
    int x=0;
    int y=0;
    // Start is called before the first frame update
    void Start()
    {
        buttons.Add("0,0", btnMove);
        buttons.Add("0,1", btnAttack);
        buttons.Add("0,-1", btnEndTurn);
        buttons["0,0"].Select();
    }

    // Update is called once per frame
    void Update()
    {
        string info = x + "," + y;
        buttons[info].Select();
        
    }

    void OnSelectUp(InputAction.CallbackContext context)
    {
        Vector2 selection = context.ReadValue<Vector2>();
        if(selection.y>0)
        {
            if(y==1)
            {
                y = 1;
            }
            else
            {
                y += 1;
            }
        }
    }

    void OnSelectDown(InputAction.CallbackContext context)
    {
        Vector2 selection = context.ReadValue<Vector2>();
        if (selection.y > 0)
        {
            if (y == -1)
            {
                y = -1;
            }
            else
            {
                y -= 1;
            }
        }
    }
}
