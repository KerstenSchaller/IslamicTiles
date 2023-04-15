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

	bool showPoly = true;
	[Export(PropertyHint.Flags)]
	public bool ShowPoly
	{
		get { return showPoly; }
		set { showPoly = (bool)value; }
	}

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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var size = GetViewport().Size;
		width = (int)((size.x / 100) + 2);
		height = (int)((size.y / 100) + 2);

		if (false)
		{
			width = 1;
			height = 1;
		}

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
		Update();
	}

	public override void _Draw()
	{
		if (shapePoly == null) return;
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// Draw all polygonlines multiple times over the plane 
				var offset = new Vector2(-100, -100) + new Vector2(x * (float)shapePoly.getXDist() + (y % 2) * (float)shapePoly.getXOffset(), y * (float)shapePoly.getYDist() + (x % 2) * (float)shapePoly.getYOffset());

				if (showPoly)
				{
					foreach (var p in polygonSides)
					{
						var start = (p.Start + offset);
						var end = (p.End + offset);
						DrawLine(start, end, Colors.Gray);
					}

				}

				if (true)
				{
					// Draw all hankinslines multiple times over the plane 
					foreach (var p in hankinslines)
					{
						var start = (p.Start + offset);
						var end = (p.End + offset);
						DrawLine(start, end, Colors.SeaGreen);
					}
				}
			}
		}


	}
}
