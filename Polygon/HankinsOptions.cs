using Godot;
using System;

public class HankinsOptions : Node
{

	public static double angleFromBase = Math.PI / 4;

	[Export(PropertyHint.Range, "0,90,1.1")]
	public double Angle
	{
		get { return angleFromBase * 180 / Math.PI; }
		set { angleFromBase = value * Math.PI / 180; }
	}

	public static float offset;

	[Export(PropertyHint.Range, "0,50,1.1")]
	public double Offset
	{
		get { return offset; }
		set { offset = (float)value; }
	}

	public enum Shapes
	{
		Hexagon,
		Square,
		MultiTile, 
		OctagonRosette
	}

	public static Shapes shape = Shapes.Hexagon;
	[Export]
	public Shapes Shape
	{
		get { return shape; }
		set { shape = (Shapes)value; }
	}


	public static Color[] colors;
	[Export]
	public Color[] _Colors
	{
		get { return colors; }
		set { colors = (Color[])value; }
	}

	public static bool showPoly = true;
	[Export(PropertyHint.Flags)]
	public bool ShowPoly
	{
		get { return showPoly; }
		set { showPoly = (bool)value; }
	}

	public static bool showFrame = true;
	[Export(PropertyHint.Flags)]
	public bool ShowFrame
	{
		get { return showFrame; }
		set { showFrame = (bool)value; }
	}

	public static bool printSingleTileOnly = false;
	[Export(PropertyHint.Flags)]
	public bool PrintSingleTileOnly
	{
		get { return printSingleTileOnly; }
		set { printSingleTileOnly = (bool)value; }
	}
}
