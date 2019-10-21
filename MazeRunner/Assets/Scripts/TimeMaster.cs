using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TimeMaster : MonoBehaviour
{
    public float timer = 60f;
    public int level = 0;
    public Maze maze;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadLevel", 0.1f);
        SceneManager.sceneLoaded += LevelLoaded;
        SceneManager.sceneUnloaded += LevelUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            timer = 60f;
            level = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("TimeAttack", LoadSceneMode.Additive);
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName("TimeAttack"))
        {
            maze = FindObjectOfType<Maze>();
            maze.TriggerMaze();
        }
    }

    private void LevelUnloaded(Scene scene)
    {
        SceneManager.LoadScene("TimeAttack", LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
        SceneManager.sceneUnloaded -= LevelUnloaded;
    }


    public void EndLevel()
    {
        level++;
        timer += 15f;
        Scene temp = SceneManager.GetSceneByName("TimeAttack");
        if(temp.isLoaded)
            SceneManager.UnloadSceneAsync(temp);
    }
}
