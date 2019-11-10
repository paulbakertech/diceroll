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
        this.MenuController.UpdateRollCount(this.RollCount);
        this.MenuController.UpdateScoreText(this.CurrentScore);
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
        this.CurrentScore += score;
        this.MenuController.UpdateScoreText(this.CurrentScore);
        if (score < 5)
        {
            if (this.CurrentScore > this.HighScore)
            {
                this.HighScore = this.CurrentScore;
            }
            this.MenuController.UpdateFinalScoreText(this.CurrentScore);
            this.MenuController.UpdateHighScoreText(this.HighScore);
            this.MenuController.ShowGameOverPanel();
            this.AudioController.PlaySound(1);
        }
    }

}
