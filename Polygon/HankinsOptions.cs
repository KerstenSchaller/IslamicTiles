using Godot;
using System;

public class HankinsOptions : Node
{

	public static double angleFromBase = Math.PI / 4; 

	public static double AngleGrad
	{
		get { return angleFromBase * 180 / Math.PI; }
		set { angleFromBase = value * Math.PI / 180; }
	}


	public static float offset;


	public enum Shapes
	{
		Hexagon,
		Square,
		MultiTile, 
		OctagonRosette
	}

	public static bool resetRequest = false;

	public static float ShapesSideLength = -1;

	public static Shapes shape = Shapes.Hexagon;

	public static Color[] colors;
	[Export]
	public Color[] _Colors
	{
		get { return colors; }
		set { colors = (Color[])value; }
	}

	public static bool showPoly = true;
	public static bool showFrame = true;
	public static bool printSingleTileOnly = false;

}
