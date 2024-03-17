using System;
using UnityEngine;

//Parent class for all enemies, we can create children classes for each subtype
public class Enemy : Character
{
    public Enemy(PosNode p) : base(p)
    {
        throw new NotImplementedException();
    }

    public bool initateDeath()
    {
        this.GetComponentInChildren<Renderer>().material.color = new Color(0, 0, 0);
        return true;
    }


}