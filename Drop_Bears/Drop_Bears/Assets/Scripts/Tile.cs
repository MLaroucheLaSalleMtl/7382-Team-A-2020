using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool isObstacle;
    private MeshRenderer renderer;
    [SerializeField] private Vector2 loc;
    #region zachnotes
    //this is zach testing something that may account for obstacles
    [SerializeField]private int movementvalue;
    [SerializeField]private bool moveable=false;
    [SerializeField] private Material movemat;
    [SerializeField] private Material defaultmat;
    [SerializeField] private Material selectedTile;
    [SerializeField] private Material obstacleMat;
    [SerializeField] private Material attackMat;
    [SerializeField] private bool isSelected;
    [SerializeField] private int attackvalue;


    #endregion zachnotes

    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Enemy")
        {
            isEnemy = true;
           
        }
        if(other.tag=="Player")
        {
            IsPlayer = true;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            isPlayer = false;
          
        }
        if (other.tag=="Enemy")
        {
            IsEnemy = false;
         
        }
    }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool IsObstacle { get => isObstacle; set => isObstacle = value; }
    public bool IsPlayer { get => isPlayer; set => isPlayer = value; }
    public int Movementvalue { get => movementvalue; set => movementvalue = value; }
    public bool Moveable { get => (movementvalue >= 0) ? true:false; set => moveable = value; }
    public Vector2 Loc { get => loc; set => loc = value; }
    public Material Movemat { get => movemat; set => movemat = value; }
    public Material Defaultmat { get => defaultmat; set => defaultmat = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public int Attackvalue { get => attackvalue; set => attackvalue = value; }
    public Material AttackMat { get => attackMat; set => attackMat = value; }
    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }

    void Awake()
    {
        loc.x = this.X;
        loc.y = this.Y;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if (IsSelected)
        {
            renderer.material = selectedTile;
        }
       else if (IsObstacle )
        {
            renderer.material = obstacleMat;
        }
       else if(/*(isPlayer||IsEnemy)*/ /*&&*/(movementvalue<=0&&attackvalue<=0))
        {
            renderer.material = defaultmat;
        }
    
    
       
    }
}
