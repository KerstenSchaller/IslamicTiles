using Godot;
using System;

using System.Collections.Generic; //for list

public class SquarePattern : Node, IPattern
{
	int scale;
	Shape square;

	public void init(int _scale)
	{
		scale = _scale;
		square = new Shape();
		AddChild(square);
		square.init(4,scale);
	}
	public double getXDist()
	{
		return scale;
	}

	public double getXOffset()
	{
		return 0;
	}

	public double getYDist()
	{
		return scale;
	}

	public double getYOffset()
	{
		return 0;
	}
}



