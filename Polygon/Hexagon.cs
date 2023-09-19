using Godot;
using System;

using System.Collections.Generic; //for list

public class HexagonPattern : Node, IPattern
{
	List<Vector2> vertices = new List<Vector2>();
	Shape hexagon;

	public void init(int scale)
	{ 
		hexagon = new Shape();
		AddChild(hexagon);	
		hexagon.init(6, scale);
	}

	public double getXDist()
	{
		return (hexagon.vertices[2] - hexagon.vertices[5]).Length() + (hexagon.vertices[1] - hexagon.vertices[0]).Length();
	}

	public double getYDist()
	{
		return Math.Abs((hexagon.vertices[0].y - hexagon.vertices[3].y)/2);
	}

	public double getXOffset()
	{
		return (hexagon.vertices[2].x - hexagon.vertices[4].x);
	}

	public double getYOffset()
	{
		return 0;
	}
}
