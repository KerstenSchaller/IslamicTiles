using Godot;
using System;

using System.Collections.Generic; //for list

public class SquarePattern : Node, IPattern
{
	int scale;
	Square square;

	public void init(int _scale)
	{
		scale = _scale;
		square = new Square();
		AddChild(square);
		square.init(scale);
	}
	public double getXDist()
	{
		return scale;
	}

	public double getXOffset()
	{
		return 0;
	}

	public double getYDist()
	{
		return scale;
	}

	public double getYOffset()
	{
		return 0;
	}
}

public class Square : Shape, IShape
{
   
	
	public void init(int _scale)
	{
		scale = _scale;

		this.AddChild(polygon);
		var startLine = new Vector2[]{new Vector2(0,0),new Vector2(0,scale)};
		
		vertices = PolyHelper.CreatePolygonVertices(startLine, 4, scale); 
		polygon.init(vertices.ToArray());
	}


	public int getNumberOfVertices()
	{
		return 4;
	}

	public double getSideLength()
	{
		return scale;
	}
}

