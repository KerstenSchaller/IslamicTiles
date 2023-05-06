using System;
using Godot;
using System.Collections.Generic; //for list

class StupidGraphSolver
{
    Dictionary<int, GraphNode> graph;
    

    public StupidGraphSolver(Dictionary<int, GraphNode> _graph)
    {
        graph = _graph;
    }

    public List<GraphNode[]> getPolygons()
    {   
        int nSides = graph.Count/5;
        List<GraphNode[]> retval = new List<GraphNode[]>();
        GraphNode[] innerLoop = new GraphNode[nSides*2];
        for(int i=0;i < nSides;i++)
        {
            GraphNode[] loop = new GraphNode[4];
            loop[0] = graph[(i*5)];
            loop[1] = graph[(i*5)+1];
            loop[2] = graph[(i*5)+2];
            loop[3] = graph[i==0? graph.Count-4 : (i*5)-4];
            retval.Add(loop);
            innerLoop[i*2] = graph[(i*5)+1];
            innerLoop[(i*2)+1] = graph[i==nSides-1 ? 2 : (i*5)+4];

        }
        retval.Add(innerLoop);
        
        return retval;
    }


}