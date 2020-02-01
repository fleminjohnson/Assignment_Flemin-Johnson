using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static bool gameOver = false;
    [SerializeField]
    private TMP_Text[] Points;

    [SerializeField]
    private GameObject gameOverPopUp;

    private ReadCSV csvFile;
    private List<string> color;
    private List<string> point;

    void Start()
    {
        color = new List<string>();
        point = new List<string>();
        csvFile = GetComponent<ReadCSV>();
    }

    void Update()
    {
        for(int i = 0; i< Points.Length; i++)
        {
            Points[i].text = score.ToString();
        }

        if (gameOver)
        {
            Time.timeScale = 0f;
            gameOverPopUp.SetActive(true);
        }
    }

    public void MainMenu()
    {
        gameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
