using Godot;
using System;

using System.Collections.Generic; //for list

public class Tesselator : Node2D
{

	int width;
	int height;

	int xOffset = 100;
	int yOffset = 100;

	List<ILine> polygonSides = new List<ILine>();
	List<ILine> hankinslines = new List<ILine>();

	public void addPolygonSide(PolygonSide polySide)
	{
		polygonSides.Add(polySide);
	}

	public void addHankinsline(HankinLine polySide)
	{
		hankinslines.Add(polySide);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var size = GetViewport().Size;
		width = (int)((size.x/100)+2);
		height = (int)((size.y/100)+2);

		if(true)
		{
			width = 1;
			height = 1;
		}
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	  Update();
  }

	public override void _Draw()
	{

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// Draw all polygonlines multiple times over the plane 
				var offset =  new Vector2(x*xOffset, y*yOffset);
				foreach (var p in polygonSides)
				{
					var start = (p.Start + offset);
					var end =  (p.End + offset);
					DrawLine(start, end, Colors.Gray);
				}

				// Draw all hankinslines multiple times over the plane 
				foreach (var p in hankinslines)
				{
					var start = (p.Start + offset);
					var end =  (p.End + offset);
					DrawLine(start, end, Colors.SeaGreen);
				}
			}
		}

	   
	}
}
