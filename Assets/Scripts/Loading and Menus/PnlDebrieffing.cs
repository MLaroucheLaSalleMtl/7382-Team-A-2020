using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PnlDebrieffing : MonoBehaviour
{
    public static int MissionNMB;
    [SerializeField] private TextMeshProUGUI missionDesc;
    private void OnEnable()
    {
        switch (MissionNMB)
        {
            case 1:
                //enter your mission descriptions here if you dont like what i wrote
                missionDesc.text = "Sheeps";
                break;
            case 2:
                missionDesc.text = "BUCKS";
                break;
            case 3:
                missionDesc.text = "LEOMON";
                break;
            case 4:
                missionDesc.text = "BIG BOSSY";
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
