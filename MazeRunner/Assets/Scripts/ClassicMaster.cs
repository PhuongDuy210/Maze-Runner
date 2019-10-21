using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClassicMaster : MonoBehaviour
{
    //private CameraController cameraController;
    public ClassicUI classicUI;
    //public float timer;
    public Maze maze;
    public int level = 1;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadLevel", 0.1f);
        SceneManager.sceneLoaded += LevelLoaded;
        SceneManager.sceneUnloaded += LevelUnloaded;
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("FindPath", LoadSceneMode.Additive);
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName("FindPath"))
        {
            maze = FindObjectOfType<Maze>();
            //maze.classic = this;
            maze.UpdateAllReference(this);
        }
    }

    private void LevelUnloaded(Scene scene)
    {
        SceneManager.LoadScene("FindPath", LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
        SceneManager.sceneUnloaded -= LevelUnloaded;
    }

    public IEnumerator EndLevel()
    {
        classicUI.EndLevel();
        yield return new WaitForSeconds(3f);
        level++;
        Scene temp = SceneManager.GetSceneByName("FindPath");
        if (temp.isLoaded)
            SceneManager.UnloadSceneAsync(temp);
    }
}
