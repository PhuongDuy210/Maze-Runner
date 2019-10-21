using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private float speed = 20f;
    public Maze maze;
    private UIManager gameMaster;
    private Vector3 target;
    public Tile targetTile;
    public List<Tile> moveList = new List<Tile>();
    private int moveMade = 0;
    private bool moved, loaded, ended;
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = FindObjectOfType<UIManager>();
        transform.position = maze.tiles[maze.startX, maze.startY].floor.transform.position;
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        loaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveList.Count == 0)
        {
            moveList = GetComponent<WallHug>().path;
        }
        else if(!moved)
        {
            targetTile = moveList[0];
        }
        if (targetTile != null)
        {
            MoveTo(targetTile);
        }
        if(ended && !loaded)
        {
            loaded = true;
            gameMaster.ReachGoal(false);
        }
    }

    private void MoveTo(Tile tile)
    {
        if (!moved)
            moved = true;
        target = tile.floor.transform.position;
        target = new Vector3(target.x, transform.position.y, target.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            moveMade++;
            if (moveMade != moveList.Count)
                SetTarget(moveList[moveMade]);
            else
            {
                ended = true;
                targetTile = null;
            }
        }
    }

    private void SetTarget(Tile target)
    {
        if (target != null)
            targetTile = target;
    }
}
