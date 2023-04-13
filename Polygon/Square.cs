using Godot;
using System;

using System.Collections.Generic; //for list



public class Square : Node2D
{
   
	Polygon polygon = new Polygon();
	int scale = 100;
	

	public override void _Ready()
	{
		this.AddChild(polygon);
		var vertices = new Vector2[]{new Vector2(0,0),new Vector2(0,scale),new Vector2(scale,scale),new Vector2(scale,0)};
		polygon.addVertices(vertices);
	}

	public void tesselate()
	{
		
	}

}

