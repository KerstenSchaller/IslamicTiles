using Godot;
using System;

public class HankinsOptions : Node
{
	public static float offset;
    public static double angleFromBase = Math.PI / 4;

	[Export(PropertyHint.Range, "0,90,1.1")]
	public double Angle
	{
		get { return angleFromBase*180/Math.PI; }
		set {angleFromBase = value * Math.PI/180;  }
	}

	[Export(PropertyHint.Range, "0,50,1.1")]
	public double Offset
	{
		get { return offset; }
		set { offset = (float)value;  }
	}
}
