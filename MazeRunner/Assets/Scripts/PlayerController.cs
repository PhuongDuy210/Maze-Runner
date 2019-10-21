using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private float speed = 20f;
    public Maze maze;
    private UIManager gameMaster;
    private BreadthFirst breadthFirst;
    private Vector3 target;
    private Tile targetTile;
    public List<Tile> moveList = new List<Tile>();
    private int moveMade = 0;
    private float ogHeight;

    private Rigidbody rigid;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = FindObjectOfType<UIManager>();
        breadthFirst = GetComponent<BreadthFirst>();
        ogHeight = transform.position.y;
        //transform.position = maze.tiles[maze.startX, maze.startY].floor.transform.position;
        transform.position = new Vector3(maze.tiles[maze.startX, maze.startY].floor.transform.position.x, 1.5f, maze.tiles[maze.startX, maze.startY].floor.transform.position.z);
        rigid = GetComponent<Rigidbody>();
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTile != null)
        {
            MoveTo(targetTile);
        }
    }

    private void MoveTo(Tile tile)
    {
        target = tile.floor.transform.position;
        target = new Vector3(target.x, ogHeight, target.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
       /* if (rigid.velocity == Vector3.zero)
        {
            direction = target - transform.position;
            rigid.velocity = direction.normalized * speed * Time.deltaTime;
        }*/
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
           // direction = Vector3.zero;
            //rigid.velocity = direction;
            moveMade++;
            if (targetTile == maze.tiles[maze.endX, maze.endY])
            {
                if (gameMaster != null)
                {
                    gameMaster.ReachGoal(true);
                    targetTile = null;
                }
                return;
            }
            if (moveMade != moveList.Count)
                SetTarget(moveList[moveMade]);
            else
            {
                targetTile = null;
            }
        }
    }

    public void GetTargerList(Tile input)
    {
        moveMade = 0;
        if (targetTile == null && moveList.Count != 0)
        {
            moveList = breadthFirst.BFS(moveList[moveList.Count-1], input);
        }
        else
        {
            if (moveList.Count == 0)
                moveList = breadthFirst.BFS(maze.tiles[maze.startX, maze.startY], input);
            else if (targetTile != null)
            {
                moveList = breadthFirst.BFS(targetTile, input);
            }
        }
        if(moveList.Count != 0)
            SetTarget(moveList[0]);
    }

    public void SetTarget(Tile target)
    {
        if(target != null)
            targetTile = target;
    }
}
