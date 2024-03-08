﻿using System;


//Simple node to store the position in a grid
public class PosNode
{

	//stores the position of the node
	private (int, int) pos;
	//stores the character occupying the node
	public Character Occupant;
	//stores the nodes north (+y), south (-y), east (+x), and west (-x) of this node, may need to add more or modify to an array if we add diagonal movement
	private PosNode North;
    private PosNode South;
    private PosNode East;
    private PosNode West;

	//empty init
	public PosNode()
	{
		pos = (0, 0);
		Occupant = null;
		North = null;
		South = null;
		East = null;
		West = null;
	}

	//nonempty init
	public PosNode((int, int) p, Character o, PosNode n, PosNode s, PosNode e, PosNode w)
	{
		pos = p;
		Occupant = o;
		North = n;
		South = s;
		East = e;
		West = w;
	}

	//getter methods
	public Character getOcc()
	{
		return Occupant;
	}

	public PosNode getNorth()
	{
		return North;
	}
    public PosNode getSouth()
    {
        return South;
	}
    public PosNode getEast()
    {
	    return East;
    }
    public PosNode getWest()
    {
	    return West;
    }

	public void setOccupant(Character c)
	{
		Occupant = c;
	}

	//return if node is occupied
	public bool isOpen()
	{
		return (Occupant == null);
	}

}