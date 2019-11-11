using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MenuController MenuController;
    public DiceManager DiceManager;
    public AudioController AudioController;

    private int RollCount;
    private int HighScore;
    private int CurrentScore;

    private void Start()
    {
        this.HighScore = 0;
        this.ResetGame();
    }

    public void ResetGame()
    {
        this.RollCount = 0;
        this.CurrentScore = 0;
        // Load the previous high score.
        if (PlayerPrefs.HasKey("HighScore"))
        {
            this.HighScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            this.HighScore = 0;
        }
        this.MenuController.UpdateRollCount(this.RollCount);
        this.MenuController.UpdateScoreText(this.CurrentScore, this.CurrentScore);
        this.MenuController.UpdateStartHighScoreText(this.HighScore);
        this.MenuController.ShowStartScreenPanel();
    }

    public void RollDice()
    {
        if (this.DiceManager.Roll())
        {
            this.AudioController.PlaySound(0);
            this.RollCount++;
            this.MenuController.UpdateRollCount(this.RollCount);
        }
    }

    public void AddRollToScore(int score)
    {
        this.MenuController.UpdateScoreText(this.CurrentScore, this.CurrentScore + score);
        this.CurrentScore += score;
        if (score == 2)
        {
            this.MenuController.UpdateFinalScoreText(this.CurrentScore);
            if (this.CurrentScore > this.HighScore)
            {
                this.HighScore = this.CurrentScore;
                this.MenuController.UpdateHighScoreText(this.HighScore);
                this.AudioController.PlaySound(1);
                // Save the high score for future games.
                PlayerPrefs.SetInt("HighScore", this.HighScore);
            }
            else
            {
                this.MenuController.UpdateHighScoreText(0);
            }
            this.MenuController.ShowGameOverPanel();
        }
        else
        {
            this.AudioController.PlaySound(2);
        }
    }

}
