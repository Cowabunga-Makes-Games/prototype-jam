using System;
using UnityEngine;

//Parent class for all enemies, we can create children classes for each subtype
public class Enemy : Character
{
    public Enemy(PosNode p): base(p)
    {
        throw new NotImplementedException();
    }

    public bool initateDeath()
    {
        PosNode tile = this.getTile();
        tile.removeOccupant();
        Destroy(this.gameObject);
        return true;
    }


}