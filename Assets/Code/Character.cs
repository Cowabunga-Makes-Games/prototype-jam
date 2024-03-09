using System;
using UnityEngine;


//Simple parent class for both player and enemy characters
public class Character : MonoBehaviour
{
	//stores position of character
	public PosNode pos;
	//we will probably need to add more stuff as it comes up, but it's good to have a way of adding properties
	//to all characters together or players and enemies seperately I think


	//empty init
	public Character()
	{
		pos = null;
	}

    //nonempty init
    public Character(PosNode p)
    {
		pos = p;
    }
    
    //move this character north one node
    public void moveNorth()
    {
	    if (this.pos.getNorth().isOpen())
	    {
		    this.pos.setOccupant(null);
		    this.pos = this.pos.getNorth();
		    this.pos.setOccupant(this);

	    }
    }
    
    // move this character south one node
    public void moveSouth()
    {
	    if (this.pos.getSouth().isOpen())
	    {
		    this.pos.setOccupant(null);
		    this.pos = this.pos.getSouth();
		    this.pos.setOccupant(this);

	    }
    }
    
    // move this character east one node
    public void moveEast()
    {
	    if (this.pos.getEast().isOpen())
	    {
		    this.pos.setOccupant(null);
		    this.pos = this.pos.getEast();
		    this.pos.setOccupant(this);

	    }
    }
    
    // move this character west one node
    public void moveWest()
    {
	    if (this.pos.getWest().isOpen())
	    {
		    this.pos.setOccupant(null);
		    this.pos = this.pos.getWest();
		    this.pos.setOccupant(this);

	    }
    }
}


//player class, feel free to change name btw
public class Player : Character
{


    public Player(PosNode p) : base(p)
    {
    }
}


//Parent class for all enemies, we can create children classes for each subtype
public class Enemy : Character
{
    public Enemy(PosNode p) : base(p)
    {
	    throw new NotImplementedException();
    }

    public bool initateDeath()
    {
	    this.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
	    return true;
    }
}

