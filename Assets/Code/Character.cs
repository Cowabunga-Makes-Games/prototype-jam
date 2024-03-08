using System;


//Simple parent class for both player and enemy characters
public class Character
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
}


//player class, feel free to change name btw
public class Player : Character
{


    public Player(PosNode p : base(p))
    {
    }


    //move the player north one node
    public void moveNorth()
    {
        if (this.pos.getNorth().isOpen())
        {
            this.pos.setOccupant(null);
            this.pos = this.pos.getNorth();
            this.pos.setOccupant(this);

        }
    }
}


//Parent class for all enemies, we can create children classes for each subtype
public class Enemy : Character
{
    public Enemy(PosNode p : base(p))
    {
    }
}

