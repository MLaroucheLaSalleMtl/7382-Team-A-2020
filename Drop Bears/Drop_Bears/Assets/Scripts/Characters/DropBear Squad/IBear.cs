using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBear 
{
    int hp {get; set;}
    int totalHP { get; set; }
    int defense { get; set; }
    int melee { get; set; }
    int ranged { get; set; }
    int movement { get; set; }

}
