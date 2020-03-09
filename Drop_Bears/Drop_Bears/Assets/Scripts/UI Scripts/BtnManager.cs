using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{

    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    [SerializeField] private Button btnAbilities, btnMove, btnMap, btnEndTurn, btnAttack, btnAbility1, btnAbility2, btnBack;
    [SerializeField] private GameObject SubMenu;
    [SerializeField] private Text text;
    public int x = 0;
    public int y = 0;
    string info;
    public static BtnManager instance;
    private Color textColor = Color.red;
    public bool AttackIsSelected = false;
    public bool onlyOnce = false;
    private SquadSelection selectedSquad;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        buttons.Add("0,1", btnAbilities);
        buttons.Add("0,0", btnMove);
        buttons.Add("0,-1", btnMap);
        buttons.Add("0,-2", btnEndTurn);
        buttons.Add("1,1", btnAttack);
        buttons.Add("1,0", btnAbility1);
        buttons.Add("1,-1", btnAbility2);
        buttons.Add("1,-2", btnBack);


    }
    // Start is called before the first frame update
    void Start()
    {
        buttons["0,-2"].Select();
        btnEndTurn.Select();
        selectedSquad = SquadSelection.instance;
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
        if (onlyOnce)
        {
            DisplaySubMenu();
        }

    }
    Color DecideColor()
    {
        return selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().BearRace;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGamePaused == false)
        {
            //Debug.Log(x + "," + y);
            AbilityDescriptions();
            BtnSelection();
            GetText();
            text.color = DecideColor();
            //still need to get the colour to work with the text
            if (onlyOnce)
            {
                DisplaySubMenu();
            }
            if (Input.GetButtonDown("Back") && AttackIsSelected)
            {
                OnClickAttack();
            }
        }
    }

    void BtnSelection()
    {

        OnSelectUp();
        //OnSelectSide();
        info = x + "," + y;
        if(buttons.ContainsKey(info))
        buttons[info].Select();
        GetText();
        //buttons[info].GetComponentInChildren<Text>().color = Color.red;
        text.color = Color.red;      
    }


    [SerializeField] private GameObject[] attackDes;
    [SerializeField] private GameObject[] supportDes; //Ability 1
    [SerializeField] private GameObject[] specialDes; //Ability 2
    public void DisableAbilityDescription()
    {
        attackDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
        supportDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
        specialDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
    }
    void AbilityDescriptions()
    {
        if (info == 1 + "," + 1)
        {
            attackDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(true);
            //Deactivates Other Panels
            supportDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
            specialDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
        }
        else if (info == 1 + "," + 0)
        {
            supportDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(true);
            //Deactivates Other Panels
            attackDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
            specialDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
        }
        else if (info == 1 + "," + -1)
        {
            specialDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(true);
            //Deactivates Other Panels
            attackDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
            supportDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
        }
        else
        {
            attackDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
            supportDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
            specialDes[selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().avatarNumber].SetActive(false);
        }
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
        if(buttons.ContainsKey(info))
        text = buttons[info].GetComponentInChildren<Text>();
    }

    //couldnt figure out how to use the new input system for this
    public void Selection(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 navigate = context.ReadValue<Vector2>();
            //x = x + (int)navigate.x;
            y = y + (int)navigate.y;
            if(y == 2)
            {
                y = -2;
            }
            Debug.Log(x + ", " + y);
        }
    }


    void OnSelectUp(/*/*InputAction.CallbackContext context*/)
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
    #region NianMistakes
    //void OnSelectSide()
    //{
    //    if (AttackIsSelected)
    //    {
    //        if (Input.GetAxisRaw("Horizontal") > 0.1f && Input.GetButtonDown("Horizontal") && y != -2f)
    //        {
    //            if (x == 1)
    //            {
    //                x = 0;
    //            }
    //            else
    //            {
    //                x += 1;
    //            }
    //            text.color = Color.white;
    //        }
    //        if (Input.GetAxisRaw("Horizontal") > 0.1f && Input.GetButtonDown("Horizontal") && y == -2f)
    //        {
    //            y = -1;
    //            x = 1;

    //            text.color = Color.white;
    //        }
    //        else if (Input.GetAxisRaw("Horizontal") < -0.1f && Input.GetButtonDown("Horizontal"))
    //        {
    //            if (x == 0)
    //            {
    //                x = 1;
    //            }
    //            else
    //            {
    //                x -= 1;
    //            }
    //            text.color = Color.white;
    //        }
    //        //  text.color = Color.white;
    //    }
    //}
    #endregion NianMistakes

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
