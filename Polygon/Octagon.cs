using Godot;

public class Octagon : Node2D, IShape
{
   
	Polygon polygon = new Polygon();
	int scale;
	
	public void init(int _scale)
	{
		scale = _scale;
		Graph graph = new Graph();
		AddChild(graph);

		this.AddChild(polygon);

        var vertices = new Vector2[8];
        vertices[0] = new Vector2(scale, 0);
		vertices[1] = new Vector2(scale, scale);
		vertices[2] = vertices[1] + scale * new Vector2( 1,  1).Normalized();
		vertices[3] = vertices[2] + scale * new Vector2( 1,  0).Normalized();
		vertices[4] = vertices[3] + scale * new Vector2( 1, -1).Normalized();
		vertices[5] = vertices[4] + scale * new Vector2( 0, -1).Normalized();
		vertices[6] = vertices[5] + scale * new Vector2(-1, -1).Normalized();
		vertices[7] = vertices[6] + scale * new Vector2(-1,  0).Normalized();

		polygon.init(vertices, graph);
		graph.buildGraphConnections();
	}


	public int getNumberOfVertices()
	{
		return 4;
	}

	public double getSideLength()
	{
		return scale;
	}
}
