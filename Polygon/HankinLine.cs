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

	GraphNode startNode;
	GraphNode endNode;

	public GraphNode StartNode{get{return startNode;}}
	public GraphNode EndNode{get{return endNode;}}

	public Vector2 Start { get { return startPoint; } }
	public Vector2 End
	{
		get
		{
			if (neighbour == null) return originPoint + direction * 25;
			shiftPoint();
			init();
			endPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { neighbour.Start, neighbour.Start + neighbour.direction });
			endNode.setPosition(endPoint);
			return endPoint;
		}
	}

	static List<HankinLine> allHankinLines = new List<HankinLine>();


	Tesselator tesselator;
	Graph graph;
	public void init(Vector2 origin, double _baseAngle, bool inverted, Graph _graph)
	{
		this.isVisible = true;
		angleInverted = inverted;

		originPoint = origin;
		baseAngle = _baseAngle;

		graph = _graph;

        // init startNode
        startNode = new GraphNode();
        endNode = new GraphNode();
		graph.addGraphNode(startNode);
		graph.addGraphNode(endNode);

		AddChild(startNode);
		AddChild(endNode);

		init();


	}

	private void init()
	{
		var dirAngle = (!angleInverted) ? baseAngle - HankinsOptions.angleFromBase : baseAngle - Math.PI + HankinsOptions.angleFromBase;
		direction = new Vector2((float)Math.Cos(dirAngle), (float)Math.Sin(dirAngle));
    }


	 
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
		startNode.setPosition(startPoint);
	}

}
