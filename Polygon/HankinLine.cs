using Godot;
using System;

using System.Collections.Generic; //for list

public class HankinLineCollector
{
	int numberOfHankinsLines = 0;
	public List<HankinLine> allHankinLines = new List<HankinLine>();
	public int addHankinsline(HankinLine hankinLine) { allHankinLines.Add(hankinLine); numberOfHankinsLines++; return numberOfHankinsLines - 1; }
	public int NumberOfHankinLines { get { return allHankinLines.Count; } }



}
public class HankinLine : Node2D, ILine
{
	Vector2 originPoint;
	Vector2 startPoint;
	Vector2 endPoint;

	public Vector2 direction;


	bool angleInverted;
	int id;


	HankinLine neighbour; // the hankinsline which connects to this one if offset is 0
	public HankinLine sameSideNeigbour;

	double baseAngle; // angle of the polygonline the hankinlines originates from



	Vector2 startNode;
	Vector2 endNode;

	Vector2 intersectionNode;

	public Vector2 StartNode { get { return startNode; } }
	public Vector2 EndNode { get { return endNode; } }

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
	Vector2 frameEdgePoint;

	public void init(Vector2 origin, double _baseAngle, bool inverted, Vector2 _frameEdgePoint, HankinLineCollector _hankinLineCollector)
	{
		hankinLineCollector = _hankinLineCollector;
		frameEdgePoint = _frameEdgePoint;

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
		var dirAngle = (!angleInverted) ? baseAngle - HankinsOptions.getHankinsOptions().angleFromBase : baseAngle - Math.PI + HankinsOptions.getHankinsOptions().angleFromBase;
		direction = new Vector2((float)Math.Cos(dirAngle), (float)Math.Sin(dirAngle));
	}

	public override void _Process(float delta)
	{
		GD.Print("Process HankinLine");
		shiftPoint();
		init();
		endPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { neighbour.Start, neighbour.Start + neighbour.direction });
		endNode = endPoint;
		if (angleInverted ) calcEnclosedPolys();
		updateIntersection();

	}

	public Vector2 getSameSideNeighbourIntersection(){return LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { sameSideNeigbour.Start, sameSideNeigbour.Start + sameSideNeigbour.direction });}
	static List<Vector2> enclosedPoly3 = new List<Vector2>();
	public void calcEnclosedPolys()
	{
		var t = GetTree().GetNodesInGroup("Tesselator");
		Tesselator tesselator = (Tesselator)t[0];

		if (HankinsOptions.getHankinsOptions().offset == 0)
		{
			List<Vector2> enclosedPoly = new List<Vector2>();
			enclosedPoly.Add(frameEdgePoint);
			enclosedPoly.Add(startNode);
			enclosedPoly.Add(endNode);
			enclosedPoly.Add(neighbour.startNode);

			tesselator.addPoly(enclosedPoly.ToArray(), 0);
		}
		else
		{

			List<Vector2> enclosedPoly1 = new List<Vector2>();
			enclosedPoly1.Add(frameEdgePoint);
			enclosedPoly1.Add(sameSideNeigbour.startNode);
			enclosedPoly1.Add(getSameSideNeighbourIntersection());
			enclosedPoly1.Add(endNode);
			enclosedPoly1.Add(neighbour.getSameSideNeighbourIntersection());
			enclosedPoly1.Add(neighbour.sameSideNeigbour.startNode);

			List<Vector2> enclosedPoly2 = new List<Vector2>();
			enclosedPoly2.Add(sameSideNeigbour.startNode);
			enclosedPoly2.Add(startNode);
			enclosedPoly2.Add(getSameSideNeighbourIntersection());



			tesselator.addPoly(enclosedPoly1.ToArray(), 0);
			tesselator.addPoly(enclosedPoly2.ToArray(), 1);
		}




		//calc innerPoly
		if (this.id % 2 == 0)
		{
			if (id < 2) enclosedPoly3.Clear();
			enclosedPoly3.Add(EndNode);
			enclosedPoly3.Add(getSameSideNeighbourIntersection());
		}


		if (this.id > (hankinLineCollector.NumberOfHankinLines - 3))
		{
			tesselator.addPoly(enclosedPoly3.ToArray(), 2);
		}


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
		var dir = new Vector2((float)Math.Cos(baseAngle), (float)Math.Sin(baseAngle));
		// Limit offset depending of sidelength of the shape
		float offset = ((HankinsOptions.getHankinsOptions().ShapesSideLength / 2) - (float)HankinsOptions.getHankinsOptions().offset) >= 0 ? (float)HankinsOptions.getHankinsOptions().offset : HankinsOptions.getHankinsOptions().ShapesSideLength / 2;
		startPoint = (angleInverted) ? originPoint + dir * offset : originPoint - dir * offset;
		startNode = startPoint;
	}

	void updateIntersection()
	{
		var interPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { sameSideNeigbour.Start, sameSideNeigbour.Start + sameSideNeigbour.direction });
		intersectionNode = interPoint;
	}

}
