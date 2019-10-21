using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PathDrawer : MonoBehaviour
{
    public Material paint;
    public Maze maze;
    private Material original;
    public Stack<Tile> path = new Stack<Tile>();
    private PlayerController player;

    public bool cutOff;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene == SceneManager.GetSceneByName("FindPath"))
        {
            if (maze == null)
                maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Maze>();
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    public void PaintPath(Tile input)
    {
        if (path.Count == 0)
        {
            if (input != maze.tiles[maze.startX, maze.startY])
            {
                if (maze.AvailableNeighboor(maze.tiles[maze.startX, maze.startY]).Contains(input))
                {
                    maze.tiles[maze.startX, maze.startY].floor.GetComponent<Renderer>().material = paint;
                    path.Push(maze.tiles[maze.startX, maze.startY]);
                    input.floor.GetComponent<Renderer>().material = paint;
                    path.Push(input);
                }
            }
            else
            {
                input.floor.GetComponent<Renderer>().material = paint;
                path.Push(input);
            }
        }
        else
        {
            if (path.Peek() != input)
            {
                Tile pre = path.Peek();
                if (path.Contains(input))
                {
                    pre.floor.GetComponent<Renderer>().material = pre.ogMat;
                    path.Pop();
                }
                else
                {
                    if (maze.AvailableNeighboor(pre).Contains(input))
                    {
                        input.floor.GetComponent<Renderer>().material = paint;
                        path.Push(input);
                    }
                }
            }
        }
    }

    public void CheckPath()
    {
        if (path.Count != 0)
        {
            if (!path.Contains(maze.tiles[maze.endX, maze.endY])
                || path.Peek() != maze.tiles[maze.endX, maze.endY])
            {
                cutOff = true;
            }
            else
            {
                List<Tile> movePath = new List<Tile>();
                int count = path.Count;
                for (int i = 0; i < count; i++)
                {
                    movePath.Add(path.Pop());
                }
                movePath.Reverse();
                player.moveList = movePath;
                player.SetTarget(movePath[0]);
            }
        }
    }

    public void ResetPath()
    {
        foreach(Tile tile in path)
        {
            tile.floor.GetComponent<Renderer>().material = tile.ogMat;
        }
        path.Clear();
    }
}
