using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public DieController[] Dice;

    private bool WaitingForNewScore;

    public Text ScoreText;

    private int currentScore = 0;

    private void Start()
    {
        this.WaitingForNewScore = false;
        this.UpdateScore();
    }

    private void Update()
    {
        if (this.WaitingForNewScore)
        {
            if (this.CheckIfAllStopped())
            {
                int newScore = this.GetTotalScore();
                this.currentScore += newScore;
                this.UpdateScore();
                this.WaitingForNewScore = false;
                if (newScore == 2)
                {
                    Debug.Log("GAME OVER!");
                }
            }
        }
    }

    private bool CheckIfAllStopped()
    {
        bool allStopped = true;
        foreach (var die in this.Dice)
        {
            if (die.DieStatus != DieStatus.Stopped)
            {
                allStopped = false;
                break;
            }
        }
        return allStopped;
    }

    public void Roll()
    {
        if (!this.WaitingForNewScore)
        {
            this.Reset();
            this.WaitingForNewScore = true;
            foreach (var die in this.Dice)
            {
                die.Roll();
            }
        }
    }

    public void Reset()
    {
        foreach (var die in this.Dice)
        {
            die.Reset();
        }
        this.WaitingForNewScore = false;
    }

    public int GetTotalScore()
    {
        int total = 0;
        foreach (var die in this.Dice)
        {
            total += die.CurrentSide;
        }
        return total;
    }

    private void UpdateScore()
    {
        this.ScoreText.text = string.Format("SCORE: {0}", this.currentScore);
    }
}
