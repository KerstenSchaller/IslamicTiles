using Godot;
using System;

using System.Collections.Generic; //for list


class MultiTilePattern : Node, IShape
{
	Polygon octagon = new Polygon();
	double LengthOctagon{get{return (scale + 2*scale*(new Vector2(1,1)).x);}}

	public IPattern Pattern {get{return pattern;}}

	Pattern pattern;

	Polygon square = new Polygon();

	int scale = 50;

	public override void _Ready()
	{
		pattern = new Pattern();
		pattern.setXDist(scale+LengthOctagon);
		pattern.setYDist(scale+scale*(new Vector2(1,1)).x);
		pattern.setXOffset(scale+scale*(new Vector2(1,1)).x);
		pattern.setYOffset(0);

		createSquare();
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
	private void createOctagon()
	{
		Graph graph = new Graph();
		AddChild(graph);

		var vertices = new Vector2[8];

		vertices[0] = new Vector2(scale, 0);
		vertices[1] = new Vector2(scale, scale);
		vertices[2] = vertices[1] + scale * new Vector2(1, 1);
		vertices[3] = vertices[2] + scale * new Vector2(1, 0);
		vertices[4] = vertices[3] + scale * new Vector2(1, -1);
		vertices[5] = vertices[4] + scale * new Vector2(0, -1);
		vertices[6] = vertices[5] + scale * new Vector2(-1, -1);
		vertices[7] = vertices[6] + scale * new Vector2(-1, 0);


		AddChild(octagon);
		octagon.init(vertices, graph);
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
