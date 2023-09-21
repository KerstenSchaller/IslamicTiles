using Godot;
using System;

using System.Collections.Generic; //for list


public class Polygon : Godot.Node2D
{
    List<Vector2> vertices = new List<Vector2>();
    List<PolygonSide> polygonSides = new List<PolygonSide>();

    HankinLineCollector hankinLineCollector = new HankinLineCollector();

    public void init(Vector2[] _vertices)
    {
        var pos = this.Position;
        vertices.AddRange(_vertices);

        //set length of one segment in options to be used by other parts of the program
        HankinsOptions.getHankinsOptions().ShapesSideLength = (vertices[0] - vertices[1]).Length();

        // create polygonSides
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector2 start, end;

            if (i < vertices.Count - 1)
            {
                start = vertices[i];
                end = vertices[i + 1];
            }
            else
            {
                // last element, close loop
                start = vertices[i];
                end = vertices[0];
            }

            // create side
            var side = new PolygonSide();
            this.AddChild(side);
            side.init(start, end, hankinLineCollector);
            polygonSides.Add(side);

        }
        foreach (var p in polygonSides)
        {
            p.connect();
        }

    }


     
    public override void _Ready()
    {
        vertices = new List<Vector2>();
        polygonSides = new List<PolygonSide>();
    }

    public override void _Process(float delta)
    {

    }

}


