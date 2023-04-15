using Godot;
using System;

using System.Collections.Generic; //for list
public class HankinLine : Node2D, ILine
{
	Vector2 originPoint;
	Vector2 startPoint;
	Vector2 endPoint;

	public Vector2 direction;


	bool angleInverted;
	int id;
	static int numberOfHankinsLines = 0;

	HankinLine neighbour;

	double baseAngle; // angle of the polygonline the hankinlines originates from


	bool isVisible = false;
	public bool IsLineVisible{get{return isVisible;}}

	public Vector2 Start { get { return startPoint; } }
	public Vector2 End
	{
		get
		{
			if (neighbour == null) return originPoint + direction * 25;
			shiftPoint();
			init();
			endPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { neighbour.Start, neighbour.Start + neighbour.direction });
			return endPoint;
		}
	}

	static List<HankinLine> allHankinLines = new List<HankinLine>();


	Tesselator tesselator;
	public void init(Vector2 origin, double _baseAngle, bool inverted)
	{
		this.isVisible = true;
		angleInverted = inverted;

		originPoint = origin;
		baseAngle = _baseAngle;

		init();

	}

	private void init()
	{
		var dirAngle = (!angleInverted) ? baseAngle - HankinsOptions.angleFromBase : baseAngle - Math.PI + HankinsOptions.angleFromBase;
		direction = new Vector2((float)Math.Cos(dirAngle), (float)Math.Sin(dirAngle));
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// add this to the tesselator so it can draw copys of it
		var t = GetTree().GetNodesInGroup("Tesselator");
		tesselator = (Tesselator)t[0];
		tesselator.addHankinsline(this);
		allHankinLines.Add(this);

		id = numberOfHankinsLines;
		numberOfHankinsLines++;
	}

	public void connect()
	{
		// get neighbouring hankinline from list
		int neighbourId = (angleInverted) ? id - 1 : id + 1;
		if (id == 0 && angleInverted)
		{
			neighbourId = numberOfHankinsLines - 1;
		}
		if (id == numberOfHankinsLines - 1 && !angleInverted)
		{
			neighbourId = 0;
		}
		neighbour = allHankinLines[neighbourId];
	}

	void shiftPoint()
	{
		var dir = new Vector2((float)Math.Cos(baseAngle),(float)Math.Sin(baseAngle));
		startPoint = (angleInverted) ? originPoint + dir*(float)HankinsOptions.offset : originPoint - dir*(float)HankinsOptions.offset;
	}

}
