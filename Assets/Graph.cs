using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public Dictionary<Vector2Int, List<Vector2Int>> adjList;
    private HashSet<Vector2Int> roadTiles;

    public Graph(int cityWidth, int cityHeight, HashSet<Vector2Int> roadTiles)
    {
        this.roadTiles = roadTiles;
        adjList = new Dictionary<Vector2Int, List<Vector2Int>>();

        for (int x = 0; x < cityWidth; x++)
        {
            for (int y = 0; y < cityHeight; y++)
            {
                Vector2Int current = new Vector2Int(x, y);
                adjList[current] = new List<Vector2Int>();

                // Only include current tile if it's a road tile
                if (roadTiles.Contains(current))
                {
                    if (roadTiles.Contains(new Vector2Int(x + 1, y)))
                    {
                        adjList[current].Add(new Vector2Int(x + 1, y));
                    }

                    if (roadTiles.Contains(new Vector2Int(x, y + 1)))
                    {
                        adjList[current].Add(new Vector2Int(x, y + 1));
                    }

                    if (roadTiles.Contains(new Vector2Int(x - 1, y)))
                    {
                        adjList[current].Add(new Vector2Int(x - 1, y));
                    }

                    if (roadTiles.Contains(new Vector2Int(x, y - 1)))
                    {
                        adjList[current].Add(new Vector2Int(x, y - 1));
                    }
                }
            }
        }
    }
}
