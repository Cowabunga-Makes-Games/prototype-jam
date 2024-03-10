using System;
using UnityEngine;


//Simple parent class for both player and enemy characters
public class Character : MonoBehaviour {
	//stores position of character
	public PosNode pos;
	//we will probably need to add more stuff as it comes up, but it's good to have a way of adding properties
	//to all characters together or players and enemies seperately I think


	// Changed to Init method instead of constructor to support Monobehaviour GameObject linkages for accessing
	// and manipulating the transform and other components associated with the Character type in question upon movement.
	// TODO: Init is to be handled by the Managers to link initial player position to a tile in the grid on Start
	// Note that the PlayerController enables player input on Start, so there could be a potential issue if
	// PlayerController.Start is handled before <Manager name>.Start
	public void Init() {
		pos = null;
	}

    //nonempty init
    public void Init(PosNode p) {
		pos = p;
    }
    
    //move this character north one node
    public void MoveNorth() {
	    if (!this.pos.getNorth().isOpen()) return;
	    
	    this.pos.setOccupant(null);
	    this.pos = this.pos.getNorth();
	    this.pos.setOccupant(this);
    }
    
    // move this character south one node
    public void MoveSouth() {
	    if (!this.pos.getSouth().isOpen()) return;
	    
	    this.pos.setOccupant(null);
	    this.pos = this.pos.getSouth();
	    this.pos.setOccupant(this);
    }
    
    // move this character east one node
    public void MoveEast() {
	    if (!this.pos.getEast().isOpen()) return;
	    
	    this.pos.setOccupant(null);
	    this.pos = this.pos.getEast();
	    this.pos.setOccupant(this);
    }
    
    // move this character west one node
    public void MoveWest() {
	    if (!this.pos.getWest().isOpen()) return;
	    
	    this.pos.setOccupant(null);
	    this.pos = this.pos.getWest();
	    this.pos.setOccupant(this);
    }
}

//Parent class for all enemies, we can create children classes for each subtype
public class Enemy : Character {
}

