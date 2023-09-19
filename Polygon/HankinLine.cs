using Godot;
using System;

using System.Collections.Generic; //for list

public class HankinLineCollector
{
	int numberOfHankinsLines = 0;
	public List<HankinLine> allHankinLines = new List<HankinLine>();
	public int addHankinsline(HankinLine hankinLine){allHankinLines.Add(hankinLine);numberOfHankinsLines++;return numberOfHankinsLines-1 ;}
	public int NumberOfHankinLines{get{return allHankinLines.Count;}}

	 

}
public class HankinLine : Node2D, ILine
{
	Vector2 originPoint;
	Vector2 startPoint;
	Vector2 endPoint;

	public Vector2 direction;


	bool angleInverted;
	int id;


	HankinLine neighbour;
	HankinLine sameSideNeigbour;

	double baseAngle; // angle of the polygonline the hankinlines originates from



	Vector2 startNode;
	Vector2 endNode;

	Vector2 intersectionNode;

	public Vector2 StartNode{get{return startNode;}}
	public Vector2 EndNode{get{return endNode;}}

	public Vector2 Start { get { return startPoint; } }
	public Vector2 End
	{
		get
		{
			if (neighbour == null) return originPoint + direction * 25;
			return endPoint;
		}
	}

	public void addSameSideNeighbour(HankinLine _sameSideNeighbour)
	{
		sameSideNeigbour = _sameSideNeighbour;
	}

	

	Tesselator tesselator;
	HankinLineCollector hankinLineCollector;
	public void init(Vector2 origin, double _baseAngle, bool inverted, HankinLineCollector _hankinLineCollector)
	{
		hankinLineCollector = _hankinLineCollector;

		// add this to the tesselator so it can draw copys of it
		var t = GetTree().GetNodesInGroup("Tesselator");
		tesselator = (Tesselator)t[0];
		tesselator.addHankinsline(this);
		id = hankinLineCollector.addHankinsline(this);

		angleInverted = inverted;

		originPoint = origin;
		baseAngle = _baseAngle;

        // init startNode
        startNode = new Vector2();
        endNode = new Vector2();
		intersectionNode = new Vector2();

		init();


	}

	private void init()
	{
		var dirAngle = (!angleInverted) ? baseAngle - HankinsOptions.angleFromBase : baseAngle - Math.PI + HankinsOptions.angleFromBase;
		direction = new Vector2((float)Math.Cos(dirAngle), (float)Math.Sin(dirAngle));
    }

	public override void _Process(float delta)
	{
			shiftPoint();
			init();
			endPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { neighbour.Start, neighbour.Start + neighbour.direction });
			endNode = endPoint;
			updateIntersection();
	}

	public void connect()
	{
		// get neighbouring hankinline from list
		int neighbourId = (angleInverted) ? id - 1 : id + 1;
		if (id == 0 && angleInverted)
		{
			neighbourId = hankinLineCollector.NumberOfHankinLines - 1;
		}
		if (id == hankinLineCollector.NumberOfHankinLines - 1 && !angleInverted)
		{
			neighbourId = 0;
		}
		neighbour = hankinLineCollector.allHankinLines[neighbourId];
	}

	void shiftPoint()
	{
		var dir = new Vector2((float)Math.Cos(baseAngle),(float)Math.Sin(baseAngle));
		// Limit offset depending of sidelength of the shape
		float offset = ((HankinsOptions.ShapesSideLength/2) - (float)HankinsOptions.offset) >= 0 ? (float)HankinsOptions.offset :  HankinsOptions.ShapesSideLength/2;
		startPoint = (angleInverted) ? originPoint + dir*offset : originPoint - dir*offset;
		startNode = startPoint;
	}

	void updateIntersection()
	{
		var interPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { sameSideNeigbour.Start, sameSideNeigbour.Start + sameSideNeigbour.direction });
		intersectionNode = interPoint;
	}

}
