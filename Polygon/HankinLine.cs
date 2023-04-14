using Godot;
using System;

using System.Collections.Generic; //for list
public class HankinLine : Node2D, ILine
{
    Vector2 originPoint;
    Vector2 endPoint;

    public Vector2 direction;
    double offset;

    bool angleInverted;
    int id;
    static int numberOfHankinsLines = 0;

    HankinLine neighbour;

    double baseAngle; // angle of the polygonline the hankinlines originates from
    static double angleFromBase = Math.PI / 4;

    public Vector2 Start { get { return originPoint; } }
    public Vector2 End
    {
        get
        {
            if (neighbour == null) return originPoint + direction * 25;
            endPoint = LineHelper.calcIntersection(new List<Vector2>() { this.Start, this.Start + this.direction }, new List<Vector2>() { neighbour.Start, neighbour.Start + neighbour.direction });
            return endPoint;
        }
    }

    static List<HankinLine> allHankinLines = new List<HankinLine>();

    public void init(Vector2 origin, double _baseAngle, bool inverted)
    {
        angleInverted = inverted;

        originPoint = origin;
        baseAngle = _baseAngle;

        var dirAngle = (!inverted) ? baseAngle - angleFromBase : baseAngle - Math.PI + angleFromBase;
        direction = new Vector2((float)Math.Cos(dirAngle), (float)Math.Sin(dirAngle));

    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // add this to the tesselator so it can draw copys of it
        var t = GetTree().GetNodesInGroup("Tesselator");
        Tesselator tesselator = (Tesselator)t[0];
        tesselator.addHankinsline(this);
        allHankinLines.Add(this);

        id = numberOfHankinsLines;
        numberOfHankinsLines++;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

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

    public override void _Draw()
    {
		/*
        Vector2 p1 = new Vector2(200, 200);
        Vector2 dir = new Vector2(-1, 0);
        Vector2 p2 = p1 + dir * 50;
        Vector2 p12 = (p1 + p2) / 2;
        DrawLine(p1, p2, Colors.Azure);
        DrawCircle(p1, 5, Colors.Red);
        DrawCircle(p12, 2, Colors.Green);
        double a = Math.Atan2(dir.y, dir.x);
		a = a - Math.PI/4 - Math.PI/2;
        Vector2 p3 = p12 + new Vector2( (float)Math.Cos(a), (float)Math.Sin(a))*25;
		DrawLine(p12, p3, Colors.Blue);
		*/

		var p1 = new Vector2(100,100);
		var p2 = new Vector2(100,200);

		var p3 = new Vector2(100,300);
		var p4 = new Vector2(100,400);
		
		bool dummy = false;
		LineHelper.calcIntersection(p1,p2,p3,p4,ref dummy);


    }

}
