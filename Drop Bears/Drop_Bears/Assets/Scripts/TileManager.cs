using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TileManager : MonoBehaviour
{
    public static TileManager instance = null;
    public static TileManager Instance => instance;
    //I Need the player to access a list or array of the tiles so im just playing around with options
    [SerializeField] private GameObject[] tilearray;
    public GameObject[] Tilearray { get => tilearray; set => tilearray = value; }
    public Dictionary<Vector2, GameObject> TileDic = new Dictionary<Vector2, GameObject>();
    private bool check = false;
    #region unneccesary_stuff
    //putting this stuff here for now as they are unneccesary and can be erased later
    //[SerializeField] private GameObject[][] tile;
    //public List<Tile> tileList = new List<Tile>();
    #endregion unneccesary_stuff
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        #region ZachNotes
        //cant return a list with this so imma try an array
        //I think i can simplify my movement code with a dictonary instead of using multiple lists
        //Gets a array of tiles then transfers it to a dictonary then i erase the array
        #endregion ZachNotes
        Tilearray = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < Tilearray.Length; i++)
        {
            TileDic.Add(tilearray[i].GetComponent<Tile>().Loc, tilearray[i]);
        }
        
    }
//This produces an error if the tile dont exist so unless we can solve that we need the array too
    //public GameObject GetTile(Vector2 coordinates)
    //{
    //    if (TileDic[coordinates]!=null)
    //        return TileDic[coordinates];
    //    else
    //        return null;
    //}
    #region arraymethod
        //currently this method works to calculate the areas of movement and in theory attack ranges 
        //using a dictonary doesnt work without error if the tile does not exist
    public GameObject GetTile(Vector2 coordinates)
    {
        for (int i = 0; i < tilearray.Length; i++)
        {
            if (coordinates.x == tilearray[i].GetComponent<Tile>().X)
            {
                if (coordinates.y == tilearray[i].GetComponent<Tile>().Y)
                {
                    return tilearray[i];
                }
            }
        }
        return null;

    }
    #endregion arraymethod
    // Update is called once per frame
    private void LateUpdate()
    {
        //save memory erase the array this can be done if you can make GetTile work with dic
        //if (check==false)
        //{
        //    Array.Clear(tilearray, 0, tilearray.Length);
        //}
    }
    void Update()
    {
        
    }
}
