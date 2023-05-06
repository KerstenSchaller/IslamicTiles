/*Created by ChatGpt with prompt "Write an C# algorithm that finds minimal cycles in an undirected graph"*/


using System;
using System.Collections.Generic;

class UndirectedGraph
{
    private int V; // Number of vertices
    private List<int>[] adjList; // Adjacency list

    public UndirectedGraph(int V)
    {
        this.V = V;
        adjList = new List<int>[V];
        for (int i = 0; i < V; i++)
        {
            adjList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        // slight modification to not allow doubles
        if(!adjList[v].Contains(w))adjList[v].Add(w);
        if(!adjList[w].Contains(v))adjList[w].Add(v);
    }

    public List<List<int>> FindMinimalCycles()
    {
        List<List<int>> cycles = new List<List<int>>();
        bool[] visited = new bool[V];
        int[] parent = new int[V];

        for (int i = 0; i < V; i++)
        {
            if (!visited[i])
            {
                DFS(i, visited, parent, cycles);
            }
        }

        return cycles;
    }

  private void DFS(int v, bool[] visited, int[] parent, List<List<int>> cycles)
{
    visited[v] = true;

    foreach (int w in adjList[v])
    {
        if (!visited[w])
        {
            parent[w] = v;
            DFS(w, visited, parent, cycles);
        }
        else if (parent[v] != w)
        {
            // We have found a cycle
            List<int> cycle = new List<int>();
            int u = v;
            bool[] visitedCycle = new bool[V];
            visitedCycle[u] = true;

            while (u != w)
            {
                cycle.Add(u);
                u = parent[u];
                visitedCycle[u] = true;
            }
            cycle.Add(w);
            cycle.Add(v);

            // Add only the nodes that have not already been visited in the current cycle
            for (int i = 0; i < V; i++)
            {
                if (visitedCycle[i])
                {
                    cycle.Remove(i);
                }
            }

            cycles.Add(new List<int>(cycle));
        }
    }
}


}