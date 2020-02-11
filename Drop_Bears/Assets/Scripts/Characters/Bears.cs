using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bears : MonoBehaviour
{
    //Stats
    private IBear bearColor;
    private int hp;
    private int totalHP;
    private int defense;
    private int melee;
    private int ranged;
    private int movement;
    [SerializeField] private bool selected;

    //Ablilities
    private bool support; //Can be activated every turn once
    private bool special; //Only Happens every "N" turns

    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public int Melee { get => melee; set => melee = value; }
    public int Ranged { get => ranged; set => ranged = value; }
    public int Movement { get => movement; set => movement = value; }
    public int Defense { get => defense; set => defense = value; }
    public bool Special { get => special; set => special = value; }
    public bool Support { get => support; set => support = value; }
    public bool Selected { get => selected; set => selected = value; }
}
