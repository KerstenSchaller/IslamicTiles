using Godot;
using System;

using System.Collections.Generic; //for list


public class Polygon : Godot.Node2D
{
    List<Vector2> vertices;
    List<PolygonSide> polygonSides;

    public void addVertices(Vector2[] _vertices)
    {
        var pos = this.Position;
        vertices.AddRange(_vertices);

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
            side.init(start, end);

            polygonSides.Add(side);
            this.AddChild(side);
        }
        foreach (var p in polygonSides)
        {
            p.connect();
        }
    }

    int scale = 100;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        vertices = new List<Vector2>();
        polygonSides = new List<PolygonSide>();
    }

}


