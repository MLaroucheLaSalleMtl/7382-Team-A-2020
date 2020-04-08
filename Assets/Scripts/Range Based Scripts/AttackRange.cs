using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    
    // Start is called before the first frame update
   [SerializeField] TileManager tilemanager;
    float timer = .1f;
    private bool justOnce=false;
    [SerializeField]private bool melee;
    [SerializeField] private bool rangecheck;
    [SerializeField] private int range;
    [SerializeField] private int x;
    [SerializeField] private int y;
    private bool onlyonce = false;
    private Bears stats;
    private TileSelector tileselector;
    private int abilitytouse;
    private GameManager code;
    //public Bears nerd;

    public int Range { get => range; set => range = value; }
    public bool JustOnce { get => justOnce; set => justOnce = value; }


    void Start()
    {
        tilemanager = TileManager.instance;
        //  InvokeRepeating("DisplayAttackRange", 0, timer);
        code = GameManager.instance;
        tileselector = TileSelector.instance;
        stats = GetComponent<Bears>();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Tile")
    //    {
    //        this.x = other.GetComponent<Tile>().X;
    //        this.y = other.GetComponent<Tile>().Y;
    //        other.GetComponent<Tile>().IsPlayer = true;
    //    }

    //}
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
                tileshort.Renderer.material = tileshort.AttackMat;
            }
            else if (tileshort.Attackvalue == 0 && !tileshort.IsObstacle && !tileshort.IsSelected)
            {
                tileshort.Renderer.material = tileshort.Defaultmat;
            }
        }
    }
    private void DisplayAttackRange(TileManager tilemanager)
    {
        #region ZachNotes
        //This function checks after a movement value has been assigned to a tile if its moveable
        //if it is it gives it a material to indicate so
        #endregion ZachNotes
        foreach (GameObject tile in tilemanager.TileDic.Values)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (tileshort.Attackvalue > 0 && !tileshort.IsSelected && !tileshort.IsObstacle)
            {
                tileshort.Renderer.material = tileshort.AttackMat;
            }
            else if (tileshort.Attackvalue == 0 && !tileshort.IsObstacle && !tileshort.IsSelected)
            {
                tileshort.Renderer.material = tileshort.Defaultmat;
            }
        }
    }
    public void GetAttackRangeIgnoreObstacles(int Range,int x,int y)
    {
        #region ZachNotes
        //this will treat all tiles the same
        //this is usefull for attacks that disregard range
        #endregion ZachNotes
        for (int i = x - Range; i <= x + Range; i++)
        {
            int cal1 = Mathf.Abs(x - i);
            int diff= Mathf.Abs(cal1 - Range);
            for (int k = y - diff; k <= y + diff; k++)
            {
               GameObject thistile=tilemanager.GetTileDic(new Vector2(i,k));
                if(thistile!=null)
                {
                    thistile.GetComponent<Tile>().Attackvalue = 1;
                }
            }
        }
    }

    public void GetAttackRangeIgnoreObstacles(int Range, Tile tile)
    {
        #region ZachNotes
        //this will treat all tiles the same
        //this is usefull for attacks that disregard range
        #endregion ZachNotes
        if(Range==0)
        {
            tile.Attackvalue = 1;
            return;
        }
        for (int i = (int)tile.Loc.x - Range; i <= (int)tile.Loc.x + Range; i++)
        {
            int cal1 = Mathf.Abs(x - i);
            int diff = Mathf.Abs(cal1 - Range);
            for (int k = (int)tile.Loc.y - diff; k <= (int)tile.Loc.y + diff; k++)
            {
                GameObject thistile = tilemanager.GetTileDic(new Vector2(i, k));
                if (thistile != null)
                {
                    thistile.GetComponent<Tile>().Attackvalue = 1;
                }
            }
        }
    }
    public void GetAttackRangeIgnoreObstaclesEnemies(int Range, Tile tile,EnemyAIBase currentenemy)
    {
       
        for (int i = (int)tile.Loc.x - Range; i <= (int)tile.Loc.x + Range; i++)
        {
        
            int cal1 = Mathf.Abs((int)tile.Loc.x - i);
            int diff = Mathf.Abs(cal1 - Range);
            for (int k = (int)tile.Loc.y - diff; k <= (int)tile.Loc.y + diff; k++)
            {
                
                GameObject thistile = tilemanager.GetTileDic(new Vector2(i, k));
                if (thistile != null)
                {
                    Tile tileShort = thistile.GetComponent<Tile>();
                    if (tileShort != null)
                    {
                        tileShort.Attackvalue = 1;
                        if (tileShort.IsPlayer && thistile.GetComponentInChildren<Bears>().IsAlive)
                        {
                            AttackTilePairings pairing = new AttackTilePairings();
                            pairing.EnemyTile = tile;
                            pairing.PlayerTile = tileShort;
                            currentenemy.Pairs.Add(pairing);
                        }
                    }
                }
            }
        }
       
    }
    public void GetAttackRangeIgnoreObstaclesEnemyHealer(int Range, Tile tile, EnemyAIBase currentenemy)
    {
        #region ZachNotes
        //this will treat all tiles the same
        //this is usefull for attacks that disregard range
        //  ClearTileAttackValues();
        #endregion ZachNotes
        for (int i = (int)tile.Loc.x - Range; i <= (int)tile.Loc.x + Range; i++)
        {

            int cal1 = Mathf.Abs((int)tile.Loc.x - i);
            int diff = Mathf.Abs(cal1 - Range);
            for (int k = (int)tile.Loc.y - diff; k <= (int)tile.Loc.y + diff; k++)
            {

                GameObject thistile = tilemanager.GetTileDic(new Vector2(i, k));
                if (thistile != null)
                {
                    thistile.GetComponent<Tile>().Attackvalue = 1;
                    if (thistile.GetComponent<Tile>().IsEnemy && thistile.GetComponentInChildren<Bears>().IsAlive)
                    {
                        AttackTilePairings pairing = new AttackTilePairings();
                        pairing.EnemyTile = tile;
                        pairing.PlayerTile = thistile.GetComponent<Tile>();
                        currentenemy.Pairs.Add(pairing);
                    }
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
                    GameObject nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                    AssignTileAttackRange(nexttile, tileshort.Attackvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Attackvalue = -1;
            }
        }

        return;

    }
    private void AssignTileAttackRange(Tile tile, int AttackRange)
    {
        #region ZachNotes
        //this will assign attack values according to distance and with obstacles in mind
        //this could be usefull if we want descending hit values according to range 
        //and dont want be able to attack through cover
        #endregion ZachNotes
        if (tile != null)
        {

           
            if (!tile.IsObstacle)
            {
                if (tile.Attackvalue >= 0 && tile.Attackvalue < AttackRange)
                {
                    tile.Attackvalue = AttackRange;
                    GameObject nexttile = tilemanager.GetTileDic(new Vector2(tile.X - 1, tile.Y));
                    AssignTileAttackRange(nexttile, tile.Attackvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tile.X + 1, tile.Y));
                    AssignTileAttackRange(nexttile, tile.Attackvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y - 1));
                    AssignTileAttackRange(nexttile, tile.Attackvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y + 1));
                    AssignTileAttackRange(nexttile, tile.Attackvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Attackvalue = -1;
            }
        }

        return;

    }
    public void AssignDescendingTileAttackRange(GameObject tile, int AttackRange)
    {
        #region ZachNotes
        //this will assign attack values according to distance not taking in to account obstacles 
        //this could be usefull if we want descending hit values accoridng to range
        #endregion ZachNotes
        if (tile != null)
        {

            Tile tileshort = tile.GetComponent<Tile>();

            if (tileshort.Attackvalue >= 0 && tileshort.Attackvalue < AttackRange)
            {
                tileshort.Attackvalue = AttackRange;
      
                GameObject nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                AssignDescendingTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                AssignDescendingTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                AssignDescendingTileAttackRange(nexttile, tileshort.Attackvalue - 1);
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                AssignDescendingTileAttackRange(nexttile, tileshort.Attackvalue - 1);

            }


        }

        return;

    }

    public void AssignDescendingTileAttackRange(Tile tile, int AttackRange)
    {
        #region ZachNotes
        //this will assign attack values according to distance not taking in to account obstacles 
        //this could be usefull if we want descending hit values accoridng to range
        #endregion ZachNotes
        if (tile != null)
        {

            

            if (tile.Attackvalue >= 0 && tile.Attackvalue < AttackRange)
            {
                tile.Attackvalue = AttackRange;
                GameObject nexttile = tilemanager.GetTileDic(new Vector2(tile.X - 1, tile.Y));
                AssignDescendingTileAttackRange(nexttile, tile.Attackvalue - 1);
                nexttile = tilemanager.GetTileDic(new Vector2(tile.X + 1, tile.Y));
                AssignDescendingTileAttackRange(nexttile, tile.Attackvalue - 1);
                nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y - 1));
                AssignDescendingTileAttackRange(nexttile, tile.Attackvalue - 1);
                nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y + 1));
                AssignDescendingTileAttackRange(nexttile, tile.Attackvalue - 1);

            }


        }

        return;

    }
    /// VERY IMPORTANT FUNCTION MAKE SURE TO CALL WHENEVER YOU SWITCH CHARACTER AND ATTACK VALUES HAVE BEEN GENERATED
    public static void ClearTileAttackValues(TileManager tilemanager)
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
    public static void ClearTileAttackValues()
    {
        #region ZachNotes
        //this function clears the Attack values of all the tiles
        //this will need to be called whenever new attack ranges are calculated if not it can cause crashes
        //Because of the pathfinding system
        #endregion ZachNotes
        foreach (GameObject tile in TileManager.instance.Tilearray)
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

        if (code.CurrPhase == GameManager.Phase.attackPhase && GetComponent<Bears>().Selected)
        {
            abilitytouse = tileselector.AbilityToUse;
            switch (abilitytouse)
            {
                case 1:
                    range = stats.Range;
                    break;
                case 2:
                    range = stats.BasicAbility.CastRange;
                    break;
                case 3:
                    range = stats.SpecialAbility.CastRange;
                    break;
            }
            this.x = GetComponent<Movement>().X;
            this.y = GetComponent<Movement>().Y;

            //  GameObject startingtile = tilemanager.TileDic[new Vector2(x, y)];
            // AssignTileAttackRange(startingtile, range+1);
            GetAttackRangeIgnoreObstacles(range, x, y);
            if (!JustOnce)
            {
                DisplayAttackRange();
                justOnce = true;
                onlyonce = false;
            }


        }
        else if (code.CurrPhase == GameManager.Phase.confPhase && GetComponent<Bears>().Selected)
        {
            switch (abilitytouse)
            {
                case 1:
                    range = 0;
                    break;
                case 2:
                    range = stats.BasicAbility.Aoe;
                    break;
                case 3:
                    range = stats.SpecialAbility.Aoe;
                    break;

            }

            GetAttackRangeIgnoreObstacles(range, tileselector.CurrentTileShort);
            //if (!onlyonce)
            //{
                DisplayAttackRange();
                onlyonce= true;
                justOnce = false;
            //}

        }
        else
        {
            justOnce = false;
            onlyonce = false;
        }
       
    }
}

  
   
