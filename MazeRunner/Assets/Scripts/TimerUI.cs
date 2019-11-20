using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimerUI : MonoBehaviour
{
    public Text levelText;
    public Text timeLeft;
    public Text highScore;
    public Text announcer;
    public TimeMaster gameMaster;
    private RotateToMouse rotateMouse;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMaster.timer > 0)
            gameMaster.timer = gameMaster.timer - Time.deltaTime;
        timeLeft.text = "Time Left: " + Mathf.Round(gameMaster.timer).ToString();
        if (gameMaster.timer <= 0f)
        {
            FailLevel();
        }
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameMaster == null)
        {
            gameMaster = FindObjectOfType<TimeMaster>();
        }
        else
        {
            if (player == null)
                player = Player.instance;
            levelText.text = "Score: " + gameMaster.level.ToString();
            if(gameMaster.level > player.timeAttackHighScore)
            {
                player.timeAttackHighScore = gameMaster.level;
            }
            highScore.text = "Highscore: " + player.timeAttackHighScore.ToString();
        }
        if (scene == SceneManager.GetSceneByName("TimeAttack"))
        {
            rotateMouse = FindObjectOfType<RotateToMouse>();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    private void FailLevel()
    {
        if (Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (rotateMouse.enabled == true)
        {
            announcer.text = "Your score: " + gameMaster.level.ToString();
            rotateMouse.enabled = false;
            Time.timeScale = 0f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }
}
