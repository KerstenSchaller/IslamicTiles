using Godot;
using System;

using System.Collections.Generic; //for list

public class Graph : Node2D
{
	public enum NodeType { Edge, Inner }

	int nextId = 0;
	Dictionary<int, GraphNode> nodes = new Dictionary<int, GraphNode>();

	List<GraphNode[]> polys = new List<GraphNode[]>();

	public void addGraphNode(GraphNode node)
	{
		nodes.Add(nextId, node);
		node.Id = nextId;
		nextId++;

	}

	public void buildGraphConnections()
	{
		//foreach (var n in nodes) n.Value.clearNeighbours();
		//connectPolygonEdgeNodes();
		if(nodes.Count == 0)return;
		var l = nodes[0].shape.getSideLength();
		var o = HankinsOptions.offset;
		var nXPos = nodes[3].getPosition().x;


		StupidGraphSolver graphSolver = new StupidGraphSolver(nodes);
		AddChild(graphSolver);
		polys = graphSolver.getPolygons(1);
		return;

		/*
		// case 1: No addtional intersection nodes
		if (o == 0)
		{
			polys = graphSolver.getPolygons(0);

		}

		// case 2: one addtional intersection node
		// Xpos of 
		if (o > 0 && nXPos < l / 2)
		{
			polys = graphSolver.getPolygons(1);
		}

		// case 3: two additional intersection nodes
		if (o > 0 && nXPos > l / 2)
		{
			//GraphSolver graphSolver = new GraphSolver();
		}
		*/


	}



	public override void _Process(float delta)
	{
		base._Process(delta);
		buildGraphConnections();

	}




}

