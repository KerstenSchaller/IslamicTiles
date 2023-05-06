using System;
using Godot;
using System.Collections.Generic; //for list
class GraphSolver
{
    private List<GraphNode> graphNodes = new List<GraphNode>();

    List<List<int>> loops = new List<List<int>>();
    List<List<int>> uniqueLoops;
    List<List<int>> minimalLoops;

    public GraphSolver(Dictionary<int, GraphNode> _graphNodes)
    {
        foreach (var node in _graphNodes.Values)
        {
            graphNodes.Add(node);
        }

    }

    public void findUniqueCycles()
    {
        var cycles = loops;
        List<List<int>> temp = new List<List<int>>();
        for (int i = 0; i < cycles.Count; i++)
        {
            bool alreadyContained = false;
            for (int j = 0; j < temp.Count; j++)
            {
                if (compareCycles(cycles[i], temp[j]))
                {
                    alreadyContained = true;
                }
            }
            if (alreadyContained == false) temp.Add(cycles[i]);
        }
        uniqueLoops = temp;

    }


    private bool compareCycles(List<int> c1, List<int> c2)
    {
        if (c1.Count != c2.Count) return false;
        foreach (var i in c1)
        {
            if (!c2.Contains(i)) return false;
        }

        return true;
    }


    public void findClosedLoops()
    {
        List<int> currentLoop;
        for (int i = 0; i < graphNodes.Count; i++)
        {
            currentLoop = new List<int>();
            var node = graphNodes[i];
            foreach (var link in node.getLinkedNodeIds())
            {
                dfs(link, node.Id, currentLoop);

            }
        }
    }

    private void dfs(int currentNodeId, int parentNodeId, List<int> currentLoop)
    {
        GraphNode node = graphNodes[currentNodeId];

        if (currentLoop.Contains(currentNodeId))
        {
            if (currentNodeId == currentLoop[0])
            {
                // cycle found
                loops.Add(new List<int>(currentLoop));
                return;
            }
            else
            {
                return;
            }
        }
        currentLoop.Add(currentNodeId);

        //recurse again
        foreach (var link in node.getLinkedNodeIds())
        {
            if (link != parentNodeId) dfs(link, currentNodeId, currentLoop);
        }
        currentLoop.Remove(currentNodeId);

    }

    internal Vector2[] loopToPoly(List<int> loop)
    {
        List<Vector2> tpoly = new List<Vector2>();
        foreach (var n in loop)
        {
            tpoly.Add(graphNodes[n].getPosition());
        }
        return tpoly.ToArray();

    }

    internal void sortOutOverlappingCycles()
    {
        List<List<int>> temp = new List<List<int>>();
        for (int i = 0; i < uniqueLoops.Count; i++)
        {
            bool isOverlapping = false;
            for (int j = 0; j < uniqueLoops.Count; j++)
            {
                if (i == j) continue;
                var interSec =  Geometry.IntersectPolygons2d(loopToPoly(uniqueLoops[i]), loopToPoly(uniqueLoops[j]));
                var c = interSec.Count;
                //interSec[0]
                foreach (var p in uniqueLoops[j])
                {
                    var nodePos = graphNodes[p].getPosition();
                    var cycle = uniqueLoops[i];
                    if (IsPointInPolygon(cycle, nodePos))
                    {
                        isOverlapping = true;
                    }
                }
            }
            if (isOverlapping == false)
            {
                temp.Add(uniqueLoops[i]);
            }
        }
        minimalLoops = temp;


        var fromA = new Vector2(0,0);
        var toA = new Vector2(100,0);
        var fromB = new Vector2(50,0);
        var toB = new Vector2(50,0);
        var result = Geometry.SegmentIntersectsSegment2d(fromA,toA,fromB,toB);



    }

    private bool IsPointInPolygon(List<int> polygon, Vector2 testPoint)
    {
        List<Vector2> temp = new List<Vector2>();
        foreach (var p in polygon)
        {
            var n = graphNodes[p].getPosition();
            temp.Add(n);
        }
        var isPointInOrOnPolygon = Geometry.IsPointInPolygon(testPoint, temp.ToArray());
        var result = isPointInOrOnPolygon;
        if (isPointInOrOnPolygon)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                var p11 = temp[i];
                var p12 = i + 1 < temp.Count ? temp[i + 1] : temp[0];
                List<Vector2> line = new List<Vector2>() { p11, p12 };
                var isOnLine = LineHelper.isPointOnLine(testPoint, line, true);
                if (isOnLine)
                {
                    result = false;
                }
            }
        }

        // test linehelper
        var tp11 = new Vector2(0, 250);
        var tp12 = new Vector2(0, 500);

        var res5 = LineHelper.isPointOnLine(new Vector2(297.4965f, 297.4965f), new List<Vector2>() { tp11, tp12 }, true);

        return result;

    }
}
