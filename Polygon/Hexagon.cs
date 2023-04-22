using Godot;
using System;

using System.Collections.Generic; //for list

public class Hexagon : Node2D, IShape
{
	static List<Vector2> vertices = new List<Vector2>();
	Polygon polygon = new Polygon();
	private void CreateHexagonVertices()
	{
		int numberOfVertices = 6;
		int radius = 60;
		for (int i = 0; i < numberOfVertices; i++)
		{
			float angle_deg = -60 * i + 30;
			float angle_rad = (float)Math.PI / 180 * angle_deg;
			float x = radius * (float)Math.Cos(angle_rad);
			float y = radius * (float)Math.Sin(angle_rad);
			vertices.Add(new Vector2(x, y));
		}
	}

	public double getSideLength()
	{
		return (vertices[0] - vertices[1]).Length();
	}

	 
	public override void _Ready()
	{
		Graph graph = new Graph();
		AddChild(graph);
		graph.addShape(this);

		this.AddChild(polygon);
		CreateHexagonVertices();
		polygon.init(vertices.ToArray(), graph);
		graph.buildGraphConnections();


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

	}

	public double getXDist()
	{
		if (vertices.Count == 0) return 100;
		return (vertices[0] - vertices[4]).Length();
	}

	public double getYDist()
	{
		if (vertices.Count == 0) return 100;
		return Math.Abs((vertices[0].y - vertices[2].y));
	}

	public double getXOffset()
	{
		if (vertices.Count == 0) return 100;
		return (vertices[0] - vertices[4]).Length() / 2;
	}

	public double getYOffset()
	{
		return 0;
	}

	public int getNumberOfVertices()
	{
		return 6;
	}
}
