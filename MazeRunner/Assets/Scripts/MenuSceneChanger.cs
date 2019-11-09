using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void TimeAttackMode()
    {
        SceneManager.LoadScene("InitTimeScene");
    }

    public void VersusMode()
    {
        SceneManager.LoadScene("InitVersusScene");
    }

    public void PathFindMode()
    {
        SceneManager.LoadScene("InitClassicScene");
    }
}
