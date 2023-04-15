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



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddChild(polygon);
		CreateHexagonVertices();
		polygon.addVertices(vertices.ToArray());
	}

	public override void _Draw()
	{
		/*
		for (int i = 0; i < vertices.Count; i++)
		{
			Vector2 p1 = this.Position + vertices[i];
			Vector2 p2;
			if (i == vertices.Count - 1)
			{
				p2 = this.Position + vertices[0];
			} 
			else
			{
				p2 = this.Position + vertices[i + 1];
			}
			DrawLine(p1, p2, Colors.Red);
		}

		
		DrawCircle(this.Position + vertices[0],5, Colors.Red);
		DrawCircle(this.Position + vertices[1],5, Colors.Green);
		DrawCircle(this.Position + vertices[2],5, Colors.Blue);
		DrawCircle(this.Position + vertices[3],5, Colors.Yellow);
		DrawCircle(this.Position + vertices[4],5, Colors.Purple);
		DrawCircle(this.Position + vertices[5],5, Colors.Pink);
		*/

		//DrawCircle(this.Position ,5, Colors.Pink);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

	}

	public double getXDist()
	{
		if(vertices.Count == 0)return 100;
		return (vertices[0]-vertices[4]).Length();
	}

	public double getYDist()
	{
		if(vertices.Count == 0)return 100;
		return Math.Abs((vertices[0].y-vertices[2].y));
	}

	public double getXOffset()
	{
		if(vertices.Count == 0)return 100;
		return (vertices[0]-vertices[4]).Length()/2;
	}

	public double getYOffset()
	{
		return 0;
	}
}
