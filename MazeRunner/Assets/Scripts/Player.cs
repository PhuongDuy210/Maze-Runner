using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public int classicHighScore;
    public int timeAttackHighScore;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        LoadPlayer();
        SceneManager.sceneLoaded += MenuLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= MenuLoaded;
    }

    //This method only get called if it belongs to the first created Player instance because it subscribe this method before the
    //second time the Menu scene is loaded.
    //The second copy of this instance only just subscribe the method when the scene is loaded the second time so it 
    //won't be called, hence get destroyed and only leave the first instance.
    public void MenuLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene == SceneManager.GetSceneByName("Menu"))
        {
            Player[] players = FindObjectsOfType<Player>();
            foreach(Player player in players)
            {
                if(player != this)
                    Destroy(player.gameObject);
            }
        }
    }

    private void LoadPlayer()
    {
        PlayerData tempData = SaveSystem.LoadPLayer();
        if (tempData != null)
        {
            classicHighScore = tempData.classicHighScore;
            timeAttackHighScore = tempData.timeAttackHighScore;
        }
    }

    private void SavePlayer()
    {
        int[] scoreArray = { classicHighScore, timeAttackHighScore };
        SaveSystem.SavePlayer(scoreArray);
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
    }
}
