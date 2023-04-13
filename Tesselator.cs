using Godot;
using System;

using System.Collections.Generic; //for list

public class Tesselator : Node2D
{

	int width;
	int height;

	List<PolygonSide> polygonSides = new List<PolygonSide>();

	public void addPolygonSide(PolygonSide polySide)
	{
		polygonSides.Add(polySide);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var size = GetViewport().Size;
		width = (int)((size.x/100)+2);
		height = (int)((size.y/100)+2);
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	  Update();
  }

	public override void _Draw()
	{
		int xOffset = 100;
		int yOffset = 100;
		for (int y = 0; y < height-1; y++)
		{
			for (int x = 0; x < width-1; x++)
			{
				foreach (var p in polygonSides)
				{
					var offset = p.Position + new Vector2(x*xOffset, y*yOffset);
					DrawLine(p.Start + offset, p.End + offset, p.LineColor);
				}
			}
		}

	   
	}
}
