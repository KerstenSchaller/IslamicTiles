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

public class Square : Node2D, IShape
{
   
	Polygon polygon = new Polygon();
	int scale;
	
	public void init(int _scale)
	{
		scale = _scale;
		Graph graph = new Graph();
		AddChild(graph);

		this.AddChild(polygon);
		var vertices = new Vector2[]{new Vector2(0,0),new Vector2(0,scale),new Vector2(scale,scale),new Vector2(scale,0)};
		polygon.init(vertices, graph);
		graph.buildGraphConnections();
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

