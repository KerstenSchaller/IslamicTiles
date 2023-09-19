using Godot;
using System;

using System.Collections.Generic; //for list

public class HexagonPattern : Node, IPattern
{
	List<Vector2> vertices = new List<Vector2>();
	Hexagon hexagon;

	public override void _Ready()
	{ 
		hexagon = new Hexagon();
		AddChild(hexagon);	
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

public class Hexagon : Shape, IShape
{

	public double getSideLength()
	{
		return (vertices[0] - vertices[1]).Length();
	}

	 
	public override void _Ready()
	{
		this.AddChild(polygon);

		Vector2[] startLine = new Vector2[]{new Vector2(100,100), new Vector2(200,100)};
		vertices = PolyHelper.CreatePolygonVertices(startLine, 6, 100);
		polygon.init(vertices.ToArray());

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

	}

	public int getNumberOfVertices()
	{
		return 6;
	}
}
