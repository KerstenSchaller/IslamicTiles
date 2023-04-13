using Godot;
using System;



public class PolygonSide : Godot.Node2D
{
	Vector2 startPoint, endPoint;
	Color lineColor;
	public Vector2 Start{get{return startPoint;}}
	public Vector2 End{get{return endPoint;}}
	public Color LineColor{get{return lineColor;}} 
	//Line2D line2d = new Line2D(); 

	public void init(Vector2 start, Vector2 end)
	{

		startPoint = start;
		endPoint = end;
		//line2d.Points = new Vector2[]{startPoint, endPoint};
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lineColor = Colors.BurlyWood;
		//line2d.DefaultColor = lineColor;
		//line2d.Width = 5;
		//this.AddChild(line2d);

		// add this to the tesselator so it can draw copys of it
		//Tesselator tesselator = Tesselator.getInstance();
		var t = GetTree().GetNodesInGroup("Tesselator");
		Tesselator tesselator = (Tesselator)t[0];
		tesselator.addPolygonSide(this);
	}



	  // Called every frame. 'delta' is the elapsed time since the previous frame.
	  public override void _Process(float delta)
	  {
		  Update();
	  }
}

