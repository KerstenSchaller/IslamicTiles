using Godot;
using System;

using System.Collections.Generic; //for list

public interface IShape
{
	double getXDist();
	double getYDist();
	double getXOffset();
	double getYOffset();
}

public class Tesselator : Node2D
{

	int width;
	int height;

	Vector2 originShift;
	int scale = 1;

	List<ILine> polygonSides = new List<ILine>();
	List<ILine> hankinslines = new List<ILine>();

	public IShape shapePoly;

	public void addPolygonSide(PolygonSide polySide)
	{
		polygonSides.Add(polySide);
	}

	public void addHankinsline(HankinLine polySide)
	{
		hankinslines.Add(polySide);
	}

	void getScreenSize()
	{
		var size = GetViewport().Size;
		width = (int)((size.x / 100) + 2);
		height = (int)((size.y / 100) + 2);

		if (HankinsOptions.printSingleTileOnly)
		{
			width = 1;
			height = 1;
			originShift = new Vector2(200,200);
			scale = 3;
		}
		else
		{
			originShift = new Vector2(-100,-100);
			scale = 1;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		getScreenSize();
		switch (HankinsOptions.shape)
		{
			case HankinsOptions.Shapes.Square:
				shapePoly = new Square();
				break;
			case HankinsOptions.Shapes.Hexagon:
				shapePoly = new Hexagon();
				break;
		}

		AddChild((Node)shapePoly);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var size = GetViewport().Size;
		width = (int)((size.x / shapePoly.getXDist()) + 2);
		height = (int)((size.y / shapePoly.getYDist()) + 2);
		Update();
	}

	public override void _Draw()
	{
		if (shapePoly == null) return;
		getScreenSize();
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// Draw all polygonlines multiple times over the plane 
				var offset = originShift + new Vector2(x * (float)shapePoly.getXDist() + (y % 2) * (float)shapePoly.getXOffset(), y * (float)shapePoly.getYDist() + (x % 2) * (float)shapePoly.getYOffset());

				if (HankinsOptions.showPoly)
				{
					foreach (var p in polygonSides)
					{
						var start = (scale*p.Start + offset);
						var end = (scale*p.End + offset);
						DrawLine(start, end, Colors.Gray);
					}

				}

				if (true)
				{
					// Draw all hankinslines multiple times over the plane 
					foreach (var p in hankinslines)
					{
						var start = (scale*p.Start + offset);
						var end = (scale*p.End + offset);
						DrawLine(start, end, Colors.SeaGreen);
					}
				}
			}
		}


	}
}
