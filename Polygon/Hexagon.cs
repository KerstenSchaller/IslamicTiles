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
		return (hexagon.Vertices[0] - hexagon.Vertices[4]).Length();
	}

	public double getYDist()
	{
		return Math.Abs((hexagon.Vertices[0].y - hexagon.Vertices[2].y));
	}

	public double getXOffset()
	{
		return (hexagon.Vertices[0] - hexagon.Vertices[4]).Length() / 2;
	}

	public double getYOffset()
	{
		return 0;
	}
}

public class Hexagon : Node2D, IShape
{
	List<Vector2> vertices = new List<Vector2>();
	public List<Vector2> Vertices{get{return vertices;}}
	Polygon polygon = new Polygon();

	private List<Vector2> CreateHexagonVertices()
	{
		List<Vector2> _vertices = new List<Vector2>();
		int numberOfVertices = 6;
		int radius = 60;
		for (int i = 0; i < numberOfVertices; i++)
		{
			float angle_deg = -60 * i + 30;
			float angle_rad = (float)Math.PI / 180 * angle_deg;
			float x = radius * (float)Math.Cos(angle_rad);
			float y = radius * (float)Math.Sin(angle_rad);
			_vertices.Add(new Vector2(x, y));
		}
		return _vertices;
	}

	public double getSideLength()
	{
		return (vertices[0] - vertices[1]).Length();
	}

	 
	public override void _Ready()
	{
		Graph graph = new Graph();
		AddChild(graph);
		this.AddChild(polygon);

		vertices = CreateHexagonVertices();
		polygon.init(vertices.ToArray(), graph);
		graph.buildGraphConnections();

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
