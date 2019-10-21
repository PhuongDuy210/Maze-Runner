using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int Size;
    public GameObject wall, floor;
    private float roomSize;
    private float wallSize;
    public Tile[,] tiles;

    public int currX, currY;
    private bool isFinished;

    public Material finishTile, startTile;

    public int startX, startY;
    public int endX, endY;

    public bool clickable;

    private CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        /*classic = GameObject.FindGameObjectWithTag("GameController").GetComponent<ClassicMaster>();
        if (classic != null)
        {
            UpdateAllReference();
            classic.maze = this;
        }*/

        /*TimeMaster time = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeMaster>();
        if (time != null)
            time.maze = this;*/

        GameMaster versus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>();
        if (versus != null)
            versus.maze = this;
    }

    public void UpdateAllReference(ClassicMaster classic)
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        switch (classic.level)
        {
            case 1:
            case 2:
            case 3:
                Size = 5;
                cameraController.ChangeCameraPosition(0);
                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                Size = 7;
                cameraController.ChangeCameraPosition(1);
                break;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
                Size = 10;
                cameraController.ChangeCameraPosition(2);
                break;
            default:
                Size = 12;
                cameraController.ChangeCameraPosition(3);
                break;
        }
        TriggerMaze();
    }


    public void TriggerMaze()
    {
        roomSize = floor.transform.localScale.x * 10f;
        wallSize = wall.transform.localScale.z;
        transform.position = new Vector3(roomSize * Size / 2, 0f, roomSize * Size / 2);
        InitMaze();
        startY = Random.Range(0, Size);
        startX = 0;
        endX = Size - 1;
        endY = Random.Range(0, Size);
        currX = Random.Range(0, Size);
        currY = Random.Range(0, Size);
        isFinished = false;
        GenerateMaze();
    }

    private void InitMaze()
    {
        tiles = new Tile[Size, Size];
        for(int h = 0; h < Size;h++)
        {
            for(int v = 0; v < Size; v++)
            {
                var room = new GameObject();
                room.name = "Room (" + h + "," + v + ")";
                tiles[h,v] = room.AddComponent<Tile>();
                
                tiles[h, v].floor = Instantiate(floor, new Vector3(roomSize * h, 0, roomSize * v), floor.transform.rotation);
                tiles[h, v].floor.transform.parent = room.transform;
                tiles[h, v].ogMat = tiles[h, v].floor.GetComponent<Renderer>().material;
                if (clickable)
                    room.AddComponent<ClickDetecter>();
                if (h==0)
                {
                    tiles[h, v].WWall = Instantiate(wall, new Vector3(-(roomSize) / 2, wall.transform.position.y, roomSize * v), wall.transform.rotation);
                    tiles[h, v].WWall.transform.Rotate(Vector3.up * 90);
                    tiles[h, v].WWall.transform.parent = room.transform;

                }

                if (v == 0)
                {
                    tiles[h, v].SWall = Instantiate(wall, new Vector3(roomSize  * h , wall.transform.position.y, -roomSize  / 2), wall.transform.rotation);
                    tiles[h, v].SWall.transform.parent = room.transform;
                }

                tiles[h, v].EWall = Instantiate(wall, new Vector3(roomSize * (h + 0.5f), wall.transform.position.y, roomSize * v), wall.transform.rotation);
                tiles[h, v].EWall.transform.Rotate(Vector3.up * 90);
                tiles[h, v].EWall.transform.parent = room.transform;

                tiles[h, v].NWall = Instantiate(wall, new Vector3(roomSize  * h, wall.transform.position.y, roomSize  * (v + 0.5f)), wall.transform.rotation);
                tiles[h, v].NWall.transform.parent = room.transform;

                tiles[h, v].pos = new Vector2(h, v);

                room.transform.parent = transform;
                //room.transform.position = tiles[h, v].floor.transform.position;
                BoxCollider collider = room.AddComponent<BoxCollider>();
                collider.size = new Vector3(roomSize,0.5f,roomSize);
                collider.center = new Vector3(tiles[h,v].floor.transform.position.x, wall.transform.localScale.y + 0.1f, tiles[h, v].floor.transform.position.z);
            }
        }
    }

    private void GenerateMaze()
    {
        while(!isFinished)
        {
            Kill();
            Hunt();
        }
        tiles[startX, startY].floor.GetComponent<Renderer>().material = startTile;
        tiles[startX, startY].ogMat = startTile;
        //tiles[startX, startY].DestroyWall(tiles[startX, startY].WWall);
        tiles[endX, endY].floor.GetComponent<Renderer>().material = finishTile;
        tiles[endX, endY].ogMat = finishTile;
        tiles[endX, endY].floor.AddComponent<PlayerDetecter>();
        //tiles[endX, endY].DestroyWall(tiles[endX, endY].EWall);

    }

    private void Kill()
    {
        int dir;
        while(CanStillMove(currX,currY))
        {
            dir = Random.Range(0, 4);       //0: North, 1: East, 2: South, 3: West
            if(dir == 0 && HaveNotVisit(currX,currY+1))
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].NWall);
                tiles[currX, currY].NWall = null;
                tiles[currX, currY+1].DestroyWall(tiles[currX, currY+1].SWall);
                tiles[currX, currY + 1].SWall = null;
                currY++;
            }

            else if(dir == 1 && HaveNotVisit(currX+1,currY))
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].EWall);
                tiles[currX, currY].EWall = null;
                tiles[currX+1, currY].DestroyWall(tiles[currX+1, currY].WWall);
                tiles[currX + 1, currY].WWall = null;
                currX++;
            }

            else if (dir == 2 && HaveNotVisit(currX, currY - 1))
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].SWall);
                tiles[currX, currY].SWall = null;
                tiles[currX, currY-1].DestroyWall(tiles[currX, currY-1].NWall);
                tiles[currX, currY - 1].NWall = null;
                currY--;
            }

            else if (dir == 3 && HaveNotVisit(currX - 1, currY))
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].WWall);
                tiles[currX, currY].WWall = null;
                tiles[currX-1, currY].DestroyWall(tiles[currX-1, currY].EWall);
                tiles[currX - 1, currY].EWall = null;
                currX--;
            }
            tiles[currX, currY].visited = true;
        }
    }

    private void Hunt()
    {
        for(int x = 0;x<Size;x++)
        {
            for(int y= Size-1; y>=0; y--)
            {
                if (tiles[x, y].visited)
                    continue;
                else
                {
                    if(HaveVisitedNeighboor(x,y))
                    {
                        currX = x;
                        currY = y;
                        OpenPath();
                        tiles[currX, currY].visited = true;
                        return;
                    }
                }
            }
        }
        isFinished = true;
    }

    private void OpenPath()
    {
        int dir;
        bool destroyed = false;
        while(!destroyed)
        {
            dir = Random.Range(0, 4);
            if(dir == 0 && currY < (Size - 2) && tiles[currX,currY+1].visited)
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].NWall);
                tiles[currX, currY].NWall = null;
                tiles[currX, currY + 1].DestroyWall(tiles[currX, currY + 1].SWall);
                tiles[currX, currY + 1].SWall = null;
                destroyed = true;
                break;
            }
            else if(dir == 1 && currX < (Size -2) && tiles[currX+1,currY].visited)
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].EWall);
                tiles[currX, currY].EWall = null;
                tiles[currX + 1, currY].DestroyWall(tiles[currX + 1, currY].WWall);
                tiles[currX + 1, currY].WWall = null;
                destroyed = true;
                break;
            }
            else if(dir == 2 && currY > 0 && tiles[currX,currY-1].visited)
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].SWall);
                tiles[currX, currY].SWall = null;
                tiles[currX, currY - 1].DestroyWall(tiles[currX, currY - 1].NWall);
                tiles[currX, currY - 1].NWall = null;
                destroyed = true;
                break;
            }
            else if (dir == 3 && currX > 0 && tiles[currX-1,currY].visited)
            {
                tiles[currX, currY].DestroyWall(tiles[currX, currY].WWall);
                tiles[currX, currY].WWall = null;
                tiles[currX -1, currY].DestroyWall(tiles[currX-1, currY].EWall);
                tiles[currX - 1, currY].EWall = null;
                destroyed = true;
                break;
            }
        }
    }

    private bool CanStillMove(int x, int y)
    {
        if (x > 0 && !tiles[x - 1, y].visited)
            return true;
        if (x < Size - 1 && !tiles[x + 1, y].visited)
            return true;
        if (y > 0 && !tiles[x, y - 1].visited)
            return true;
        if (y < Size - 1 && !tiles[x, y + 1].visited)
            return true;
        return false;
    }

    private bool HaveNotVisit(int x, int y)
    {
        if ((x >= 0) && (x < Size) && (y >= 0) && (y < Size) && (!tiles[x, y].visited))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HaveVisited(int x, int y)
    {
        if ((x >= 0) && (x < Size) && (y >= 0) && (y < Size) && (tiles[x, y].visited))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HaveVisitedNeighboor(int x, int y)
    {
        if (HaveVisited(x + 1, y) || HaveVisited(x - 1, y) || HaveVisited(x, y-1) || HaveVisited(x, y +1))
            return true;
        return false;
    }

    public List<Tile> AvailableNeighboor(Tile input)
    {
        int x = (int)input.pos.x;
        int y = (int)input.pos.y;

        List<Tile> output = new List<Tile>();
        if (x < Size -1 && tiles[x+1,y].WWall == null && tiles[x,y].EWall == null)
            output.Add(tiles[x + 1, y]);

        if (y < Size - 1 && tiles[x, y+1].SWall == null && tiles[x, y].NWall == null)
            output.Add(tiles[x, y+1]);

        if (x > 0 && tiles[x - 1, y].EWall == null && tiles[x, y].WWall == null)
            output.Add(tiles[x - 1, y]);

        if (y > 0 && tiles[x, y - 1].NWall == null && tiles[x, y].SWall == null)
            output.Add(tiles[x, y-1]);

        return output;
    }
}
