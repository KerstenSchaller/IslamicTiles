using Godot;
using System;

using System.Collections.Generic; //for list


public class MultiTilePattern : Node, IPattern
{
	
	double LengthOctagon{get{return (scale + 2*(new Vector2(1,1)).Normalized().x*scale);}}

	

	int scale = 50;

	public override void _Ready()
	{
		createSquare();
		createOctagon();
	}

	private void createSquare()
	{
		Square square = new Square();
		AddChild(square);
		square.init(scale);
	}
	private void createOctagon()
	{
		Octagon octagon = new Octagon();
		

		Graph graph = new Graph();
		AddChild(graph);
		
		AddChild(octagon);
		octagon.init(50);
	}

	public double getSideLength()
	{
		return scale;
	}

	public int getNumberOfVertices()
	{
		return 1;
	}

    public double getXDist()
    {
        return scale+LengthOctagon;
    }

    public double getYDist()
    {
        return scale+(new Vector2(1,1).Normalized().x*scale);
    }

    public double getXOffset()
    {
        return scale+(new Vector2(1,1).Normalized().x*scale);
    }

    public double getYOffset()
    {
        return 0;
    }
}
