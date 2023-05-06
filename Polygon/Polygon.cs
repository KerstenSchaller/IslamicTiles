using Godot;
using System;

using System.Collections.Generic; //for list


public class Polygon : Godot.Node2D
{
    List<Vector2> vertices;
    List<PolygonSide> polygonSides;

    public void init(Vector2[] _vertices, Graph graph)
    {
        var pos = this.Position;
        vertices.AddRange(_vertices);
        HankinLineCollector hankinLineCollector = new HankinLineCollector();

        // create polygonSides
        for (int i = 0; i < vertices.Count; i++)
        {
            // create node from vertice 
            var node = new GraphNode();
            IShape shape = (IShape)GetParent();
            node.setShape(shape);
            node.setPosition(vertices[i]);
            graph.addGraphNode(node);
            AddChild(node);

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
            side.init(start, end, graph, hankinLineCollector);
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

}


