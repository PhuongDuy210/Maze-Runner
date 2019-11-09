using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ClassicUI : MonoBehaviour
{
    public Text level;
    public Text timer;
    public Text moves;
    public Text announcer;
    public Text highScore;
    private int optMoves;
    private float time;
    private PlayerController player;
    public ClassicMaster gameMaster;
    public PathDrawer pathDrawer;
    public BreadthFirst breadthFirst;
    private Player playerData;
    private Scene temp;
    private bool loaded;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName("FindPath"))
        {
            InitLevel();
            loaded = true;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    private void InitLevel()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
            breadthFirst = GetComponent<BreadthFirst>();
            breadthFirst.maze = FindObjectOfType<Maze>();
        }
        if (playerData == null)
            playerData = FindObjectOfType<Player>();
        optMoves = breadthFirst.BFS(breadthFirst.maze.tiles[breadthFirst.maze.startX, breadthFirst.maze.startY], breadthFirst.maze.tiles[breadthFirst.maze.endX, breadthFirst.maze.endY]).Count;
        announcer.text = "";
        level.text = "Level: " + gameMaster.level.ToString();
        if ((gameMaster.level - 1) > playerData.classicHighScore)
            playerData.classicHighScore = gameMaster.level - 1;
        highScore.text = "Highscore: " + playerData.classicHighScore.ToString();
        time = (10f - gameMaster.level / 5) + optMoves * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = "Time left: " + Mathf.Round(time);
        if(time > 0 && player.moveList.Count == 0)
        {
            time -= Time.deltaTime;
            moves.text = "Move: " + pathDrawer.path.Count.ToString() + "/" + optMoves.ToString();
        }
        if (time <= 0 )
        {
            if(loaded)
                FailLevel();
        }
    }

    private void FailLevel()
    {
        announcer.text = "Your score: " + (gameMaster.level - 1);
        Time.timeScale = 0f;
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void EndLevel()
    {
        loaded = false;
        if(player.moveList.Count == optMoves)
        {
            announcer.text = "Perfect";
        }
        else if(Mathf.Abs(player.moveList.Count - optMoves) > 1 && Mathf.Abs(player.moveList.Count - optMoves) <= 7)
        {
            announcer.text = "Great";
        }
        else
        {
            announcer.text = "Clear";
        }
    }
}
