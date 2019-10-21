using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHug : MonoBehaviour
{
    private int currX, currY;
    private int facing;     //0: North, 1: East, 2: South, 3:West
    public Maze maze;
    public List<Tile> path = new List<Tile>();
    private List<Tile> Rpath = new List<Tile>();
    private List<Tile> Lpath = new List<Tile>();
    //public Material visited, original;
    //private float step;
    private int RStep, LStep;
    private bool RDone, Moved;
    // Start is called before the first frame update
    void Start()
    {
        facing = 1;
        //step = 0f;
        Invoke("LateStart", 0.1f);
    }

    private void LateStart()
    {
        LeftWallHugAlgorithm();
        //StartCoroutine(LeftWallHugAlgorithm());
    }

    /*IEnumerator*/ private void LeftWallHugAlgorithm()
    {
        facing = 1;
        currX = maze.startX; currY = maze.startY;
        while (currX != maze.endX || currY != maze.endY)
        {
            Tile currentTile = maze.tiles[currX, currY];
            LStep++;
            //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = original;
            if (facing == 0)
            {
                if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    /*maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    if (step != 0)
                    {
                        yield return new WaitForSeconds(step);
                    }*/
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    /*maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    if (step != 0)
                    {
                        yield return new WaitForSeconds(step);
                    }*/
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    /*maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    if (step != 0)
                    {
                        yield return new WaitForSeconds(step);
                    }*/
                    Lpath.Add(maze.tiles[currX, currY]);

                }
                else if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    /*maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    if (step != 0)
                    {
                        yield return new WaitForSeconds(step);
                    }*/
                    Lpath.Add(maze.tiles[currX, currY]);

                }
            }
            else if (facing == 1)
            {
                if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}                    
                    Lpath.Add(maze.tiles[currX, currY]);

                }
                else
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}                 
                    Lpath.Add(maze.tiles[currX, currY]);

                }
                else if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
            }
            else if (facing == 2)
            {
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
            }
            else if (facing == 3)
            {
                if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Lpath.Add(maze.tiles[currX, currY]);
                }
            }
        }
        if(RDone!=true)
        {
            //StartCoroutine(RightWallHugAlgorithm());
            RightWallHugAlgorithm();
        }
        //yield return null;
    }

    /*IEnumerator*/ private void RightWallHugAlgorithm()
    {
        facing = 1;
        currX = maze.startX; currY = maze.startY;
        while (currX != maze.endX || currY != maze.endY)
        {
            Tile currentTile = maze.tiles[currX, currY];
            RStep++;
            //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = original;
            if (facing == 0)
            {
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
            }
            else if (facing == 1)
            {
                if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
            }
            else if (facing == 2)
            {
                if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
            }
            else if (facing == 3)
            {
                if (currentTile.NWall == null)
                {
                    currY++;
                    facing = 0;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else if (currentTile.WWall == null && maze.tiles[currX - 1, currY].EWall == null)
                {
                    currX--;
                    facing = 3;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.SWall == null && maze.tiles[currX, currY - 1].NWall == null)
                {
                    currY--;
                    facing = 2;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
                else
                if (currentTile.EWall == null)
                {
                    currX++;
                    facing = 1;
                    //maze.tiles[currX, currY].floor.GetComponent<Renderer>().material = visited;
                    //if (step != 0)
                    //{
                    //    yield return new WaitForSeconds(step);
                    //}
                    Rpath.Add(maze.tiles[currX, currY]);
                }
            }
            if(RStep > LStep && !RDone)
            {
                break;
            }
        }
        RDone = true;
        //step = 0.2f;
        Debug.Log("R: " + RStep + "L: " + LStep);
        if (!Moved)
        {
            Moved = true;
            if (RStep < LStep)
            {
                RStep = LStep = 0;
                //StartCoroutine(RightWallHugAlgorithm());
                path = Rpath;
            }
            else
            {
                RStep = LStep = 0;
                //StartCoroutine(LeftWallHugAlgorithm());
                path = Lpath;
            }
        }
        //yield return null;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
