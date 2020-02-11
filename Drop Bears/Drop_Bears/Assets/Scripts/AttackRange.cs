using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    
    // Start is called before the first frame update4
   [SerializeField] TileManager tilemanager;
    float timer = .1f;
    private bool justOnce;
    [SerializeField]private bool melee;
    [SerializeField] private bool rangecheck;
    [SerializeField] private int range=3;
    [SerializeField] private int x;
    [SerializeField] private int y;
    void Start()
    {
        tilemanager = tilemanager.GetComponent<TileManager>();
      //  InvokeRepeating("DisplayAttackRange", 0, timer);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            this.x = other.GetComponent<Tile>().X;
            this.y = other.GetComponent<Tile>().Y;
            other.GetComponent<Tile>().IsPlayer = true;
        }

    }
    private void DisplayAttackRange()
    {
        #region ZachNotes
        //This function checks after a movement value has been assigned to a tile if its moveable
        //if it is it gives it a material to indicate so
        #endregion ZachNotes
        foreach (GameObject tile in tilemanager.TileDic.Values)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (tileshort.Attackvalue > 0 && !tileshort.IsSelected&&!tileshort.IsObstacle)
            {
                tile.GetComponent<MeshRenderer>().material = tile.GetComponent<Tile>().AttackMat;
            }
            else if (tileshort.Attackvalue == 0 && !tileshort.IsObstacle && !tileshort.IsSelected)
            {
                tile.GetComponent<MeshRenderer>().material = tile.GetComponent<Tile>().Defaultmat;
            }
        }
    }
    private void GetAttackRangeIgnoreObstacles()
    {
        #region ZachNotes
        //this will treat all tiles the same
        //this is usefull for attacks that disregard range
        #endregion ZachNotes
        for (int i = x - range; i <= x + range; i++)
        {
            int cal1 = Mathf.Abs(x - i);
            int diff = Mathf.Abs(cal1 - range);
            for (int k = this.y - diff; k <= this.y + diff; k++)
            {
               GameObject thistile=tilemanager.GetTile(new Vector2(i,k));
                if(thistile!=null)
                {
                    thistile.GetComponent<Tile>().Attackvalue = 1;
                }
            }
        }
    }
    private void AssignTileAttackRange(GameObject tile, int AttackRange)
    {
        #region ZachNotes
        //this will assign attack values according to distance and with obstacles in mind
        //this could be usefull if we want descending hit values according to range 
        //and dont want be able to attack through cover
        #endregion ZachNotes
        if (tile != null)
        {

            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                if (tileshort.Attackvalue >= 0 && tileshort.Attackvalue < AttackRange)
                {
                    tileshort.Attackvalue = AttackRange;   
                    GameObject nexttile = tilemanager.GetTile(new Vector2(tileshort.X - 1, tileshort.Y));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X + 1, tileshort.Y));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y - 1));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y + 1));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Movementvalue = -1;
            }
        }

        return;

    }
    private void AssignDescendingTileAttackRangeNoObs(GameObject tile, int AttackRange)
    {
        #region ZachNotes
        //this will assign attack values according to distance not taking in to account obstacles 
        //this could be usefull if we want descending hit values accoridng to range
# endregion ZachNotes
        if (tile != null)
        {

            Tile tileshort = tile.GetComponent<Tile>();
            
                if (tileshort.Attackvalue >= 0 && tileshort.Attackvalue < AttackRange)
                {
                    tileshort.Attackvalue = AttackRange;
                    GameObject nexttile = tilemanager.GetTile(new Vector2(tileshort.X - 1, tileshort.Y));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X + 1, tileshort.Y));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y - 1));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTile(new Vector2(tileshort.X, tileshort.Y + 1));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);

                }
            
           
        }

        return;

    }
    private void ClearTileAttackValues()
    {
        #region ZachNotes
        //this function clears the Attack values of all the tiles
        //this will need to be called whenever new attack ranges are calculated if not it can cause crashes
        //Because of the pathfinding system
        #endregion ZachNotes
        foreach (GameObject tile in tilemanager.Tilearray)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                tileshort.Attackvalue = 0;
            }
        }
    }
    void Update()
    {
        DisplayAttackRange();
        if (melee &&!justOnce)
        {
            GameObject startingtile = tilemanager.TileDic[new Vector2(this.x, this.y)];
            AssignTileAttackRange(startingtile, 2);
            justOnce = true;
        }
        else if(rangecheck &&!justOnce)
        {
            GameObject startingtile = tilemanager.TileDic[new Vector2(this.x, this.y)];
            // AssignTileAttackRange(startingtile, range+1);
            GetAttackRangeIgnoreObstacles();
            justOnce = true;
        }
    }
}

  
   
