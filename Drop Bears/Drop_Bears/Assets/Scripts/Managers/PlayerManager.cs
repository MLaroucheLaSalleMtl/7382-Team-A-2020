using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] GameObject[] players;

    public GameObject[] Players { get => players; set => players = value; }

    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
