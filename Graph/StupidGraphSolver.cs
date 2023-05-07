using System;
using Godot;
using System.Collections.Generic; //for list

class StupidGraphSolver : Node
{
    Dictionary<int, GraphNode> graph;


    public StupidGraphSolver(Dictionary<int, GraphNode> _graph)
    {
        graph = _graph;
    }

    public List<GraphNode[]> getPolygons(int _case)
    {

        switch (_case)
        {
            case 0:
                return getPolygons0();

            case 1:
                return getPolygons1();

            case 2:
                return getPolygons0();

            default:
                return getPolygons0();

        }
    }



    private List<GraphNode[]> getPolygons0()
    {
        var t = GetTree().GetNodesInGroup("Tesselator");
        var tesselator = (Tesselator)t[0];
  

        int nSides = graph.Count / 7;
        List<GraphNode[]> retval = new List<GraphNode[]>();
        GraphNode[] innerLoop = new GraphNode[nSides * 2];
        for (int i = 0; i < nSides; i++)
        {
            GraphNode[] loop = new GraphNode[4];
            loop[0] = graph[(i * 7)];
            loop[1] = graph[(i * 7) + 4];
            loop[2] = graph[(i * 7) + 3];
            loop[3] = graph[i == 0 ? graph.Count - 6 : (i * 7) - 6];
            retval.Add(loop);
            innerLoop[i * 2] = graph[(i * 7) + 1];
            innerLoop[(i * 2) + 1] = graph[i == nSides - 1 ? 3 : (i * 7) + 6];

            tesselator.addPoly(loop, 1);


        }
        tesselator.addPoly(innerLoop, 2);
        retval.Add(innerLoop);



        return retval;
    }

    private List<GraphNode[]> getPolygons1()
    {
        var t = GetTree().GetNodesInGroup("Tesselator");
        var tesselator = (Tesselator)t[0];


        int nSides = graph.Count / 7;
        List<GraphNode[]> retval = new List<GraphNode[]>();
        GraphNode[] innerLoop = new GraphNode[nSides * 2];
        for (int i = 0; i < nSides; i++)
        {
            GraphNode[] loop1 = new GraphNode[3];
            loop1[0] = graph[(i * 7) + 4];
            loop1[1] = graph[(i * 7) + 1];
            loop1[2] = graph[(i * 7) + 2];
            retval.Add(loop1);

            GraphNode[] loop2 = new GraphNode[6];
            loop2[0] = graph[(i * 7)];
            loop2[1] = graph[(i * 7) + 4];
            loop2[2] = graph[(i * 7) + 2];
            loop2[3] = graph[(i * 7) + 3];
            loop2[4] = graph[i == 0 ? graph.Count - 2 : (i * 7) - 2];
            loop2[5] = graph[i == 0 ? graph.Count - 6 : (i * 7) - 6];
            retval.Add(loop2);

            innerLoop[i * 2] = graph[(i * 7) + 2];
            innerLoop[(i * 2) + 1] = graph[(i * 7) + 6];

            tesselator.addPoly(loop1, 0);
            tesselator.addPoly(loop2, 1);
        }
        tesselator.addPoly(innerLoop, 2);
        retval.Add(innerLoop);

        return retval;
    }

    private List<GraphNode[]> getPolygons3()
    {

        throw new NotImplementedException();

        return new List<GraphNode[]>();
    }


}