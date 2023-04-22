using Godot;
using System;

using System.Collections.Generic; //for list

public class Graph : Node
{
    public enum NodeType{Edge,Inner}

    static int nextId = 0;
    Dictionary<int,GraphNode> nodes = new Dictionary<int, GraphNode>();

    static IShape shape;

    public void addShape(IShape _shape){shape = _shape;}

    public void addGraphNode(GraphNode node)
    {   
        nodes.Add(nextId, node);
        node.Id = nextId;
        nextId++;
        
    } 


     
    public void buildGraphConnections()
    {
        foreach(var n in nodes)n.Value.clearNeighbours();
        connectPolygonEdgeNodes();

        var l = shape.getSideLength();
        var o = HankinsOptions.offset;
        var nXPos = nodes[2].Position.x;


        // case 1: No addtional intersection nodes
        if(o == 0)
        {
            case1Connections();
            GraphSolver graphSolver = new GraphSolver(20);
            for(int i =0; i < nodes.Count;i++)
            {
                GraphNode node = nodes[i];
                foreach(var link in node.getLinkedNodeIds())
                {
                    graphSolver.addEdge(node.Id, link);
                }
                graphSolver.findClosedLoops();
            }
        }

        // case 2: one addtional intersection node
        // Xpos of 
        if(o > 0 && nXPos > l/2)
        {
            //GraphSolver graphSolver = new GraphSolver();
        }

        // case 3: two additional intersection nodes
        if(o > 0 && nXPos < l/2)
        {
            //GraphSolver graphSolver = new GraphSolver();
        }

        

    } 

    private void case1Connections()
    {
        int n = shape.getNumberOfVertices();
        for (int i = 0; i < n; i++)
        {
            // connect along the border of the polygon
            // one the first side the order of positions from left to right expressed as Ids is
            //   2 4
            // 0 3 1 5
            // first three nodes on that line will be connected to the id
            // before and after
            // 4th node is already belonging to next loop iteration

            var node2 = nodes[i*5+3];
            node2.addLink(node2.Id + 1);

            var node3 = nodes[i*5+1];
            node3.addLink(node3.Id + 1);

            var node4 = nodes[i*5+2];
            node4.addLink(node4.Id - 1);
            node4.addLink( (i==0 ? n*5-1 : node4.Id - 3));

            //var ind = i == 0 ? 5*n - 4 : (5*i) - 4; 
            var node5 = nodes[i*5+4];
            node5.addLink(node5.Id - 1);
            node5.addLink( (i==n-1 ? 2 : node5.Id + 3));
        }



    }
    private void case2Connections()
    {

    }
    private void case3Connections()
    {

    }

    void connectPolygonEdgeNodes()
    {
        int n = shape.getNumberOfVertices();
        for (int i = 0; i < n; i++)
        {
            // connect along the border of the polygon
            // one the first side the order of positions from left to right expressed as Ids is
            //   2 4
            // 0 3 1 5
            // first three nodes on that line will be connected to the id
            // before and after
            // 4th node is already belonging to next loop iteration

            var node1 = nodes[i*5];
            node1.addLink(i == 0 ? 5*n - 4 : (5*i) - 4);
            node1.addLink(node1.Id + 3);

            var node2 = nodes[i*5+3];
            node2.addLink(node2.Id - 3);
            node2.addLink(node2.Id - 2);

            var node3 = nodes[i*5+1];
            node3.addLink(i == n - 1 ? 0 : node3.Id + 4);
            node3.addLink(node3.Id + 2);
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        buildGraphConnections();
    }
}


class GraphSolver {
    private int numVertices;
    private List<int>[] adjList;

    public GraphSolver(int numVertices) {
        this.numVertices = numVertices;
        adjList = new List<int>[numVertices];
        for (int i = 0; i < numVertices; i++) {
            adjList[i] = new List<int>();
        }
    }

    public void addEdge(int src, int dest) {
        adjList[src].Add(dest);
        adjList[dest].Add(src);
    }

    public void findClosedLoops() {
        bool[] visited = new bool[numVertices];
        for (int i = 0; i < numVertices; i++) {
            if (!visited[i]) {
                dfs(i, visited, -1, new HashSet<int>());
            }
        }
    }

    private void dfs(int curr, bool[] visited, int parent, HashSet<int> path) {
        visited[curr] = true;
        path.Add(curr);

        foreach (int neighbor in adjList[curr]) {
            if (!visited[neighbor]) {
                dfs(neighbor, visited, curr, path);
            }
            else if (neighbor != parent && path.Contains(neighbor)) {
                // Found a closed loop
                GD.Print("Closed loop: ");
                foreach (int node in path) {
                    GD.Print(node + " ");
                    if (node == neighbor) {
                        break;
                    }
                }
                GD.Print();
            }
        }

        path.Remove(curr);
    }
}