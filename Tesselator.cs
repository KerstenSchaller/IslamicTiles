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
		set { showPoly = (bool)value;  }
	}

	int xOffset = 100;
	int yOffset = 100;

	List<ILine> polygonSides = new List<ILine>();
	List<ILine> hankinslines = new List<ILine>();

	public Hexagon shapePoly;
	//public Square shapePoly;



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

		if(false)
		{
			width = 1;
			height = 1;
		}

		shapePoly = new Hexagon();
		AddChild(shapePoly);
		//shapePoly = new Square();
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	  Update();
  }

	public override void _Draw()
	{
		if(shapePoly == null)return;
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// Draw all polygonlines multiple times over the plane 
				 var offset =  new Vector2(100,100) + new Vector2(x*(float)shapePoly.getXDist()+(y%2)*(float)shapePoly.getXOffset(), y*(float)shapePoly.getYDist()+(x%2)*(float)shapePoly.getYOffset());
				//var offset = new Vector2(250,250);

				if(showPoly)
				{
					foreach (var p in polygonSides)
					{
						var start = (p.Start + offset);
						var end =  (p.End + offset);
						DrawLine(start, end, Colors.Gray);
					}

				}

				if(true)
				{
					// Draw all hankinslines multiple times over the plane 
					foreach (var p in hankinslines)
					{
						if(!p.IsLineVisible)continue;
						var start = (p.Start + offset);
						var end =  (p.End + offset);
						DrawLine(start, end, Colors.SeaGreen);
						//DrawCircle(end, 2, Colors.Green);
						//DrawCircle(end, 2, Colors.Red);
					}
				}
			}
		}

	   
	}
}
