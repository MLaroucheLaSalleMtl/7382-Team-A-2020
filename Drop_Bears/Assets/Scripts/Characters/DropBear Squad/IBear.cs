using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBear 
{
    int Hp {get; set;}
    int TotalHP { get; set; }
    int Defense { get; set; }
    int Melee { get; set; }
    int Ranged { get; set; }
    int Movement { get; set; }

}
