using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreadthFirst : MonoBehaviour
{
    private Queue<Tile> frontier;
    public Maze maze;
    private Tile destination;
    private List<Tile> visited = new List<Tile>();
    private List<Tile> path = new List<Tile>();
    //public Material test, original;
    private Tile startTile;
    public bool returnAll;
    //private bool finding, found, shown;
    // Start is called before the first frame update
    void Start()
    {
        //startTile = maze.tiles[maze.startX, maze.startY];
        //destination = maze.tiles[maze.endX, maze.endY];
        //BFS();
    }

    public List<Tile> /*IEnumerator*/ BFS(Tile start, Tile destination)
    {
        Tile currentTile;
        frontier = new Queue<Tile>();
        path = new List<Tile>();
        visited = new List<Tile>();
        if (start == destination)
        {
            path.Add(start);
            return path;
        }
        frontier.Enqueue(start);
        visited.Add(start);
        //maze.tiles[maze.startX, maze.startY].floor.GetComponent<Renderer>().material = test;
        while(frontier.Count != 0)
        {
            currentTile = frontier.Dequeue();
            if (currentTile == destination)
                break;
            foreach (Tile neighboor in maze.AvailableNeighboor(currentTile))
            {
                if (!visited.Contains(neighboor))
                {
                    frontier.Enqueue(neighboor);
                    visited.Add(neighboor);
                    neighboor.previousTile = currentTile;
                    //neighboor.floor.GetComponent<Renderer>().material = test;
                }
            }
            //if(finding)
              //  yield return new WaitForSeconds(0f);
        }
        currentTile = destination.previousTile;
        path.Add(destination);
        path.Add(currentTile);
        while (!path.Contains(start))
        {
            path.Add(currentTile.previousTile);
            currentTile = currentTile.previousTile;
        }
        path.Reverse();
        /*foreach (Tile tile in maze.tiles)
        {
            tile.floor.GetComponent<Renderer>().material = original;
        }

        foreach (Tile tile in path)
        {
            tile.floor.GetComponent<Renderer>().material = test;
        }
        found = true;
        */
        //startTile = destination;
        if (!returnAll)
        {
            if (path.Count <= 7)
                return path;
            else
            {
                path.Clear();
                path.Add(start);
                return path;
            }
        }
        else
            return path;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
