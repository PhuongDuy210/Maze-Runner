using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    public Text playerText;
    public Text computerText;
    public Text roundText;
    public Text announcer;

    public GameMaster gameMaster;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>();
        playerText.text = "Player Score: " + gameMaster.playerScore;
        computerText.text = "Computer Score: " + gameMaster.computerScore;
        roundText.text = "Round: " + gameMaster.round;
        announcer.text = "";
    }

    public void UpdateUI(bool isPlayer)
    {
        if (announcer.text == "")
        {
            gameMaster.round++;
            if (isPlayer)
            {
                announcer.text = "Player Win";
                gameMaster.playerScore++;
                playerText.text = "Player Score: " + gameMaster.playerScore;
            }
            else
            {
                announcer.text = "Computer Win";
                gameMaster.computerScore++;
                computerText.text = "Computer Score: " + gameMaster.computerScore;
            }
        }
    }


    public void ReachGoal(bool isPlayer)
    {
        UpdateUI(isPlayer);
        if (gameMaster.playerScore < 5 && gameMaster.computerScore < 5)
        {
            StartCoroutine(ReloadLevel());
        }
        else
            StartCoroutine(FinishGame());
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Versus"));
    }

    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
