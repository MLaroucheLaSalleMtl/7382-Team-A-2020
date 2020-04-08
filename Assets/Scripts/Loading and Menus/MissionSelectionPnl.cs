using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionSelectionPnl : MonoBehaviour
{
    [SerializeField] private Button btnBoss;
    [SerializeField] private Button[] btnLevels;

    void Start()
    {
        btnBoss.gameObject.SetActive(false);
    }
    public void MissionDesc(int missionnmb)
    {
        PnlDebrieffing.MissionNMB = missionnmb;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.levelsComplete>=btnLevels.Length)
        {
            btnBoss.gameObject.SetActive(true);
        }
    }
}
