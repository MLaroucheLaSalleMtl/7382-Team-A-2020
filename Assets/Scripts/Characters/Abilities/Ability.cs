using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability:MonoBehaviour
{
   private int id;
    private string name;
   private int castRange;
   private int aoe;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int CastRange { get => castRange; set => castRange = value; }
    public int Aoe { get => aoe; set => aoe = value; }

    public abstract void CastAbility(Tile tileTooCastOn,int attack);

    public abstract string GetAbilityDesc(int damage);

  
}
