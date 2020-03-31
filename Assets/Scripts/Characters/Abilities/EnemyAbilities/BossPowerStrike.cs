using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerStrike :Ability
{
    public BossPowerStrike()
    {
        Id = 11;
        Name = "BIG DAMAGE";
        CastRange = 1;
        Aoe = 1;
    }

  

    private void AttackSquare(Vector2 tile, TileManager tilemanager, int damage)
    {
        GameObject tileToBurn = tilemanager.GetTileDic(tile);
        if (tileToBurn != null&&tileToBurn.GetComponent<Tile>()!=GetComponentInParent<Tile>())
        {
            Bears target = tileToBurn.GetComponentInChildren<Bears>();
            if (target)
            {
                if (target.Invincible == false)
                    tileToBurn.GetComponentInChildren<Bears>().Hp -= damage;
            }
        }
        
    }
    public override string GetAbilityDesc(int damage)
    {
        int number = (int)(damage * 1.5);
        return "BIG HURTS " + number.ToString();
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        int damage = (int)(attack * (1.5));
        TileManager tilemanager = TileManager.instance;
        if (tileToCastOn.GetComponentInChildren<Bears>() != null)
            tileToCastOn.GetComponentInChildren<Bears>().Hp -= damage;
        Vector2 attackCoordinate = new Vector2(tileToCastOn.X - 1, tileToCastOn.Y);
        AttackSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X + 1, tileToCastOn.Y);
        AttackSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X, tileToCastOn.Y + 1);
        AttackSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X, tileToCastOn.Y - 1);
        AttackSquare(attackCoordinate, tilemanager, damage);
        

    }
}
