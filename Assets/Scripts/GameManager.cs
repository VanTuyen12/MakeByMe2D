using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;// loading scene
public class GameManager : MonoBehaviour
{
    private int score = 0;
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private GameObject gameOverUi;
   [SerializeField] private GameObject gameWinUi;
   
    private bool isgameOver ;
    private bool isgameWin;
    private void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
    }

    public void addScore(int points)
    {
        if (!isgameOver && !isgameWin) {
            score += points;
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = ("Coin:" + score.ToString());
    }

    public void ResetGame()
    {
        isgameOver = false;
        UpdateScore();
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;// 1 : chạy bình thường
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        isgameOver = true;
        UpdateScore();
        score = 0;
        Time.timeScale = 0;//Kiểm soát tỷ lệ thời gian mà các thành phần dựa vào thời gian trong game
        /*1: Thời gian chạy bình thường (100% tốc độ thực tế).
        0: Thời gian dừng hoàn toàn (game như bị "pause").
        < 1: Thời gian chạy chậm (ví dụ: 0.5 là 50% tốc độ, slow motion).
        > 1: Thời gian chạy nhanh (ví dụ: 2 là 200% tốc độ, fast-forward).*/
        gameOverUi.SetActive(true);
    }

    public void GameWin()
    {
        isgameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public bool isGameOver()
    {
        return isgameOver;
    }

    public bool isGameWin()
    {
        return isgameWin;
    }
}
