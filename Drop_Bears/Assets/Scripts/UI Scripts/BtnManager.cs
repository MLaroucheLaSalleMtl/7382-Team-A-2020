using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{

    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    [SerializeField] private Button btnAttack, btnMove, btnMap, btnEndTurn, btnMelee, btnRange, btnAbility1, btnAbility2;
    [SerializeField] private GameObject SubMenu;
   [SerializeField] private Text text;
    public int x=0;
    public int y=0;
    string info;
    public static BtnManager instance;
    private Color textColor = Color.red;
    public bool AttackIsSelected = false;
    public bool onlyOnce = false;
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
        buttons.Add("0,1", btnAttack);
        buttons.Add("0,0", btnMove);
        buttons.Add("0,-1", btnMap);
        buttons.Add("0,-2", btnEndTurn);
        buttons.Add("1,1", btnMelee);
        buttons.Add("1,0", btnRange);
        buttons.Add("1,-1", btnAbility1);
        buttons.Add("1,-2", btnAbility2);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        buttons["0,-2"].Select();
        btnEndTurn.Select();
    }

    public void OnClickAttack()
    {
        if (AttackIsSelected == false)
        {
            onlyOnce = true;
            AttackIsSelected = true;
        }
        else if (AttackIsSelected == true)
        {
            onlyOnce = true;
            AttackIsSelected = false;
        }
        if(onlyOnce)
        {
            DisplaySubMenu();
        }

    }

    // Update is called once per frame
    void Update()
    {
        BtnSelection();
        GetText();
        text.color = textColor;
        //still need to get the colour to work with the text
        if(onlyOnce)
        {
            DisplaySubMenu();
        }
        
    }

    void BtnSelection()
    {
        OnSelectUp();
        OnSelectSide();
        info = x + "," + y;
        buttons[info].Select();
        GetText();
        //buttons[info].GetComponentInChildren<Text>().color = Color.red;
       text.color = Color.red;
    }

    private void DisplaySubMenu()
    {
        text.color = Color.white;
        if (AttackIsSelected)
        {
            SubMenu.SetActive(true);
            buttons["1,1"].Select();
            x = 1;
            y = 1;
        }
        else
        {
            SubMenu.SetActive(false);
            buttons["0,1"].Select();
            x = 0;
            y = 1;
        }
        onlyOnce = false;
    }

    private void GetText()
    {
        text = buttons[info].GetComponentInChildren<Text>();
    }

    //couldnt figure out how to use the new input system for this
    void OnSelectUp(/*InputAction.CallbackContext context*/)
    {

        if (/*selection.y>0*/Input.GetAxisRaw("Vertical") > 0.1 && Input.GetButtonDown("Vertical"))
        {
            if (y == 1)
            {
                y = -2;
            }
            else
            {
                y += 1;
            }
            text.color = Color.white;
        }
        else if (Input.GetAxisRaw("Vertical") < 0.1 && Input.GetButtonDown("Vertical"))
        {
            if (y == -2)
            {
                y = 1;
            }
            else
            {
                y -= 1;
            }
            text.color = Color.white;
        }
        
    }

    void OnSelectSide()
    {
        if (AttackIsSelected)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.1 && Input.GetButtonDown("Horizontal"))
            {
                if (x == 1)
                {
                    x = 0;
                }
                else
                {
                    x += 1;
                }
                text.color = Color.white;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0.1 && Input.GetButtonDown("Horizontal"))
            {
                if (x == 0)
                {
                    x = 0;
                }
                else
                {
                    x -= 1;
                }
                text.color = Color.white;
            }
            //  text.color = Color.white;
        }
    }

    #region standardButtonStuff
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
    #endregion standardButtonStuff

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
