using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquadSelection : MonoBehaviour
{
    #region Singleton
    GameManager code;
    public static SquadSelection instance=null;
    private void Awake()
    {
        Squad = GameObject.FindGameObjectsWithTag("Player");
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion Singleton

    [SerializeField] private GameObject[] squad; //Manages The Squad
    private int selected=0; //Know which character is selected 

    //Can Be Replaced Later 
    //This is just a bool to know the turn
    [SerializeField] private bool playerTurn = false;
    private int playersAlive;
    public GameObject[] Squad { get => squad; set => squad = value; }
    public int Selected { get => selected; set => selected = value; }
    public int PlayersAlive { get => playersAlive; set => playersAlive = value; }
    private int x = 0;

    private void DisableHighLights(int totalPlayers)
    {
        for (int i=0;i<totalPlayers;i++)
        {
            Squad[i].GetComponent<Light>().enabled = false;

        }
    }


    void Start()
    {
        #region ZachNotes
        //so this is here if you dont want to manually assign the players in the inspector
        #endregion ZachNotes
        Squad = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in Squad)
        {
            PlayersAlive++;
        }
        code = GameManager.instance;
        DisableHighLights(Squad.Length);       
    }

   void HighlightCharacter(int totalPlayers,int selectedPlayer)
    {
        for (int i=0; i < totalPlayers; i++)
        {
            if (i == selectedPlayer)
            {
                
                Squad[i].GetComponent<Light>().enabled = true;
                Squad[i].GetComponent<Bears>().Selected = true;
                
            }
            else
            {
                Squad[i].GetComponent<Light>().enabled = false;
                Squad[i].GetComponent<Bears>().Selected = false;
            }
        }
   }

    public void OnSquadSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 navigate = context.ReadValue<Vector2>();
            x += (int)navigate.x;
        }
        
    }

    void Update()
    {
        
        //also ya you can only swap selection during the menu phase
        if(code.CurrPhase==GameManager.Phase.menuPhase && !BtnManager.instance.AttackIsSelected)
        {
            if (/*Input.GetAxisRaw("Horizontal") > 0 && Input.GetButtonDown("Horizontal")*/x > 0 && !PauseMenu.isGamePaused)
            {
                do
                {
                    if (Selected + 1 >= squad.Length)
                    {
                        Selected = 0;
                    }
                    else
                        Selected++;
                } while (!Squad[selected].GetComponent<Bears>().IsAlive || squad[selected].GetComponent<Bears>().TurnComplete);
                Debug.Log("Selected = " + Selected);
                x = 0;
            }
            if (/*Input.GetAxisRaw("Horizontal") < 0 && Input.GetButtonDown("Horizontal")*/x < 0 && !PauseMenu.isGamePaused)
            {

                do
                {
                    if (Selected - 1 < 0)
                    {
                        Selected = squad.Length - 1;
                    }
                    else
                        Selected--;
                } while (!Squad[selected].GetComponent<Bears>().IsAlive || squad[selected].GetComponent<Bears>().TurnComplete);
                Debug.Log("Selected = " + Selected);
                x = 0;
            }
            if (!Squad[selected].GetComponent<Bears>().IsAlive)
            {
                do
                {
                    if (selected >= squad.Length)
                    {
                        selected = 0;
                    }
                    selected++;

                } while (squad[selected].GetComponent<Bears>().IsAlive);
            }

            //I changed your if statements to the loop function it works the same I also added so that 
            //it tells the bear script that it is selected
            HighlightCharacter(Squad.Length, Selected);

            //Enables the highlight depending on the index.
            //We can add the different menus for the character and such to the code later
            #region Highlights

            //if (selected == 0)
            //{
            //    //Debug.Log("Selected = 0");
            //    squad[0].GetComponent<Light>().enabled = true;
            //    squad[1].GetComponent<Light>().enabled = false;
            //    squad[2].GetComponent<Light>().enabled = false;
            //    squad[3].GetComponent<Light>().enabled = false;
            //    squad[4].GetComponent<Light>().enabled = false;


            //}
            //if (selected == 1)
            //{
            //   // Debug.Log("Selected = 1");
            //    squad[0].GetComponent<Light>().enabled = false;
            //    squad[1].GetComponent<Light>().enabled = true;
            //    squad[2].GetComponent<Light>().enabled = false;
            //    squad[3].GetComponent<Light>().enabled = false;
            //    squad[4].GetComponent<Light>().enabled = false;
            //}
            //if (selected == 2)
            //{
            //    //Debug.Log("Selected = 2");
            //    squad[0].GetComponent<Light>().enabled = false;
            //    squad[1].GetComponent<Light>().enabled = false;
            //    squad[2].GetComponent<Light>().enabled = true;
            //    squad[3].GetComponent<Light>().enabled = false;
            //    squad[4].GetComponent<Light>().enabled = false;
            //}
            //if (selected == 3)
            //{
            //    //Debug.Log("Selected = 3");
            //    squad[0].GetComponent<Light>().enabled = false;
            //    squad[1].GetComponent<Light>().enabled = false;
            //    squad[2].GetComponent<Light>().enabled = false;
            //    squad[3].GetComponent<Light>().enabled = true;
            //    squad[4].GetComponent<Light>().enabled = false;
            //}
            //if (selected == 4)
            //{
            //    //Debug.Log("Selected = 4");
            //    squad[0].GetComponent<Light>().enabled = false;
            //    squad[1].GetComponent<Light>().enabled = false;
            //    squad[2].GetComponent<Light>().enabled = false;
            //    squad[3].GetComponent<Light>().enabled = false;
            //    squad[4].GetComponent<Light>().enabled = true;
            //}
            #endregion Highlights
        }
        else if (playerTurn && code.CurrPhase==GameManager.Phase.menuPhase && PlayersAlive > 0 && PauseMenu.isGamePaused)
        {
            if (squad[selected].GetComponent<Bears>().TurnComplete)
            {
                BtnManager.instance.DisableAbilityDescription();
                BtnManager.instance.OnClickAttack();
              
                do
                {
                    if (Selected + 1 >= squad.Length)
                    {
                        Selected = 0;
                    }
                    else
                        Selected++;
                } while (!Squad[selected].GetComponent<Bears>().IsAlive || squad[selected].GetComponent<Bears>().TurnComplete);
            }
        }
        x = 0;
    }
}
