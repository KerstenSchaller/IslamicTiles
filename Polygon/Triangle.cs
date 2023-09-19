using Godot;
using System;

using System.Collections.Generic; //for list

public class TrianglePattern : Node, IPattern
{
	int scale;

	public void init(int _scale)
	{
		scale = _scale;
		var triangle1 = new Triangle();
		AddChild(triangle1);
		triangle1.init(3, scale);

		var triangle2 = new Triangle();
		AddChild(triangle2);
		triangle2.init(3, scale, triangle1.getInvertedVertice(1));
	}
	public double getXDist()
	{
		return scale;
	}

	public double getXOffset()
	{
		return scale/2;
	}

	public double getYDist()
	{
		return Math.Sqrt(scale*scale - scale*scale/4);
	}

	public double getYOffset()
	{
		return 0;
	}
}

public class Triangle : Shape, IShape
{
   

	


	public int getNumberOfVertices()
	{
		return 3;
	}

	public double getSideLength()
	{
		return scale;
	}
}

