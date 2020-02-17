using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{
    [SerializeField] private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    [SerializeField] private Button btnAttack, btnMove, btnEndTurn;
    int x=0;
    int y=0;
    public static BtnManager instance;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }

        buttons.Add("0,0", btnMove);
        buttons.Add("0,1", btnAttack);
        buttons.Add("0,-1", btnEndTurn);
        buttons["0,0"].Select();
        btnMove.Select();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        buttons["0,0"].Select();
        btnMove.Select();
    }

    // Update is called once per frame
    void Update()
    {
        OnSelectUp();
        string info = x + "," + y;
        buttons[info].Select();
        
    }
    //couldnt figure out how to use the new input system for this
    void OnSelectUp(/*InputAction.CallbackContext context*/)
    {

        if (/*selection.y>0*/Input.GetAxisRaw("Vertical") > 0 && Input.GetButtonDown("Vertical"))
        {
            if (y == 1)
            {
                y = -1;
            }
            else
            {
                y += 1;
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetButtonDown("Vertical"))
        {
            if (y == -1)
            {
                y = 1;
            }
            else
            {
                y -= 1;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Selectable>().Select();
       
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
        GetComponent<Selectable>().OnPointerExit(null);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.Invoke();
            Input.ResetInputAxes();
        }
    }
   
    //void OnSelectDown(InputAction.CallbackContext context)
    //{
    //    Vector2 selection = context.ReadValue<Vector2>();
    //    if (selection.y > 0)
    //    {
    //        if (y == -1)
    //        {
    //            y = -1;
    //        }
    //        else
    //        {
    //            y -= 1;
    //        }
    //    }
    //}
}
