using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int classicHighScore;
    public int timeAttackHighScore;


    public PlayerData(int[] highScores)
    {
        classicHighScore = highScores[0];
        timeAttackHighScore = highScores[1];
    }
}
