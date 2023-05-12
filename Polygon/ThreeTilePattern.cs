using Godot;
using System;

using System.Collections.Generic; //for list


class ThreeTilePattern : Node, IShape
{

    Polygon innerOctagon = new Polygon();
    double LengthOctagon { get { return (scale + 2 * (new Vector2(1, 1)).Normalized().x * scale); } }

    public IPattern Pattern { get { return pattern; } }

    Pattern pattern;

    Polygon square = new Polygon();

    int scale = 70;

    public override void _Ready()
    {
        pattern = new Pattern();
        pattern.setXDist(scale + LengthOctagon);
        pattern.setYDist(scale + (new Vector2(1, 1).Normalized().x * scale));
        pattern.setXOffset(scale + (new Vector2(1, 1).Normalized().x * scale));
        pattern.setYOffset(0);

		//createSquare();
		createOctagon();
    }

    private void createSquare()
    {
        Graph graph = new Graph();
        AddChild(graph);

        var vertices = new Vector2[] { new Vector2(0, 0), new Vector2(0, scale), new Vector2(scale, scale), new Vector2(scale, 0) };

        AddChild(square);
        square.init(vertices, graph);
    }

    private void createInnerOctagon(Vector2[] _vertices)
    {
        Graph graph = new Graph();
        AddChild(graph);
        var midpoint = (_vertices[0] + _vertices[4]) / 2;
        var innerVertices = new Vector2[8];
        for (int i = 0; i < _vertices.Length; i++)
        {
            var dirVec = midpoint - _vertices[i];
            innerVertices[i] = _vertices[i] + 0.5f * dirVec;
        }
		AddChild(innerOctagon);
        innerOctagon.init(innerVertices, graph);

		//return;

        for (int i = 0; i < 8; i++)
        {
            Polygon tpoly = new Polygon();
            var tVertices = new Vector2[4];
            tVertices[0] = _vertices[i];
            tVertices[1] = _vertices[i == 7 ? 0 : i + 1];
            tVertices[2] = innerVertices[i == 7 ? 0 : i + 1];
            tVertices[3] = innerVertices[i];
            AddChild(tpoly);
            Graph graph2 = new Graph();
            AddChild(graph2);
            tpoly.init(tVertices, graph2);

        }
    }
    private void createOctagon()
    {
        Graph graph = new Graph();
        AddChild(graph);

        var vertices = new Vector2[8];

        vertices[0] = new Vector2(scale, 0);
        vertices[1] = new Vector2(scale, scale);
        vertices[2] = vertices[1] + scale * new Vector2(1, 1).Normalized();
        vertices[3] = vertices[2] + scale * new Vector2(1, 0).Normalized();
        vertices[4] = vertices[3] + scale * new Vector2(1, -1).Normalized();
        vertices[5] = vertices[4] + scale * new Vector2(0, -1).Normalized();
        vertices[6] = vertices[5] + scale * new Vector2(-1, -1).Normalized();
        vertices[7] = vertices[6] + scale * new Vector2(-1, 0).Normalized();

        createInnerOctagon(vertices);
    }

    public double getSideLength()
    {
        return scale;
    }

    public int getNumberOfVertices()
    {
        return 1;
    }
}
