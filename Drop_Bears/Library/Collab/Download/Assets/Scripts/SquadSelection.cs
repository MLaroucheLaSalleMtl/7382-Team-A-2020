using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelection : MonoBehaviour
{
    #region Singleton
    public static SquadSelection instance;
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
    }
    #endregion Singleton

    [SerializeField] private GameObject[] squad; //Manages The Squad
    private int selected; //Know which character is selected 

    //Can Be Replaced Later 
    //This is just a bool to know the turn
    [SerializeField] private bool playerTurn = false;
    
    //Disables the character highlights
    //Will probably need to add different functions for the menus and such later
    private void DisableHighLights()
    {
        squad[0].GetComponent<Light>().enabled = false;
        squad[1].GetComponent<Light>().enabled = false;
        squad[2].GetComponent<Light>().enabled = false;
        squad[3].GetComponent<Light>().enabled = false;
        squad[4].GetComponent<Light>().enabled = false;
    }

    
    void Start()
    {
        DisableHighLights();       
    }

    //HighlightCharacter(int totalPlayers,int selectedPlayer)
    //{
    //    for (int i; i < totalPlayers; i++)
    //    {
    //        if (i == selectedPlayer)
    //        {
    //  Debug.Log("Selected = " + selectedPlayer);
    //            squad[i].]GetComponent<Light>().enabled = true;
    //        }
    //        else
    //            squad[i].GetComponent<Light>().enabled = false;
    //    }
    //}

    void Update()
    {
        if(playerTurn)
        {

            //Change the controls to input system, getkeydown is only a place holder
            if (Input.GetKeyDown(KeyCode.RightArrow)) // input.getaxisraw("Horizontal") > 0)
            {
                selected++;
                // selected = Mathf.Min(selected, 4); //Limits the selection to 4
                if (selected > 4)
                {
                    selected = 0;
                }

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))// input.getaxisraw("Horizontal") < 0)
            {
               selected--;
               //selected = Mathf.Max(selected, 0); //Limits the selection to 0
               if (selected < 0)
               {
                  selected = 4;
               }
            }
           

            //Enables the highlight depending on the index.
            //We can add the different menus for the character and such to the code later
            #region Highlights
            if (selected == 0)
            {
                //Debug.Log("Selected = 0");
                squad[0].GetComponent<Light>().enabled = true;
                squad[1].GetComponent<Light>().enabled = false;
                squad[2].GetComponent<Light>().enabled = false;
                squad[3].GetComponent<Light>().enabled = false;
                squad[4].GetComponent<Light>().enabled = false;
                
            }
            if (selected == 1)
            {
               // Debug.Log("Selected = 1");
                squad[0].GetComponent<Light>().enabled = false;
                squad[1].GetComponent<Light>().enabled = true;
                squad[2].GetComponent<Light>().enabled = false;
                squad[3].GetComponent<Light>().enabled = false;
                squad[4].GetComponent<Light>().enabled = false;
            }
            if (selected == 2)
            {
                //Debug.Log("Selected = 2");
                squad[0].GetComponent<Light>().enabled = false;
                squad[1].GetComponent<Light>().enabled = false;
                squad[2].GetComponent<Light>().enabled = true;
                squad[3].GetComponent<Light>().enabled = false;
                squad[4].GetComponent<Light>().enabled = false;
            }
            if (selected == 3)
            {
                //Debug.Log("Selected = 3");
                squad[0].GetComponent<Light>().enabled = false;
                squad[1].GetComponent<Light>().enabled = false;
                squad[2].GetComponent<Light>().enabled = false;
                squad[3].GetComponent<Light>().enabled = true;
                squad[4].GetComponent<Light>().enabled = false;
            }
            if (selected == 4)
            {
                //Debug.Log("Selected = 4");
                squad[0].GetComponent<Light>().enabled = false;
                squad[1].GetComponent<Light>().enabled = false;
                squad[2].GetComponent<Light>().enabled = false;
                squad[3].GetComponent<Light>().enabled = false;
                squad[4].GetComponent<Light>().enabled = true;
            }
            #endregion Highlights
        }
    }
}
