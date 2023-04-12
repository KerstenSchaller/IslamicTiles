using Godot;
using System;



public class PolygonSide : Godot.Node2D
{
	Vector2 startPoint, endPoint; 
	Line2D line2d = new Line2D(); 

	public void init(Vector2 start, Vector2 end)
	{
		startPoint = start;
		endPoint = end;
		line2d.Points = new Vector2[]{startPoint, endPoint};
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		line2d.DefaultColor = Colors.BurlyWood;
		line2d.Width = 5;
		this.AddChild(line2d);

	}



	  // Called every frame. 'delta' is the elapsed time since the previous frame.
	  public override void _Process(float delta)
	  {
		  Update();
	  }
}

