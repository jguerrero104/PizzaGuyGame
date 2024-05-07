using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    public static List<Vector2Int> FindShortestPath(Graph graph, Vector2Int start, Vector2Int end)
    {
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, Vector2Int> previous = new Dictionary<Vector2Int, Vector2Int>();
        List<Vector2Int> nodes = new List<Vector2Int>();
        List<Vector2Int> path = new List<Vector2Int>();

        // Initialize distances and previous
        foreach (Vector2Int vertex in graph.adjList.Keys)
        {
            if (vertex == start)
            {
                distances[vertex] = 0;
            }
            else
            {
                distances[vertex] = int.MaxValue;
            }
            previous[vertex] = new Vector2Int(-1, -1);
            nodes.Add(vertex);
        }

        // Dijkstra's Algorithm
        while (nodes.Count != 0)
        {
            // Sort nodes by distance
            nodes.Sort((a, b) => distances[a] - distances[b]);
            Vector2Int smallest = nodes[0];
            nodes.Remove(smallest);

            if (smallest == end)
            {
                // Build path
                while (previous[smallest] != new Vector2Int(-1, -1))
                {
                    path.Insert(0, smallest);
                    smallest = previous[smallest];
                }
                break;
            }

            if (distances[smallest] == int.MaxValue)
            {
                break;
            }

            foreach (Vector2Int neighbor in graph.adjList[smallest])
            {
                int alt = distances[smallest] + 1;
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    previous[neighbor] = smallest;
                }
            }
        }

        return path;
    }
}
