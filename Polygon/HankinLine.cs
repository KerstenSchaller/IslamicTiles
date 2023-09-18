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

    public IShape Shape {get{ return (IShape)GetParent().GetParent().GetParent();}}

	bool angleInverted;
	int id;


	HankinLine neighbour;
	HankinLine sameSideNeigbour;

	double baseAngle; // angle of the polygonline the hankinlines originates from



	GraphNode startNode;
	GraphNode endNode;

	GraphNode intersectionNode;

	public GraphNode StartNode{get{return startNode;}}
	public GraphNode EndNode{get{return endNode;}}

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
	Graph graph;
	HankinLineCollector hankinLineCollector;
	public void init(Vector2 origin, double _baseAngle, bool inverted, Graph _graph, HankinLineCollector _hankinLineCollector)
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

		graph = _graph;

        // init startNode
        startNode = new GraphNode();
        endNode = new GraphNode();
		intersectionNode = new GraphNode();
		graph.addGraphNode(startNode);
		graph.addGraphNode(intersectionNode);
		graph.addGraphNode(endNode);

		// set shape the parent shape to the nodes, will be used for individual offsets
		IShape shape = (IShape)GetParent().GetParent().GetParent();
		startNode.setShape(shape);
		intersectionNode.setShape(shape);
		endNode.setShape(shape);

		AddChild(startNode);
		AddChild(endNode);
		AddChild(intersectionNode);

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
			endNode.setPosition(endPoint);
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
		startNode.setPosition(startPoint);
	}

	void updateIntersection()
	{
		var interPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { sameSideNeigbour.Start, sameSideNeigbour.Start + sameSideNeigbour.direction });
		intersectionNode.setPosition(interPoint);
	}

}
