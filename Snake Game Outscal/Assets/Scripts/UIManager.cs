using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_Text score;
    public int highScore;

    void Start()
    {
        score.text = PlayerPrefs.GetInt("HighScore").ToString();
        highScore = int.Parse(score.text);
        if(GameManager.score > highScore)
        {
            highScore = GameManager.score;
            PlayerPrefs.SetInt("HighScore", highScore);

            score.text = GameManager.score.ToString();
        }
        GameManager.score = 0;
    }


    public void StartGame()
    {
        SceneManager.LoadScene("SnakeGame");
    }
}
