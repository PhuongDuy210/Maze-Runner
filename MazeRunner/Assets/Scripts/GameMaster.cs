using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMaster : MonoBehaviour
{
    public int playerScore = 0;
    public int computerScore = 0;
    public int round = 1;
    public int positionNumber = 0;

    public Maze maze;
    // Start is called before the first frame update
    private void Awake()
    {
    }

    void Start()
    {
        Invoke("LoadLevel", 0.1f);
        SceneManager.sceneLoaded += LevelLoaded;
        SceneManager.sceneUnloaded += LevelUnloaded;
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Versus", LoadSceneMode.Additive);
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName("Versus"))
        {
            maze = FindObjectOfType<Maze>();
            maze.TriggerMaze();
        }
    }

    private void LevelUnloaded(Scene scene)
    {
        SceneManager.LoadScene("Versus", LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
        SceneManager.sceneUnloaded -= LevelUnloaded;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            playerScore = 0;
            computerScore = 0;
            round = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
