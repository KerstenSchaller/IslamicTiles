using Godot;
using System;



public class PolygonSide : Node2D, ILine
{
	Vector2 startPoint, endPoint, midPoint;

	public Vector2 Start { get { return startPoint; } }
	public Vector2 End { get { return endPoint; } }


    HankinLine hankinLine1;
	HankinLine hankinLine2;


	public void init(Vector2 start, Vector2 end, HankinLineCollector hankinLineCollector)
	{
		startPoint = start;
		endPoint = end;

		// do calculations to create hankinsline
		var midX = (startPoint.x + endPoint.x) / 2;
		var midY = (startPoint.y + endPoint.y) / 2;

		midPoint = new Vector2(midX, midY);
		var diff = endPoint - startPoint;
		var lineAngle = Math.Atan2((endPoint.y - startPoint.y), endPoint.x - startPoint.x);

		

		hankinLine1 = new HankinLine();
		AddChild(hankinLine1);
		hankinLine1.init(midPoint, lineAngle, true, startPoint, hankinLineCollector);

		hankinLine2 = new HankinLine();
		AddChild(hankinLine2);
		hankinLine2.init(midPoint, lineAngle, false, endPoint, hankinLineCollector);


		hankinLine1.addSameSideNeighbour(hankinLine2);
		hankinLine2.addSameSideNeighbour(hankinLine1);

	}

	 
	public override void _Ready()
	{
		// add this to the tesselator so it can draw copys of it
		var t = GetTree().GetNodesInGroup("Tesselator");
		Tesselator tesselator = (Tesselator)t[0];
		tesselator.addPolygonSide(this);


	}

	public void connect()
	{
		hankinLine1.connect();
		hankinLine2.connect();

	}
}

