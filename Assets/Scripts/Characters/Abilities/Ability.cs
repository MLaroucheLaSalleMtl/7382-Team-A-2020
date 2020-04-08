using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability:MonoBehaviour
{
   private int id;
    private string name;
   private int castRange;
   private int aoe;
    private bool alt;

    //alt is used to determine what audio clips to use basically derek in the constructors of your abilities set alt to true
    //id is useless i dont actuallly need it 
    //cast range is how the range of the skill 
    //aoe is used if its an aoe skill

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int CastRange { get => castRange; set => castRange = value; }
    public int Aoe { get => aoe; set => aoe = value; }
    public bool Alt { get => alt; set => alt = value; }

    public abstract void CastAbility(Tile tileTooCastOn,int attack);

    public abstract string GetAbilityDesc(int damage);

  
}
